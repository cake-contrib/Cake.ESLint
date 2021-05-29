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
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.ESLint.Tests.Fixtures
{
    public class ESLintRunnerFixture : ToolFixture<ESLintSettings>
    {
        public ESLintRunnerFixture()
            : base("eslint")
        {
            Log = new FakeLog();
        }

        internal FakeLog Log { get; }

        protected override void RunTool()
        {
            var tool = new ESLintRunner(FileSystem, Environment, ProcessRunner, Tools, Log);
            tool.Run(Settings);
        }

        public IFile GivenFileExists(FilePath file)
        {
            return FileSystem.CreateFile(file);
        }

        public void GivenToolExitsWithCode(int exitCode)
        {
            ProcessRunner.Process.SetExitCode(exitCode);
        }
    }
}
