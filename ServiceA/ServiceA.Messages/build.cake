﻿var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var nugetRestoreFeed = Argument("nugetRestoreFeed", "c:/nuget");
var nugetPushFeed = Argument("nugetPushFeed", "c:/nuget");
var packageVersion = Argument("packageVersion", "1.2.0");

var packageName = "ServiceA.Messages";
var solution = File("./ServiceA.Messages.sln");

Task("build")
	.IsDependentOn("NuGet-Restore")
	.Does(() => DotNetBuild(solution, settings => settings.SetConfiguration("Release")));

Task("pack")
  .IsDependentOn("build")
  .Does(() => 
  {
    NuGetPack("./" + packageName + ".nuspec", new NuGetPackSettings
	{
		OutputDirectory = "./out",
		Version = packageVersion
	});
});

Task("NuGet-Restore")
    .Description("Restoring NuGet packages")
    .Does(() =>
	{
		NuGetRestore(solution);
	});

Task("default")
  .IsDependentOn("pack");

Task("push")
  .IsDependentOn("pack")
  .Does(() =>
  {
	NuGetPush(
        "./out/" + packageName + "." + packageVersion + ".nupkg",
        new NuGetPushSettings
        {
            Source = nugetPushFeed
        });
  });
RunTarget(target);