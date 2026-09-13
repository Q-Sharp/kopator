# kopator

Simple copy/move tool with persistent settings — a small cross-platform desktop
application built with Avalonia on .NET 10.

The user interface is in German.

## Modes

| Tab | What it does |
| --- | --- |
| **Kopieren** | Copies — or, with *Verschieben?*, moves — the files of one folder into another. Subfolders are not descended into. |
| **Sammeln** | Flattens a folder tree in place: every file below the chosen folder is moved up into it, then **all** subfolders are deleted. Files matching an ignore pattern are not collected and are deleted along with their subfolder — collecting is a cleanup, not a copy. Ignored files sitting directly in the chosen folder are left alone. |
| **Katalogisieren** | Writes a listing of a folder tree as CSV, or as HTML with embedded thumbnails for images. |

Settings are stored as JSON in `~/.config/kopator/settings.json` on Linux and in
`%APPDATA%\kopator\settings.json` on Windows.

## Requirements

[.NET SDK 10](https://dotnet.microsoft.com/download) — no other tooling is needed,
and the application builds and runs on both Linux and Windows.

## Build and run

```sh
dotnet build kopator.slnx
dotnet test
dotnet run --project src/Kopator.App
```

## Release binaries

Self-contained single-file executables that need no installed .NET runtime:

```sh
dotnet publish src/Kopator.App -c Release -r linux-x64 --self-contained true
dotnet publish src/Kopator.App -c Release -r win-x64   --self-contained true
```

Versions come from git tags via [MinVer](https://github.com/adamralph/MinVer):
pushing a `vX.Y.Z` tag makes CI build both binaries and attach them to a GitHub
release. A tag with a pre-release part (`v1.2.0-rc.1`) is published as a
pre-release.
