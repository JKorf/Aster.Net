# Working on Aster.Net

For library development, use this file. For code that consumes the library, read the [Aster.Net skill](.claude/skills/aster-net/SKILL.md). [llms.txt](llms.txt) indexes the remaining documentation.

## Repository layout

- [Client interfaces](Aster.Net/Interfaces/Clients/): public endpoint signatures and XML documentation.
- [Clients](Aster.Net/Clients/): REST, WebSocket, and Shared API implementations, grouped by API and transport.
- [Models](Aster.Net/Objects/Models/), [options](Aster.Net/Objects/Options/), and [enums](Aster.Net/Enums/): wire models and configuration.
- [Unit tests](Aster.Net.UnitTests/): request/response fixtures, subscription validation, and example compilation.
- [AI-friendly examples](Examples/ai-friendly/): complete consumer programs.

## Build and validation

Run commands from the repository root. Use the SDK configured in the [build workflow](.github/workflows/dotnet.yml); targets and dependencies are defined in the [library project](Aster.Net/Aster.Net.csproj).

```sh
dotnet restore Aster.Net.sln
dotnet build Aster.Net.sln --no-restore
dotnet test Aster.Net.UnitTests/Aster.Net.UnitTests.csproj --no-build
```

Debug builds select a subset of frameworks. To check every supported target:

```sh
dotnet build Aster.Net/Aster.Net.csproj -p:BuildAllFrameworks=true
```

Run focused tests for the change. For edits to consumer examples:

```sh
dotnet test Aster.Net.UnitTests/Aster.Net.UnitTests.csproj --filter FullyQualifiedName~AiExampleCompileTests
```

That test compiles the examples; it does not execute their exchange operations or validate Markdown snippets. Integration tests are disabled by default; do not enable authenticated exchange calls as part of routine validation.

## Implementation conventions

- Use the matching client interface and a neighboring endpoint implementation as the starting point. Preserve endpoint metadata, serialization, signing, rate limits, and cancellation behavior.
- Update public interfaces and XML documentation together with implementations. Add meaningful request/response or subscription coverage when behavior changes.
- Keep Shared API adapters consistent with native endpoint behavior; verify capability support and trading modes against the implemented surface.
- Preserve serialization compatibility and source-generated JSON metadata when changing models.
- Keep changes compatible with every target in the library project; gate framework-specific APIs where required.

## AI documentation ownership

- [SKILL.md](.claude/skills/aster-net/SKILL.md) owns consumer conventions and routes to detailed skill references.
- [API surfaces](.claude/skills/aster-net/references/api-surfaces.md) owns endpoint routing; [usage](.claude/skills/aster-net/references/usage.md) owns snippets.
- Link to those files instead of repeating API guides here or in tool-specific instructions.
- Keep skill links relative. References outside the skill are checkout-only; packaged guidance must remain usable without repository sources.
- The project packs [.claude/skills](.claude/skills/) into `skills/` in NuGet. After changing its layout, inspect package entries and verify referenced resources are included.
