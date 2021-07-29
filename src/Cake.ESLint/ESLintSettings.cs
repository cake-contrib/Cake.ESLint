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

using System.Collections.Generic;

using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.ESLint
{
    /// <summary>
    /// Settings to run eslint.
    /// </summary>
    public sealed class ESLintSettings : ToolSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ESLintSettings"/> class.
        /// </summary>
        public ESLintSettings()
        {
            Files = new List<FilePath>();
            Directories = new List<DirectoryPath>();
        }

        /// <summary>
        /// Gets or Sets a value indicating whether to disable use of configuration from eslintrc.
        /// <para>Option: <c>--no-eslintrc</c>.</para>
        /// </summary>
        public bool NoEslintRc { get; set; }

        /// <summary>
        /// Gets or sets the Output file.
        /// <para>Option: <c>--output</c>.</para>
        /// </summary>
        public FilePath Output { get; set; }

        /// <summary>
        /// Gets or sets whether to force the using of colors.
        /// <para>Option: <c>--color</c> or <c>--no-color</c>.</para>
        /// </summary>
        public bool? UseColors { get; set; }

        /// <summary>
        /// Gets or sets output Format.
        /// <para>Option: <c>--format</c>.</para>
        /// </summary>
        public ESLintOutputFormat OutputFormat { get; set; }

        /// <summary>
        /// Gets or sets a path to a config file.
        /// <para>Option: <c>--config</c>.</para>
        /// </summary>
        public FilePath Config { get; set; }

        /// <summary>
        /// Gets or sets the environments.
        /// <para></para>
        /// <para>Option: <c>--env</c>.</para>
        /// </summary>
        public IEnumerable<string> Environments { get; set; }

        /// <summary>
        /// Gets or sets JavaScript file extensions.
        /// <para>Option: <c>--ext</c>.</para>
        /// </summary>
        public IEnumerable<string> Extensions { get; set; }

        /// <summary>
        /// Gets or sets global variables.
        /// <para>Option: <c>--global</c>.</para>
        /// </summary>
        public IEnumerable<string> Globals { get; set; }

        /// <summary>
        /// Gets or sets the parser to be used.
        /// <para>Option: <c>--parser</c>.</para>
        /// </summary>
        public string Parser { get; set; }

        /// <summary>
        /// Gets or sets the parser options.
        /// <para>Option: <c>--parser-options</c>.</para>
        /// </summary>
        public IEnumerable<string> ParserOptions { get; set; }

        /// <summary>
        /// Gets or sets a folder where plugins should be resolved from.
        /// <para>Option: <c>--resolve-plugins-relative-to</c>.</para>
        /// </summary>
        public DirectoryPath ResolvePluginsRelativeTo { get; set; }

        /// <summary>
        /// Gets or sets a folder where additional rules are located.
        /// <para>Option: <c>--rulesdir</c>.</para>
        /// </summary>
        public IEnumerable<DirectoryPath> RulesDirs { get; set; }

        /// <summary>
        /// Gets or sets the plugins to use.
        /// <para>Option: <c>--plugin</c>.</para>
        /// </summary>
        public IEnumerable<string> Plugins { get; set; }

        /// <summary>
        /// Gets or sets the Rule(s).
        /// <para>Option: <c>--rule</c>.</para>
        /// </summary>
        public IEnumerable<string> Rules { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to fix problems.
        /// <para>Option: <c>--fix</c>.</para>
        /// </summary>
        public bool Fix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to fix problems without saving the changes to the file system.
        /// <para>Option: <c>--fix-dry-run</c>.</para>
        /// </summary>
        public bool FixDryRun { get; set; }

        /// <summary>
        /// Gets or sets the types of fixes to apply.
        /// <para>Option: <c>--fix-types</c>.</para>
        /// </summary>
        public IEnumerable<ESLintFixType> FixTypes { get; set; }

        /// <summary>
        /// Gets or sets the path to the ignoreFile.
        /// <para>Option: <c>--ignore-path</c>.</para>
        /// </summary>
        public FilePath IgnorePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// to disable use of ignore files and patterns.
        /// <para>Option: <c>--no-ignore</c>.</para>
        /// </summary>
        public bool NoIgnore { get; set; }

        /// <summary>
        /// Gets or sets patterns of files to ignore
        /// (in addition to those in .eslintignore).
        /// <para>Option: <c>--ignore-pattern</c>.</para>
        /// </summary>
        public IEnumerable<string> IgnorePatterns { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// to report errors only.
        /// <para>Option: <c>--quiet</c>.</para>
        /// </summary>
        public bool Quiet { get; set; }

        /// <summary>
        /// Gets or sets the number of warnings to trigger nonzero exit code.
        /// <para>Option: <c>--max-warnings</c>.</para>
        /// </summary>
        public int? MaxWarnings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// to prevent comments from changing config or rules.
        /// <para>Option: <c>--no-inline-config</c>.</para>
        /// </summary>
        public bool NoInlineConfig { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// to report errors for unused eslint-disable directives.
        /// <para>Option: <c>--report-unused-disable-directives</c>.</para>
        /// </summary>
        public bool ReportUnusedDisableDirectives { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// to check only changed files.
        /// <para>Option: <c>--cache</c>.</para>
        /// </summary>
        public bool Cache { get; set; }

        /// <summary>
        /// Gets or sets the path to the cache file or directory.
        /// <para>Option: <c>--cache-location</c>.</para>
        /// </summary>
        public Path CacheLocation { get; set; }

        /// <summary>
        /// Gets or sets the strategy to use for detecting changed files.
        /// <para>Option: <c>--cache-strategy</c>.</para>
        /// </summary>
        public ESLintCacheStrategy CacheStrategy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to continue on lint errors.
        /// <para>
        /// The default is to throw a <see cref="CakeException"/> if the
        /// tool returns a non-zero exit code.
        /// If <c>ContinueOnErrors</c> is set to true a warning will be
        /// written to the log file but no exception will be thrown, for
        /// exit code 1 (see <see href="https://eslint.org/docs/user-guide/command-line-interface#exit-codes">exit-codes in the docs</see>).
        /// </para>
        /// </summary>
        public bool ContinueOnErrors { get; set; }

        /// <summary>
        /// Gets or sets the list of Files to lint.
        /// </summary>
        public IEnumerable<FilePath> Files { get; set; }

        /// <summary>
        /// Gets or sets the list of directories to lint.
        /// </summary>
        public IEnumerable<DirectoryPath> Directories { get; set; }
    }
}
