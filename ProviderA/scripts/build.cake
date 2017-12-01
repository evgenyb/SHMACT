#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool "NUnit.Extension.TeamCityEventListener"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var apiKey = Argument("apiKey", "API-4UYDPMET0PF6EDSFSLN9NUJWH9I");
var nugetPushFeed = Argument("nugetPushFeed", "http://localhost:8085/nuget/packages");
var nugetRestoreFeed = Argument("nugetRestoreFeed", "c:\\nuget");
var packageVersion = Argument("packageVersion", "6.1.0");

var packageName = "ProviderA";
var solution = File("../ProviderA.sln");

Task("NuGet-Restore")
    .Description("Restoring NuGet packages")
    .Does(() =>
	{
		NuGetRestore(solution);
	});


Task("build")
	.IsDependentOn("NuGet-Restore")
	.Does(() => DotNetBuild(solution));

Task("pack")
  .IsDependentOn("build")
  .Does(() => 
  {
    NuGetPack("../ProviderA/" + packageName +".nuspec", new NuGetPackSettings
	{
		OutputDirectory = "./out",
		Version = packageVersion
	});
});

Task("push")
  .IsDependentOn("pack")
  .Does(() =>
  {
	NuGetPush(
        "./out/" + packageName + "." + packageVersion + ".nupkg",
        new NuGetPushSettings
        {
            Source = nugetPushFeed,
            ApiKey = apiKey
        });
  });

Task("default")
  .IsDependentOn("pack");


Task("Run-ConsumerA-IntegrationTests")
	.Does(() =>
	{
		var testsPackageName = "ca.pa.integrationtests";
		var unitTestAssemblies = GetFiles("./" + testsPackageName + ".*/tests/*.IntegrationTests.dll");

		NuGetInstall(testsPackageName, new NuGetInstallSettings
		{
			Source = new[] { nugetRestoreFeed }
		});

		NUnit3(
			unitTestAssemblies,
			new NUnit3Settings()
			{
				NoHeader = true,
				NoResults = true,
				TeamCity = BuildSystem.IsRunningOnTeamCity
			});		
	});


RunTarget(target);