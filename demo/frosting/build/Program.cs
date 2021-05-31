using Cake.Common.IO;
using Cake.Core;
using Cake.ESLint;
using Cake.Frosting;
using Cake.Npm;
using Cake.Npm.Install;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class BuildContext : FrostingContext
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }
}

[TaskName("ensure-eslint-tool")]
public sealed class EnsureEslintInstalledInTools : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var tools = context.Configuration.GetToolPath(context.Environment.WorkingDirectory, context.Environment);
        context.CreateDirectory(tools);
        var settings = new NpmInstallSettings
        {
            WorkingDirectory = tools
        };
        settings.Packages.Add("eslint");

        context.NpmInstall(settings);
    }
}

[TaskName("manual-installation")]
[IsDependentOn(typeof(EnsureEslintInstalledInTools))]
public sealed class LintWithManualInstallation : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var settings = new ESLintSettings {
            ContinueOnErrors = true
        };
        settings.Directories.Add("../src1");
        context.ESLint(settings);
    }
}

[TaskName("local-project")]
public sealed class LintInLocalNpmProject : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.NpmInstall(new NpmInstallSettings {
            WorkingDirectory = "../src2"
        });

        var settings = new ESLintSettings {
            WorkingDirectory = "../src2",
            Output = "../output.json", // relative to WorkingDirectory
            OutputFormat = ESLintOutputFormat.Json,
            ContinueOnErrors = true
        };
        settings.Directories.Add("."); // relative to WorkingDirectory
        context.ESLint(settings);
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(LintWithManualInstallation))]
[IsDependentOn(typeof(LintInLocalNpmProject))]
// ReSharper disable once UnusedType.Global
public class DefaultTask : FrostingTask
{
}
