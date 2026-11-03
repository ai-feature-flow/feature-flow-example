using FeatureFlow.Example.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<WidgetStore>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { ok = true }));

app.MapGet("/widgets", (WidgetStore store) => Results.Ok(store.All()));

app.MapGet("/widgets/{id:int}", (int id, WidgetStore store) =>
    store.Find(id) is { } widget ? Results.Ok(widget) : Results.NotFound());

app.MapPost("/widgets", (WidgetCreateRequest request, WidgetStore store) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest(new { error = "name is required" });
    }

    if (request.Name.Length > WidgetStore.MaxNameLength)
    {
        return Results.BadRequest(new { error = $"name must be at most {WidgetStore.MaxNameLength} characters" });
    }

    var widget = store.Add(request.Name);
    return Results.Created($"/widgets/{widget.Id}", widget);
});

app.Run();

public record Widget(int Id, string Name);

public record WidgetCreateRequest(string Name);

// Makes the entry point reachable from the unit tier's WebApplicationFactory<Program>.
public partial class Program { }
