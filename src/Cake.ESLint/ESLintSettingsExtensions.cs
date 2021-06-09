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
            @this.AddToList(
                x => x.Directories,
                (x, y) => x.Directories = y,
                directoryPaths);
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.Files"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="filePaths">The paths to add.</param>
        public static void AddFile(this ESLintSettings @this, params FilePath[] filePaths)
        {
            @this.AddToList(
                x => x.Files,
                (x, y) => x.Files = y,
                filePaths);
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.RulesDirs"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="rulesDir">The paths to add.</param>
        public static void AddRulesDir(this ESLintSettings @this, params DirectoryPath[] rulesDir)
        {
            @this.AddToList(
                x => x.RulesDirs,
                (x, y) => x.RulesDirs = y,
                rulesDir);
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.Plugins"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="plugin">The plugins to add.</param>
        public static void AddPlugin(this ESLintSettings @this, params string[] plugin)
        {
            @this.AddToList(
                x => x.Plugins,
                (x, y) => x.Plugins = y,
                plugin);
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.Rules"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="rule">The plugins to add.</param>
        public static void AddRule(this ESLintSettings @this, params string[] rule)
        {
            @this.AddToList(
                x => x.Rules,
                (x, y) => x.Rules = y,
                rule);
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.FixTypes"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="fixType">The FixType to add.</param>
        public static void AddFixType(this ESLintSettings @this, params ESLintFixType[] fixType)
        {
            @this.AddToList(
                x => x.FixTypes,
                (x, y) => x.FixTypes = y,
                fixType);
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.IgnorePatterns"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="pattern">The pattern to add.</param>
        public static void AddIgnorePattern(this ESLintSettings @this, params FilePath[] pattern)
        {
            @this.AddToList(
                x => x.IgnorePatterns,
                (x, y) => x.IgnorePatterns = y,
                pattern.Select(x => x.FullPath));
        }

        /// <summary>
        /// adds to <see cref="ESLintSettings.IgnorePatterns"/>.
        /// </summary>
        /// <param name="this">The <see cref="ESLintSettings"/>.</param>
        /// <param name="pattern">The pattern to add.</param>
        public static void AddIgnorePattern(this ESLintSettings @this, params DirectoryPath[] pattern)
        {
            // For directories we ensure a trailing "/" at the end of the pattern.
            @this.AddToList(
                x => x.IgnorePatterns,
                (x, y) => x.IgnorePatterns = y,
                pattern.Select(x => x.FullPath + x.Separator));
        }

        private static void AddToList<T>(
            this ESLintSettings @this,
            Func<ESLintSettings, IEnumerable<T>> getter,
            Action<ESLintSettings, IEnumerable<T>> setter,
            IEnumerable<T> toAdd)
        {
            var existing = getter(@this)?.ToList() ?? new List<T>();
            existing.AddRange(toAdd);
            setter(@this, existing);
        }
    }
}
