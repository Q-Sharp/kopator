# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

`kopator` — a German-language desktop tool for copying/moving, flattening ("collecting")
and cataloguing files. Avalonia 12 on .NET 10; builds and runs on Linux and Windows.

Ported from WinForms/.NET Framework 4.8 in September 2026; anything describing AlphaFS,
Costura.Fody, `Settings.Default` or `MessageBox` refers to the pre-port code.

## Commands

```sh
dotnet build kopator.slnx          # all three projects
dotnet test                        # 57 tests, Kopator.Core.Tests
dotnet run --project src/Kopator.App
dotnet test --filter-method '*CollectService*'     # single class
dotnet test --filter-method '*DegenerateIgnore*'   # single test
```

`TreatWarningsAsErrors` is on for every project, so a warning fails the build.

Tests use **xUnit v3 on Microsoft.Testing.Platform**, not VSTest. The opt-in lives in
`global.json` (`test.runner`); without it `dotnet test` fails outright on the .NET 10 SDK.
That also means MTP filter syntax (`--filter-method`), not `--filter`.

## Layout

| Path | Role |
| --- | --- |
| `src/Kopator.Core` | All file-system logic. No UI dependency — this is what the tests drive. |
| `src/Kopator.App` | Avalonia UI. Assembly name is `kopator`, so the executable keeps its historic name. |
| `tests/Kopator.Core.Tests` | xUnit v3 suite. |

`Directory.Build.props` holds the shared TFM/nullable/warning settings and MinVer;
`Directory.Packages.props` holds every package version (central package management,
so a `PackageReference` must **not** carry a `Version` attribute).

## Architecture

**Core exposes one service per mode**, each shaped the same way:

```csharp
OperationResult Execute(<Mode>Request request, IProgress<int>? progress, CancellationToken ct)
```

A service never shows a dialog and never touches a window. It validates, works, and
returns an `OperationResult` carrying `Completed` / `Cancelled` / `Invalid` plus counts;
an `Invalid` result names a `ValidationError`. The German wording for every outcome lives
in the view models (`OperationViewModel.DescribeError`, `CompletedMessage`,
`CancelledMessage`), which is what keeps Core testable.

**Each tab is an `OperationViewModel`** (`CopyViewModel`, `CollectViewModel`,
`CatalogViewModel`) that wraps one service. `MainWindowViewModel` owns everything shared
across tabs: the move checkbox, progress bar, the action button that doubles as "Stop"
while running, and the settings round-trip. Adding a mode means: a `KopatorMode` value, a
service in Core, an `OperationViewModel` subclass, a matching `*View.axaml`, and an entry
in the `MainWindowViewModel` constructor's `Tabs` list — the tab strip is data-bound, so
there is no index-to-enum cast to keep in sync any more (the WinForms version had one).

`ViewLocator` resolves a view model to its view **by name**: `FooViewModel` → `FooView`.
A view model without a matching view silently renders as "Not Found: ...".

**Dialogs go through `IDialogService`** so view models stay window-free.
`StorageDialogService` implements it with Avalonia's `StorageProvider` (native file
chooser; `IStorageItem.TryGetLocalPath()` converts back to a path). Avalonia has no
`MessageBox` — `Views/MessageWindow` is the in-repo replacement.

## Things that will bite

- **`progress?.Report(++processed)` silently drops the increment** when `progress` is
  null: the null-conditional short-circuits the whole expression. Increment on its own
  line. This was a real bug the tests caught.
- **Avalonia's `Bitmap` needs an initialized render platform** and throws
  `Unable to locate 'Avalonia.Platform.IPlatformRenderInterface'` in a plain console or
  test process. Thumbnails therefore use **SkiaSharp directly**
  (`SkiaThumbnailProvider`), which decodes headlessly. Keep it that way — it is what
  makes catalogue export testable.
- **SkiaSharp is pinned to 3.119.4**, the version Avalonia.Skia pulls in transitively.
  A 4.x reference creates a package downgrade conflict.
- The xUnit analyser requires `TestContext.Current.CancellationToken` rather than
  `default` wherever a test passes a token (xUnit1051, an error here).
- `Progress<T>` must be constructed on the UI thread for its callbacks to marshal back;
  `MainWindowViewModel.ExecuteAsync` relies on that. Tests use `SynchronousProgress<T>`
  instead, because `Progress<T>` would race the assertions.
- Long paths need no special handling any more (that was AlphaFS's job); plain
  `System.IO` handles them on .NET 10 on both platforms.
- **Collect deletes ignored files on purpose.** `CollectService.RemoveSubdirectories`
  wipes every subdirectory recursively once a run completes, so a file excluded by an
  ignore pattern is destroyed along with its folder. This is intended behaviour the user
  has confirmed — do not "fix" it into an empty-directories-only sweep. Only a cancelled
  run deletes nothing.

## Settings

One `KopatorSettings` POCO serialized as JSON to `~/.config/kopator/settings.json`
(Linux) or `%APPDATA%\kopator\settings.json` (Windows) — one place to change, unlike the
old three-file `Settings.settings` / `Settings.Designer.cs` / `App.config` arrangement.
Enums persist by name. A missing or corrupt file yields defaults rather than throwing,
which is covered by tests; keep it that way, since it runs during startup.

## Versioning and CI

MinVer derives the version from git tags with prefix `v`. An untagged build is
`0.0.0-alpha.0.<height>`; tag `v1.4.0` yields `1.4.0`. Pushing a `v*` tag makes
`.github/workflows/ci.yml` publish self-contained single-file binaries for `linux-x64`
and `win-x64` (both cross-published from Linux) and attach them to a GitHub release; a
tag containing `-` is released as a pre-release.

CI needs `fetch-depth: 0` — MinVer reads tag history, and a shallow clone breaks it.

## Conventions

Standard .NET naming throughout — the WinForms-era Hungarian prefixes (`sPath`, `bMove`,
`oProgress`, `tb*`/`bt*`) are gone. UI strings and dialog text are German; code,
comments and commit messages are English.
