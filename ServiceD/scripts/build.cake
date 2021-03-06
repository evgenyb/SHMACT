﻿#tool "NUnit.ConsoleRunner"
#tool "NUnit.Extension.TeamCityEventListener"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var apiKey = Argument("apiKey", "API-4UYDPMET0PF6EDSFSLN9NUJWH9I");
var nugetPushFeed = Argument("nugetPushFeed", "http://localhost:8085/nuget/packages");
var nugetRestoreFeed = Argument("nugetRestoreFeed", "c:\\nuget");
var packageVersion = Argument("packageVersion", "6.1.0");

var packageName = "ServiceD.SF";
var solution = File("../ServiceD.sln");

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
    NuGetPack("../ServiceD.SF/" + packageName +".nuspec", new NuGetPackSettings
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

Task("Run-ServiceA-IntegrationTests")
	.Does(() =>
	{
		RunConsumerIntegrationTests("serviceatoserviced.integrationtests");
	});

Task("Run-ServiceB-IntegrationTests")
	.Does(() =>
	{
		RunConsumerIntegrationTests("servicebtoserviced.integrationtests");
	});

public void RunConsumerIntegrationTests(string testsPackageName)
{
	var packageFolder = Directory("../packages/") + Directory(testsPackageName);
	var testFiles = $"{packageFolder}/**/tests/*.IntegrationTests.dll";
	NuGetInstall(testsPackageName, new NuGetInstallSettings
	{
		Source = new[] { nugetRestoreFeed },
		OutputDirectory = packageFolder
	});

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