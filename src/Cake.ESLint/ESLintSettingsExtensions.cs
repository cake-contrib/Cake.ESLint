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
    }
}
