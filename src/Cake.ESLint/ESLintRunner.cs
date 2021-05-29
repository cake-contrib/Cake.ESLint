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

            // render arguments
            foreach (var file in settings.Files)
            {
                builder.AppendQuoted(file.FullPath);
            }

            foreach (var directory in settings.Directories)
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
