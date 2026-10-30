using FeatureFlow.Example.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<WidgetStore>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { ok = true }));

app.MapGet("/widgets", (WidgetStore store) => Results.Ok(store.All()));

app.MapGet("/widgets/{id:int}", (int id, WidgetStore store) =>
    store.Find(id) is { } widget ? Results.Ok(widget) : Results.NotFound());

app.MapGet("/widgets/by-name/{name}", (string name, WidgetStore store) =>
    store.FindByName(name) is { } widget ? Results.Ok(widget) : Results.NotFound());

app.MapPost("/widgets", (WidgetCreateRequest request, WidgetStore store) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Results.BadRequest(new { error = "name is required" });
    }

    var widget = store.Add(request.Name);
    return Results.Created($"/widgets/{widget.Id}", widget);
});

app.Run();

public record Widget(int Id, string Name, DateTimeOffset CreatedAt);

public record WidgetCreateRequest(string Name);
