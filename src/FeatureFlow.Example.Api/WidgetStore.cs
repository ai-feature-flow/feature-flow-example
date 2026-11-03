namespace FeatureFlow.Example.Api;

/// <summary>
/// In-memory store. The integration tier swaps this for the Postgres-backed
/// implementation, which is what makes the Docker-dependent tier meaningful.
/// </summary>
public sealed class WidgetStore
{
    /// <summary>The longest widget name the store accepts, in characters.</summary>
    public const int MaxNameLength = 100;

    private readonly object _gate = new();
    private readonly List<Widget> _widgets = new();
    private int _nextId = 1;

    public IReadOnlyList<Widget> All()
    {
        lock (_gate)
        {
            return _widgets.ToList();
        }
    }

    public Widget? Find(int id)
    {
        lock (_gate)
        {
            return _widgets.FirstOrDefault(w => w.Id == id);
        }
    }

    public Widget Add(string name)
    {
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"name must be at most {MaxNameLength} characters", nameof(name));
        }

        lock (_gate)
        {
            var widget = new Widget(_nextId++, name);
            _widgets.Add(widget);
            return widget;
        }
    }

    /// <summary>The number of widgets currently held by the store.</summary>
    public int Count()
    {
        lock (_gate)
        {
            return _widgets.Count;
        }
    }
}