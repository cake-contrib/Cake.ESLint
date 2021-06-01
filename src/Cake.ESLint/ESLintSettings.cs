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
