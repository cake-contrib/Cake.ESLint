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

namespace Cake.ESLint
{
    /// <summary>
    /// Options for <c>--fix-type</c>.
    /// </summary>
    public sealed class ESLintFixType
    {
        private ESLintFixType(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the fix-type: Problem.
        /// </summary>
        public static ESLintFixType Problem { get; } = new ESLintFixType("problem");

        /// <summary>
        /// Gets the fix-type: Suggestion.
        /// </summary>
        public static ESLintFixType Suggestion { get; } = new ESLintFixType("suggestion");

        /// <summary>
        /// Gets the fix-type: Layout.
        /// </summary>
        public static ESLintFixType Layout { get; } = new ESLintFixType("layout");

        /// <summary>
        /// Gets the name of the problem.
        /// </summary>
        public string Name { get; }
    }
}
