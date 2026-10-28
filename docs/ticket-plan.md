## Design

`public int Count()` returns `_widgets.Count`.
It belongs on WidgetStore because the store owns the collection and is the
only type that may read it under its lock.
