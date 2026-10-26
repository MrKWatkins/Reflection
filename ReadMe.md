# MrKWatkins.Reflection

[![Build Status](https://github.com/MrKWatkins/Reflection/actions/workflows/build.yml/badge.svg)](https://github.com/MrKWatkins/Reflection/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/MrKWatkins.Reflection)](https://www.nuget.org/packages/MrKWatkins.Reflection)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MrKWatkins.Reflection)](https://www.nuget.org/packages/MrKWatkins.Reflection)

> Reflection based extension methods for .NET.

Documentation is available at [mrkwatkins.github.io/Reflection](https://mrkwatkins.github.io/Reflection/).

## Building and documentation

Build and Test compiles the solution, runs its tests and validates the complete documentation on pushes and pull requests. Release runs the same validation,
then publishes the prepared package and documentation. Leave the version blank for the next minor release, or enter an explicit stable version.
Both publication options default to true. Turn **publish-packages** off to update only documentation without creating a package or release tag.
The API reference then uses the latest released source; `api-ref` can select corrected XML comments from another commit.

Preview with `python3 docs/preview.py` or **Preview documentation** in a JetBrains IDE, with Docker and the .NET 10 SDK installed.
Open `http://localhost:5173/Reflection/`. Generated API pages and site output remain ignored build artifacts.

Configure NuGet trusted publishing for repository `Reflection`, workflow `release.yml`, environment `github-pages`, owner `MrKWatkins` and package `MrKWatkins.Reflection`.
Set GitHub Pages to **GitHub Actions**, and allow `main` to deploy to the `github-pages` environment. No stored NuGet API key is required.
