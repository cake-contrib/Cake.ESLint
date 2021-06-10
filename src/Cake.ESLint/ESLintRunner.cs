/*
 * MIT License
 *
 * Copyright (c) 2021 Nils Andresen
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;

using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.ESLint.Extensions;

namespace Cake.ESLint
{
    /// <summary>
    /// The Runner, to run the tool.
    /// </summary>
    public sealed class ESLintRunner : Tool<ESLintSettings>
    {
        private readonly ICakeEnvironment environment;
        private readonly IToolLocator tools;
        private readonly IFileSystem fileSystem;
        private readonly ICakeLog log;
        private bool continueOnLintError;

        /// <summary>
        /// Initializes a new instance of the <see cref="ESLintRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">An <see cref="IFileSystem"/>.</param>
        /// <param name="environment">An <see cref="ICakeEnvironment"/>.</param>
        /// <param name="processRunner">An <see cref="IProcessRunner"/>.</param>
        /// <param name="tools">An <see cref="IToolLocator"/>.</param>
        /// <param name="log">An <see cref="ICakeLog"/>.</param>
        public ESLintRunner(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            ICakeLog log)
            : base(fileSystem, environment, processRunner, tools)
        {
            this.fileSystem = fileSystem;
            this.tools = tools;
            this.environment = environment;
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Runs the tool.
        /// </summary>
        /// <param name="settings">The settings to run with.</param>
        public void Run(ESLintSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            RegisterLocalLintTool(settings);
            Run(settings, GetArguments(settings));
        }

        /// <inheritdoc cref="Tool{TSettings}.GetToolExecutableNames(TSettings)"/>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            if (environment.Platform.Family == PlatformFamily.Windows)
            {
                return new[] { "eslint.cmd" };
            }

            return new[] { "eslint" };
        }

        /// <inheritdoc cref="Tool{TSettings}.GetToolName"/>
        protected override string GetToolName()
        {
            return "ESLint";
        }

        /// <inheritdoc cref="Tool{TSettings}.ProcessExitCode"/>
        protected override void ProcessExitCode(int exitCode)
        {
            if (exitCode == 1 && continueOnLintError)
            {
                log.Warning("There were errors while linting!");
                return;
            }

            base.ProcessExitCode(exitCode);
        }

        private ProcessArgumentBuilder GetArguments(ESLintSettings settings)
        {
            if (settings.ContinueOnErrors)
            {
                continueOnLintError = true;
            }

            var builder = new ProcessArgumentBuilder();

            // render options
            if (settings.NoEslintRc)
            {
                builder.Append("--no-eslintrc");
            }

            if (settings.UseColors.HasValue)
            {
                builder.Append(settings.UseColors.Value ? "--color" : "--no-color");
            }

            if (settings.OutputFormat != null)
            {
                builder.AppendSwitch("--format", settings.OutputFormat.Value);
            }

            if (settings.Output != null)
            {
                builder.AppendSwitchQuoted("--output-file", settings.Output.FullPath);
            }

            if (settings.Config != null)
            {
                builder.AppendSwitchQuoted("--config", settings.Config.FullPath);
            }

            foreach (var env in settings.Environments.EnsureNotNull())
            {
                builder.AppendSwitch("--env", env);
            }

            foreach (var ext in settings.Extensions.EnsureNotNull())
            {
                builder.AppendSwitch("--ext", ext);
            }

            foreach (var global in settings.Globals.EnsureNotNull())
            {
                builder.AppendSwitchQuoted("--global", global);
            }

            if (settings.Parser != null)
            {
                builder.AppendSwitchQuoted("--parser", settings.Parser);
            }

            foreach (var opts in settings.ParserOptions.EnsureNotNull())
            {
                builder.AppendSwitch("--parser-options", opts);
            }

            if (settings.ResolvePluginsRelativeTo != null)
            {
                builder.AppendSwitchQuoted(
                    "--resolve-plugins-relative-to",
                    settings.ResolvePluginsRelativeTo.FullPath);
            }

            foreach (var dir in settings.RulesDirs.EnsureNotNull())
            {
                builder.AppendSwitchQuoted("--rulesdir", dir.FullPath);
            }

            foreach (var plugin in settings.Plugins.EnsureNotNull())
            {
                builder.AppendSwitch("--plugin", plugin);
            }

            foreach (var rule in settings.Rules.EnsureNotNull())
            {
                builder.AppendSwitchQuoted("--rule", rule);
            }

            if (settings.Fix)
            {
                builder.Append("--fix");
            }

            if (settings.FixDryRun)
            {
                builder.Append("--fix-dry-run");
            }

            foreach (var type in settings.FixTypes.EnsureNotNull())
            {
                builder.AppendSwitch("--fix-type", type.Name);
            }

            if (settings.IgnorePath != null)
            {
                builder.AppendSwitchQuoted("--ignore-path", settings.IgnorePath.FullPath);
            }

            if (settings.NoIgnore)
            {
                builder.Append("--no-ignore");
            }

            foreach (var pattern in settings.IgnorePatterns.EnsureNotNull())
            {
                builder.AppendSwitchQuoted("--ignore-pattern", pattern);
            }

            if (settings.Quiet)
            {
                builder.Append("--quiet");
            }

            if (settings.MaxWarnings.HasValue)
            {
                builder.AppendSwitch("--max-warnings", settings.MaxWarnings.Value.ToString());
            }

            // render arguments
            foreach (var file in settings.Files.EnsureNotNull())
            {
                builder.AppendQuoted(file.FullPath);
            }

            foreach (var directory in settings.Directories.EnsureNotNull())
            {
                builder.AppendQuoted(directory.FullPath);
            }

            return builder;
        }

        private void RegisterLocalLintTool(ToolSettings settings)
        {
            // If the workingDirectory is a npm-project which has
            // eslint as a (dev) dependency, we can simply use that version
            // instead of any other version.
            if (settings.NoWorkingDirectory || settings.WorkingDirectory == null)
            {
                return;
            }

            var nodeBin =
                fileSystem.GetDirectory(settings.WorkingDirectory.Combine("node_modules/.bin"));
            if (!nodeBin.Exists)
            {
                return;
            }

            log.Verbose($"node_modules/.bin exists at {nodeBin.Path.FullPath} - checking for eslint.");
            GetToolExecutableNames()
                .Select(n => fileSystem.GetFile(nodeBin.Path.CombineWithFilePath(n)))
                .Where(f => f.Exists)
                .ToList()
                .ForEach(t =>
                {
                    log.Verbose($"Registering tool: {t.Path.FullPath}");
                    tools.RegisterFile(t.Path);
                });
        }
    }
}
