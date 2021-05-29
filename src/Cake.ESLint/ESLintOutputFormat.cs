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

namespace Cake.ESLint
{
    /// <summary>
    /// Available output formats.
    /// </summary>
    public class ESLintOutputFormat
    {
        private ESLintOutputFormat(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the format: Checkstyle.
        /// </summary>
        public static ESLintOutputFormat Checkstyle { get; } = new ESLintOutputFormat("checkstyle");

        /// <summary>
        /// Gets the format: Codeframe.
        /// </summary>
        public static ESLintOutputFormat Codeframe { get; } = new ESLintOutputFormat("codeframe");

        /// <summary>
        /// Gets the format: Compact.
        /// </summary>
        public static ESLintOutputFormat Compact { get; } = new ESLintOutputFormat("compact");

        /// <summary>
        /// Gets the format: html.
        /// </summary>
        public static ESLintOutputFormat Html { get; } = new ESLintOutputFormat("html");

        /// <summary>
        /// Gets the format: jslint-xml.
        /// </summary>
        public static ESLintOutputFormat JslintXml { get; } = new ESLintOutputFormat("jslint-xml");

        /// <summary>
        /// Gets the format: json.
        /// </summary>
        public static ESLintOutputFormat Json { get; } = new ESLintOutputFormat("json");

        /// <summary>
        /// Gets the format: junit.
        /// </summary>
        public static ESLintOutputFormat Junit { get; } = new ESLintOutputFormat("junit");

        /// <summary>
        /// Gets the format: stylish.
        /// </summary>
        public static ESLintOutputFormat Stylish { get; } = new ESLintOutputFormat("stylish");

        /// <summary>
        /// Gets the format: table.
        /// </summary>
        public static ESLintOutputFormat Table { get; } = new ESLintOutputFormat("table");

        /// <summary>
        /// Gets the format: tap.
        /// </summary>
        public static ESLintOutputFormat Tap { get; } = new ESLintOutputFormat("tap");

        /// <summary>
        /// Gets the format: unix.
        /// </summary>
        public static ESLintOutputFormat Unix { get; } = new ESLintOutputFormat("unix");

        /// <summary>
        /// Gets the format: visualstudio.
        /// </summary>
        public static ESLintOutputFormat VisualStudio { get; } = new ESLintOutputFormat("visualstudio");

        /// <summary>
        /// Gets the value indicating the output format.
        /// </summary>
        public string Value { get; }
    }
}
