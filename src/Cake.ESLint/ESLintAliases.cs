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

using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.ESLint
{
    /// <summary>
    /// <para>Functions to call <see href="https://eslint.org/">eslint</see>.</para>
    /// <para>
    /// In order to use this add-in, eslint has to be available.
    /// One option is using
    /// <see href="https://cake-contrib.github.io/Cake.Npm.Module/">Cake.Npm.Module</see> to install
    /// <see href="https://www.npmjs.com/package/eslint">ESLint from npmjs.com</see>.
    /// </para>
    /// <para>
    /// To install add the following lines to your cake-file:
    /// <code>
    /// <![CDATA[
    /// #module nuget:?package=Cake.Npm.Module&version=2.0.2
    /// #tool npm:?package=eslint&version=7.27.0
    /// #addin "nuget:?package=Cake.ESLint"
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
        [CakeMethodAlias]
        public static void ESLint(this ICakeContext context)
        {
            ESLint(context, new ESLintSettings());
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
