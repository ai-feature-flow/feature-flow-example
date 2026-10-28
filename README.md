# feature-flow-example

The reference fixture repository for [Feature Flow](https://github.com/ai-feature-flow/feature-flow-poc).

It is deliberately small, and deliberately shaped like a real service:

| Piece | What it is |
|---|---|
| `src/FeatureFlow.Example.Api` | A minimal .NET 9 web API (`/health`, `/widgets`) |
| `tests/…UnitTests` | The **unit** tier — fast, no Docker, runs in the sandbox |
| `tests/…IntegrationTests` | The **integration** tier — real Postgres via Testcontainers, Docker-dependent |
| `feature-flow.yaml` | The repository manifest: tiers, execution targets, branches, merge method |
| `.github/workflows/` | The CI workflows the engine delegates Docker-dependent tiers to |

## Why two tiers

The interesting property is that the two tiers have **different execution
targets**. The unit tier runs in the sandbox; the integration tier is
Docker-dependent, so it is delegated to CI. That is the whole point: a
Docker-dependent test should run in the same environment CI would use, not in a
sandbox that merely resembles it.

```yaml
testTiers:
  - name: unit
    target: sandbox
  - name: integration
    target: ci
    ci: { instance: github-actions, workflow: integration.yml }
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
