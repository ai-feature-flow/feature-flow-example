# M5-LIVE-1: Reject widget names longer than 100 characters

Part of the epic on branch `ff/epic/m5-live`. This file is the ticket as the tracker would hold it.

## Ask

`POST /widgets` accepts any name that is not blank. Limit the length:

- A widget name may be at most 100 characters. Expose the limit as `WidgetStore.MaxNameLength = 100`.
- `WidgetStore.Add` throws `ArgumentException` for a name longer than the limit. A name of exactly 100 characters is accepted.
- `POST /widgets` with a name longer than the limit returns 400 with `{ "error": "name must be at most 100 characters" }`.

## Tests

Unit tests in `tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs`, tagged `[Trait("Category", "unit")]` like the existing ones:
one for a 101-character name being refused, one for a 100-character name being accepted. The unit tier proves it.
