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
        /// </summary>
        public bool NoEslintRc { get; set; }

        /// <summary>
        /// Gets or sets the Output file.
        /// </summary>
        public FilePath Output { get; set; }

        /// <summary>
        /// Gets or sets whether to force the using of colors.
        /// </summary>
        public bool? UseColors { get; set; }

        /// <summary>
        /// Gets or sets output Format.
        /// </summary>
        public ESLintOutputFormat OutputFormat { get; set; }

        /// <summary>
        /// Gets the list of Files to lint.
        /// </summary>
        public IList<FilePath> Files { get; }

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
        /// Gets the list of directories to lint.
        /// </summary>
        public IList<DirectoryPath> Directories { get; }
    }
}
