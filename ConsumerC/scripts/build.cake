﻿#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var apiKey = Argument("apiKey", "API-4UYDPMET0PF6EDSFSLN9NUJWH9I");
var nugetPushFeed = Argument("nugetPushFeed", "http://localhost:8085/nuget/packages");
var packageVersion = Argument("packageVersion", "1.3.0");
var packageName = "CC.PA.IntegrationTests";

var solution = File("../ConsumerC.sln");

Task("CI-Build")
	.IsDependentOn("Push-CosumerTests");

Task("NuGet-Restore")
    .Description("Restoring NuGet packages")
    .Does(() =>
	{
		NuGetRestore(solution);
	});

Task("build")
	.IsDependentOn("NuGet-Restore")
	.Does(() => DotNetBuild(solution));

Task("Pack-CosumerTests")
  .IsDependentOn("build")
  .Does(() => 
  {
    NuGetPack("../Tests/CC.PA.IntegrationTests/" + packageName + ".nuspec", new NuGetPackSettings
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

Task("default")
  .IsDependentOn("build");

RunTarget(target);