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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Cake.Core;
using Cake.Core.IO;
using Cake.ESLint.Tests.Fixtures;
using Cake.Testing;

using Shouldly;

using Xunit;

using LogLevel = Cake.Core.Diagnostics.LogLevel;

namespace Cake.ESLint.Tests
{
    public class ESLintRunnerTests
    {
        private readonly ESLintRunnerFixture fixture;

        public ESLintRunnerTests()
        {
            fixture = new ESLintRunnerFixture();
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            fixture.Settings = null;

            Action result = () => fixture.Run();

            result.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            fixture.Log = null;

            Action result = () => fixture.Run();

            result.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Should_Throw_If_ESLint_Executable_Was_Not_Found()
        {
            fixture.GivenDefaultToolDoNotExist();

            Action result = () => fixture.Run();

            result
                .ShouldThrow<CakeException>()
                .Message
                .ShouldBe("ESLint: Could not locate executable.");
        }

        [Fact]
        public void Should_use_local_eslint_from_nodeModules_if_exists()
        {
            fixture.GivenDefaultToolDoNotExist();
            var expected = fixture.GivenFileExists("/some/project/node_modules/.bin/eslint");
            fixture.Settings.WorkingDirectory = "/some/project";

            var actual = fixture.Run();

            actual.Path.FullPath.ShouldBe(expected.Path.FullPath);
        }

        [Fact]
        public void Should_use_eslint_cmd_on_windows()
        {
            fixture.GivenDefaultToolDoNotExist();
            var expected = fixture.GivenFileExists("/some/project/node_modules/.bin/eslint");
            fixture.Settings.WorkingDirectory = "/some/project";

            var actual = fixture.Run();

            actual.Path.FullPath.ShouldBe(expected.Path.FullPath);
        }

        [Fact]
        public void Should_use_default_eslint_when_workingDirectory_contains_no_nodeModules()
        {
            fixture.Settings.WorkingDirectory = "/some/project";

            var actual = fixture.Run();

            actual.Path.FullPath.ShouldNotContain("/some/project");
        }

        [Fact]
        public void Should_add_noColor_when_colors_setting_is_false()
        {
            fixture.Settings.UseColors = false;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--no-color");
        }

        [Fact]
        public void Should_add_color_when_colors_setting_is_true()
        {
            fixture.Settings.UseColors = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--color");
        }

        [Fact]
        public void Should_add_noEslintRc_when_noEslintRc_setting_is_true()
        {
            fixture.Settings.NoEslintRc = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--no-eslintrc");
        }

        [Theory]
        [ClassData(typeof(OutputFormatDataGenerator))]
        public void Should_add_outputFormat_when_outputFormat_setting_is_set(
            ESLintOutputFormat format, string expectedFormat)
        {
            fixture.Settings.OutputFormat = format;

            var actual = fixture.Run();

            actual.Args.ShouldContain($"--format {expectedFormat}");
        }

        [Fact]
        public void Should_add_outputFile_when_output_setting_is_set()
        {
            fixture.Settings.Output = new FilePath("/some/strange/output");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--output-file \"/some/strange/output\"");
        }

        [Fact]
        public void Should_add_inputFiles_when_Files_is_set()
        {
            fixture.Settings.Files = new[] {new FilePath("foo"), new FilePath("bar")};

            var actual = fixture.Run();

            actual.Args.ShouldContain("\"foo\" \"bar\"");
        }

        [Fact]
        public void Should_add_inputFiles_when_Files_is_set_from_extension()
        {
            fixture.Settings.AddFile("foo", "bar");

            var actual = fixture.Run();

            actual.Args.ShouldContain("\"foo\" \"bar\"");
        }

        [Fact]
        public void Should_add_inputDirectories_when_Directories_is_set()
        {
            fixture.Settings.Directories = new[] {new DirectoryPath("foo"), new DirectoryPath("bar")};

            var actual = fixture.Run();

            actual.Args.ShouldContain("\"foo\" \"bar\"");
        }

        [Fact]
        public void Should_add_inputDirectories_when_Directories_is_set_from_extension()
        {
            fixture.Settings.AddDirectory("foo", "bar");

            var actual = fixture.Run();

            actual.Args.ShouldContain("\"foo\" \"bar\"");
        }

        [Fact]
        public void Should_not_throw_exception_but_log_warning_if_exitCode_is_1_and_continue_on_error_is_set()
        {
            fixture.Settings.ContinueOnErrors = true;
            fixture.GivenToolExitsWithCode(1);

            fixture.Run();

            var warnings = fixture.Log.Entries
                .Where(x => x.Level == LogLevel.Warning)
                .ToList();
            warnings.Single().Message.ShouldContain("There were errors while linting");
        }

        [Fact]
        public void Should_add_config_arg_when_config_is_set()
        {
            fixture.Settings.Config = new FilePath("/some/totally/different/.eslintrc");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--config \"/some/totally/different/.eslintrc\"");
        }

        [Fact]
        public void Should_add_env_args_when_environment_is_set()
        {
            fixture.Settings.Environments = new[] {"dev", "prod"};

            var actual = fixture.Run();

            actual.Args.ShouldContain("--env dev --env prod");
        }

        [Fact]
        public void Should_add_ext_args_when_extension_is_set()
        {
            fixture.Settings.Extensions = new[] {"ts", "tsx"};

            var actual = fixture.Run();

            actual.Args.ShouldContain("--ext ts --ext tsx");
        }

        [Fact]
        public void Should_add_global_args_when_globals_is_set()
        {
            fixture.Settings.Globals = new[] {"require", "exports:true"};

            var actual = fixture.Run();

            actual.Args.ShouldContain("--global \"require\" --global \"exports:true\"");
        }

        [Fact]
        public void Should_add_parser_arg_when_parser_is_set()
        {
            fixture.Settings.Parser = "espree";

            var actual = fixture.Run();

            actual.Args.ShouldContain("--parser \"espree\"");
        }

        [Fact]
        public void Should_add_parser_arg_when_parser_is_set_from_filePath()
        {
            fixture.Settings.SetParser(new FilePath("/some/custom/parser.js"));

            var actual = fixture.Run();

            actual.Args.ShouldContain("--parser \"/some/custom/parser.js\"");
        }

        [Fact]
        public void Should_add_parser_options_args_when_parserOptions_is_set()
        {
            fixture.Settings.ParserOptions = new []{ "ecmaVersion:7" };

            var actual = fixture.Run();

            actual.Args.ShouldContain("--parser-options ecmaVersion:7");
        }

        [Fact]
        public void Should_add_resolve_plugins_relative_to_arg_when_resolvePluginsRelativeTo_is_set()
        {
            fixture.Settings.ResolvePluginsRelativeTo = new DirectoryPath("../plugins");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--resolve-plugins-relative-to \"../plugins\"");
        }

        [Fact]
        public void Should_add_rulesdir_arg_when_rulesDirs_is_set()
        {
            fixture.Settings.AddRulesDir("my-rules", "my-other-rules");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--rulesdir \"my-rules\" --rulesdir \"my-other-rules\"");
        }

        [Fact]
        public void Should_add_rulesdir_arg_when_rulesDirs_is_set_multiple_times()
        {
            fixture.Settings.AddRulesDir("my-rules");
            fixture.Settings.AddRulesDir("my-other-rules");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--rulesdir \"my-rules\" --rulesdir \"my-other-rules\"");
        }

        [Fact]
        public void Should_add_plugin_arg_when_plugins_is_set()
        {
            fixture.Settings.AddPlugin("jquery", "eslint-plugin-mocha");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--plugin jquery --plugin eslint-plugin-mocha");
        }

        [Fact]
        public void Should_add_plugin_arg_when_plugins_is_set_multiple_times()
        {
            fixture.Settings.AddPlugin("jquery");
            fixture.Settings.AddPlugin("eslint-plugin-mocha");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--plugin jquery --plugin eslint-plugin-mocha");
        }

        [Fact]
        public void Should_add_rule_arg_when_Rules_is_set()
        {
            fixture.Settings.AddRule("guard-for-in: 2", "brace-style: [2, 1tbs]");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--rule \"guard-for-in: 2\" --rule \"brace-style: [2, 1tbs]\"");
        }

        [Fact]
        public void Should_add_rule_arg_when_Rules_is_set_multiple_times()
        {
            fixture.Settings.AddRule("guard-for-in: 2");
            fixture.Settings.AddRule("brace-style: [2, 1tbs]");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--rule \"guard-for-in: 2\" --rule \"brace-style: [2, 1tbs]\"");
        }

        [Fact]
        public void Should_add_fix_arg_when_Fix_is_set()
        {
            fixture.Settings.Fix = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--fix");
        }

        [Fact]
        public void Should_add_fix_dry_run_arg_when_FixDryRun_is_set()
        {
            fixture.Settings.FixDryRun = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--fix-dry-run");
        }

        [Fact]
        public void Should_add_fix_type_arg_when_FixType_is_set()
        {
            fixture.Settings.AddFixType(ESLintFixType.Layout);

            var actual = fixture.Run();

            actual.Args.ShouldContain("--fix-type layout");
        }

        [Fact]
        public void Should_add_fix_type_arg_when_FixType_is_set_with_multiple_flags()
        {
            fixture.Settings.AddFixType(ESLintFixType.Problem, ESLintFixType.Suggestion);

            var actual = fixture.Run();

            actual.Args.ShouldContain("--fix-type problem --fix-type suggestion");
        }

        [Fact]
        public void Should_add_ignore_path_arg_when_IgnorePath_is_set()
        {
            fixture.Settings.IgnorePath = "tmp/.eslintignore";

            var actual = fixture.Run();

            actual.Args.ShouldContain("--ignore-path \"tmp/.eslintignore\"");
        }

        [Fact]
        public void Should_add_ignore_pattern_arg_when_IgnorePatterns_is_set()
        {
            fixture.Settings.AddIgnorePattern(new DirectoryPath("/lib/"));
            fixture.Settings.AddIgnorePattern(new FilePath("/src/vendor/*"));

            var actual = fixture.Run();

            actual.Args.ShouldContain("--ignore-pattern \"/lib/\" --ignore-pattern \"/src/vendor/*\"");
        }

        [Fact]
        public void Should_add_no_ignore_arg_when_NoIgnore_is_set()
        {
            fixture.Settings.NoIgnore = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--no-ignore");
        }

        [Fact]
        public void Should_add_quiet_arg_when_Quiet_is_set()
        {
            fixture.Settings.Quiet = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--quiet");
        }

        [Fact]
        public void Should_add_max_warnings_arg_when_MaxWarnings_is_set()
        {
            fixture.Settings.MaxWarnings = 5;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--max-warnings 5");
        }

        [Fact]
        public void Should_no_inline_config_arg_when_NoInlineConfig_is_set()
        {
            fixture.Settings.NoInlineConfig = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--no-inline-config");
        }

        [Fact]
        public void Should_add_report_unused_disable_directives_arg_when_ReportUnusedDisableDirectives_is_set()
        {
            fixture.Settings.ReportUnusedDisableDirectives = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--report-unused-disable-directives");
        }

        [Fact]
        public void Should_add_cache_arg_when_Cache_is_set()
        {
            fixture.Settings.Cache = true;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--cache");
        }

        [Fact]
        public void Should_add_cache_location_arg_when_CacheLocation_is_set_from_file()
        {
            fixture.Settings.CacheLocation = new FilePath("foo.cache");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--cache-location \"foo.cache\"");
        }

        [Fact]
        public void Should_add_cache_location_arg_when_CacheLocation_is_set_from_directory()
        {
            fixture.Settings.CacheLocation = new DirectoryPath("/tmp/cache");

            var actual = fixture.Run();

            actual.Args.ShouldContain("--cache-location \"/tmp/cache\"");
        }

        [Fact]
        public void Should_add_cache_strategy_arg_when_CacheStrategy_is_set_to_metadata()
        {
            fixture.Settings.CacheStrategy = ESLintCacheStrategy.Metadata;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--cache-strategy metadata");
        }

        [Fact]
        public void Should_add_cache_strategy_arg_when_CacheStrategy_is_set_to_content()
        {
            fixture.Settings.CacheStrategy = ESLintCacheStrategy.Content;

            var actual = fixture.Run();

            actual.Args.ShouldContain("--cache-strategy content");
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class OutputFormatDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> data = new List<object[]>
            {
                new object[] {ESLintOutputFormat.Checkstyle, "checkstyle"},
                new object[] {ESLintOutputFormat.Codeframe, "codeframe"},
                new object[] {ESLintOutputFormat.Compact, "compact"},
                new object[] {ESLintOutputFormat.Html, "html"},
                new object[] {ESLintOutputFormat.Json, "json"},
                new object[] {ESLintOutputFormat.Junit, "junit"},
                new object[] {ESLintOutputFormat.Stylish, "stylish"},
                new object[] {ESLintOutputFormat.Table, "table"},
                new object[] {ESLintOutputFormat.Tap, "tap"},
                new object[] {ESLintOutputFormat.Unix, "unix"},
                new object[] {ESLintOutputFormat.JslintXml, "jslint-xml"},
                new object[] {ESLintOutputFormat.VisualStudio, "visualstudio"},
            };

            public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
