# feature-flow-example

The reference fixture repository for [Feature Flow](https://github.com/ai-feature-flow/feature-flow-poc).

It is deliberately small, and deliberately shaped like a real service:

| Piece | What it is |
|---|---|
| `src/FeatureFlow.Example.Api` | A minimal .NET 9 web API (`/health`, `/widgets`) |
| `tests/…UnitTests` | The **unit** tier — fast, no Docker; meant for the sandbox, in CI until the sandbox image has the .NET SDK |
| `tests/…IntegrationTests` | The **integration** tier — real Postgres via Testcontainers, Docker-dependent |
| `feature-flow.yaml` | The repository manifest (Feature Flow `docs/07` section 4): tiers and where they run, analyser |
| `.github/workflows/` | The CI workflows the engine delegates Docker-dependent tiers to |

## Why two tiers

The interesting property is that the two tiers have **different execution
targets**. The unit tier is meant for the sandbox; the integration tier is
Docker-dependent, so it is delegated to CI. That is the whole point: a
Docker-dependent test should run in the same environment CI would use, not in a
sandbox that merely resembles it.

```yaml
test_tiers:
  - name: unit
    targets: [ci]            # [sandbox, ci] once the sandbox image has the .NET SDK
    ci: { job: unit.yml }
  - name: integration
    targets: [ci]
    ci: { job: integration.yml }
```

## Running it locally

```bash
dotnet restore
dotnet test tests/FeatureFlow.Example.Api.UnitTests --filter Category=unit
docker compose -f compose.integration.yaml up --abort-on-container-exit --exit-code-from integration-tests
```

The integration tier needs Docker. Under Feature Flow it does not need Docker
locally at all — the engine delegates it to GitHub Actions.

## Status

Built for **Spike 2, one real ticket run** (`docs/08` section 2.2 of the
architecture repository). It doubles as the open-source example and as an
end-to-end test.
