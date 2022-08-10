#load nuget:?package=Cake.Recipe&version=3.0.1

// Workaround for https://github.com/cake-contrib/Cake.Recipe/issues/854
#tool nuget:?package=NuGet.CommandLine&version=5.8.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.ESLint",
    masterBranchName: "main",
    repositoryOwner: "cake-contrib",
    shouldRunDotNetCorePack: true,
    shouldUseDeterministicBuilds: true,
    preferredBuildProviderType: BuildProviderType.GitHubActions,
    preferredBuildAgentOperatingSystem: PlatformFamily.Linux);

BuildParameters.PrintParameters(Context);


// workaround for https://github.com/cake-contrib/Cake.Recipe/issues/862
ToolSettings.SetToolPreprocessorDirectives(
    reSharperTools: "#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2021.2.0");

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNetCore();
