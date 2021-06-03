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

using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.ESLint
{
    /// <summary>
    /// <para>Functions to call <see href="https://eslint.org/">eslint</see>.</para>
    /// <para>
    /// In order to use this add-in, eslint has to be available.
    /// </para>
    /// <para>
    /// One option is using
    /// <see href="https://cake-contrib.github.io/Cake.Npm.Module/">Cake.Npm.Module</see> to install
    /// <see href="https://www.npmjs.com/package/eslint">ESLint from npmjs.com</see>.
    /// </para>
    /// <para>
    /// Another option is, if you have an npm project setup, to add ESLint to
    /// package.json and call NpmInstall() to be sure it is available.
    /// </para>
    /// <para>
    /// Example:
    /// <code>
    /// <![CDATA[
    /// #addin "nuget:?package=Cake.Npm"
    /// #addin "nuget:?package=Cake.ESLint"
    /// Task("lint")
    ///   .Does(() => {
    ///     // ensure elsint is installed from npm
    ///     NpmInstall(new NpmInstallSettings {
    ///       WorkingDirectory = "src"
    ///     });
    ///
    ///     // run lint
    ///     ESLint(x => {
    ///         x.WorkingDirectory = "src2";
    ///         x.Output = "../output.json"; // relative to WorkingDirectory
    ///         x.OutputFormat = ESLintOutputFormat.Json;
    ///         x.ContinueOnErrors = true;
    ///         x.AddDirectory("."); // relative to WorkingDirectory
    ///      });
    ///   });
    /// ]]>
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("ESLint")]
    public static class ESLintAliases
    {
        /// <summary>
        /// Runs ESLint, using the default settings.
        /// <seealso cref="ESLint(Cake.Core.ICakeContext, ESLintSettings)"/>
        /// </summary>
        /// <param name="context">The <see cref="ICakeContext"/>.</param>
        /// <param name="modification">The modifications to the <see cref="ESLintSettings"/>.</param>
        [CakeMethodAlias]
        public static void ESLint(this ICakeContext context, Action<ESLintSettings> modification = null)
        {
            var settings = new ESLintSettings();
            modification?.Invoke(settings);
            ESLint(context, settings);
        }

        /// <summary>
        /// Runs ESLint, using the given settings.
        /// </summary>
        /// <param name="context">The <see cref="ICakeContext"/>.</param>
        /// <param name="settings">The <see cref="ESLintSettings"/> to use.</param>
        [CakeMethodAlias]
        public static void ESLint(this ICakeContext context, ESLintSettings settings)
        {
            var runner = new ESLintRunner(
                context.FileSystem,
                context.Environment,
                context.ProcessRunner,
                context.Tools,
                context.Log);
            runner.Run(settings);
        }
    }
}
