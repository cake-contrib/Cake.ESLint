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
using System.Linq;

using Cake.Core.IO;

namespace Cake.ESLint
{
    /// <summary>
    /// Convenient extensions for <see cref="ESLintSettings"/>.
    /// </summary>
    public static class ESLintSettingsExtensions
    {
        /// <summary>
        /// Sets <see cref="ESLintSettings.Parser"/> to a FilePath.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="pathToPlugin">The <see cref="FilePath"/> to set.</param>
        public static void SetParser(this ESLintSettings @this, FilePath pathToPlugin)
        {
            @this.Parser = pathToPlugin.FullPath;
        }

        /// <summary>
        /// Adds to <see cref="ESLintSettings.Directories"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="directoryPaths">The paths to add.</param>
        public static void AddDirectory(this ESLintSettings @this, params DirectoryPath[] directoryPaths)
        {
            var paths = @this.Directories?.ToList() ?? new List<DirectoryPath>();
            paths.AddRange(directoryPaths);
            @this.Directories = paths;
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.Files"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="filePaths">The paths to add.</param>
        public static void AddFile(this ESLintSettings @this, params FilePath[] filePaths)
        {
            var paths = @this.Files?.ToList() ?? new List<FilePath>();
            paths.AddRange(filePaths);
            @this.Files = paths;
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.RulesDirs"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="rulesDir">The paths to add.</param>
        public static void AddRulesDir(this ESLintSettings @this, params DirectoryPath[] rulesDir)
        {
            var paths = @this.RulesDirs?.ToList() ?? new List<DirectoryPath>();
            paths.AddRange(rulesDir);
            @this.RulesDirs = paths;
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.Plugins"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="plugin">The plugins to add.</param>
        public static void AddPlugin(this ESLintSettings @this, params string[] plugin)
        {
            var plugins = @this.Plugins?.ToList() ?? new List<string>();
            plugins.AddRange(plugin);
            @this.Plugins = plugins;
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.Rules"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="rule">The plugins to add.</param>
        public static void AddRule(this ESLintSettings @this, params string[] rule)
        {
            var plugins = @this.Rules?.ToList() ?? new List<string>();
            plugins.AddRange(rule);
            @this.Rules = plugins;
        }
    }
}
