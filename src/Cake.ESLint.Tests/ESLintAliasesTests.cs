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

using System.Threading.Tasks;

using Cake.Core.IO;
using Cake.ESLint.Tests.Fixtures;

using VerifyXunit;

using Xunit;

namespace Cake.ESLint.Tests
{
    public class ESLintAliasesTests
    {
        private readonly ESLintAliasesFixture fixture;

        public ESLintAliasesTests()
        {
            fixture = new ESLintAliasesFixture();
        }

        [Fact]
        public async Task Runs_tool_with_no_settings()
        {
            fixture.Settings = null;

            var result = fixture.Run();

            await Verifier.Verify(result);
        }

        [Fact]
        public async Task Runs_tool_with_given_settings()
        {
            var result = fixture.Run();

            await Verifier.Verify(result);
        }

        [Fact]
        public async Task Runs_tool_with_given_action()
        {
            fixture.Settings = null;
            fixture.Action = x => x.Files = new[] {new FilePath("**/*")};
            var result = fixture.Run();

            await Verifier.Verify(result);
        }
    }
}
