using System.Globalization;

namespace FeatureFlow.Example.Api;

/// <summary>
/// In-memory store. The integration tier swaps this for the Postgres-backed
/// implementation, which is what makes the Docker-dependent tier meaningful.
/// </summary>
public sealed class WidgetStore
{
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
        lock (_gate)
        {
            var widget = new Widget(_nextId++, Normalise(name));
            _widgets.Add(widget);
            return widget;
        }
    }

    private static string Normalise(string name) =>
        name.Trim().ToLower(CultureInfo.InvariantCulture);

    /// <summary>The number of widgets currently held by the store.</summary>
    public int Count()
    {
        lock (_gate)
        {
            return _widgets.Count;
        }
    }
}