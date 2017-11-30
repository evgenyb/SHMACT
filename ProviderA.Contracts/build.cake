var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var nugetFeed = Argument("nugetFeed", "c:/nuget");
var packageVersion = Argument("packageVersion", "1.2.0");

var solution = File("./ProviderA.Contracts.csproj");

Task("build").Does(() => DotNetBuild(solution));

Task("pack")
  .IsDependentOn("build")
  .Does(() => 
  {
    NuGetPack("./ProviderA.Contracts.nuspec", new NuGetPackSettings
	{
		OutputDirectory = "./out",
		Version = packageVersion
	});
});

Task("default")
  .IsDependentOn("pack");

Task("push")
  .IsDependentOn("pack")
  .Does(() =>
  {
	NuGetPush(
        "./out/ProviderA.Contracts." + packageVersion + ".nupkg",
        new NuGetPushSettings
        {
            Source = nugetFeed
        });
  });
RunTarget(target);