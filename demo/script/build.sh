#!/usr/bin/env bash
set -euox pipefail

cd "$(dirname "${BASH_SOURCE[0]}")"

export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_NOLOGO=1

# ensure a most-recent debug-build, so we can reference that.
dotnet build ../../src/Cake.ESLint/Cake.ESLint.csproj

dotnet tool restore

dotnet cake "$@"
