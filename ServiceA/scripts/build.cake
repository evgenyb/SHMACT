#tool "NUnit.ConsoleRunner"
#tool "NUnit.Extension.TeamCityEventListener"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var apiKey = Argument("apiKey", "API-4UYDPMET0PF6EDSFSLN9NUJWH9I");
var nugetPushFeed = Argument("nugetPushFeed", "http://localhost:8085/nuget/packages");
var packageVersion = Argument("packageVersion", "1.3.0");
var packageName = "ServiceAToServiceD.IntegrationTests";
var sfPackageName = "ServiceA.SF";

var solution = File("../ServiceA.sln");

Task("CI-Build")
	.IsDependentOn("Push-SF");

Task("NuGet-Restore")
    .Description("Restoring NuGet packages")
    .Does(() =>
	{
		NuGetRestore(solution);
	});

Task("build")
	.IsDependentOn("NuGet-Restore")
	.Does(() => DotNetBuild(solution));

Task("Run-IntegrationTests")
	.IsDependentOn("build")
	.Does(() =>
	{
		RunIntegrationTests("ServiceAToServiceD.IntegrationTests");
	});

Task("Pack-CosumerTests")
  .IsDependentOn("Run-IntegrationTests")
  .Does(() => 
  {
    NuGetPack("../Tests/ServiceAToServiceD.IntegrationTests/" + packageName + ".nuspec", new NuGetPackSettings
	{
		OutputDirectory = "./out",
		Version = packageVersion
	});
});

Task("Push-CosumerTests")
  .IsDependentOn("Pack-CosumerTests")
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

Task("Pack-SF")
  .IsDependentOn("Push-CosumerTests")
  .Does(() => 
  {
    NuGetPack("../ServiceA.SF/" + sfPackageName +".nuspec", new NuGetPackSettings
	{
		OutputDirectory = "./out",
		Version = packageVersion
	});
});

Task("Push-SF")
  .IsDependentOn("Pack-SF")
  .Does(() =>
  {
	NuGetPush(
        "./out/" + sfPackageName + "." + packageVersion + ".nupkg",
        new NuGetPushSettings
        {
            Source = nugetPushFeed,
            ApiKey = apiKey
        });
  });
Task("default")
  .IsDependentOn("build");

public void RunIntegrationTests(string testsProjectName)
{
	var testFiles = $"../Tests/{testsProjectName}/out/*.IntegrationTests.dll";

	var unitTestAssemblies = GetFiles(testFiles);

	NUnit3(
		unitTestAssemblies,
		new NUnit3Settings()
		{
			NoHeader = true,
			NoResults = true,
			TeamCity = BuildSystem.IsRunningOnTeamCity
		});
}
RunTarget(target);