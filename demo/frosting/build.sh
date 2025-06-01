# ensure a most-recent debug-build, so we can reference that.
dotnet build ../../src/Cake.ESLint/Cake.ESLint.csproj

dotnet run --project ./build/Build.csproj -- "$@"
