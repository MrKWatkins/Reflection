# Repository guidance

- Source and tests live under `src`; build with `dotnet build src/Reflection.slnx`. Root `global.json` selects native Microsoft Testing Platform: run `dotnet test --solution src/Reflection.slnx` from the repository root.
- Use NUnit and `MrKWatkins.Assertions` for tests (`value.Should().Equal(...)` and related fluent assertions). Follow the existing test naming and structure.
- Format source changes with `dotnet format src/Reflection.slnx`.
- Public APIs require XML documentation. Keep XML output enabled so Sesharp can generate documentation and the package can ship IntelliSense summaries.
- Build and Release call `MrKWatkins/build/dotnet/project`. Release publishes the prevalidated package through NuGet trusted publishing and documentation through GitHub Pages.
- Handwritten Markdown lives in `docs/content`; `docs/site.json` defines guide navigation and the API assembly. Technical spelling terms live in `docs/vocabulary`. Use British spelling in prose.
- Generate API pages during the build, never commit them. All rendered sites, generated APIs, package output and coverage belong in ignored `artifacts`.
- Preview with `python3 docs/preview.py` or the JetBrains **Preview documentation** configuration, then open `http://localhost:5173/Reflection/`. Docker and the .NET 10 SDK are required. Keep project automation in Python.
