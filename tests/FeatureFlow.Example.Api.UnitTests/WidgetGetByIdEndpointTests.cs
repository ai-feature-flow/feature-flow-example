using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

/// <summary>
/// HTTP-level tests for GET /widgets/{id}, run in-memory against the real host.
/// Each test builds its own host so that its WidgetStore starts empty.
/// </summary>
[Trait("Category", "unit")]
public sealed class WidgetGetByIdEndpointTests
{
    [Fact]
    public async Task GET_widgets_id_returns_200_with_widget_when_it_exists()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Create a widget first
        var createResponse = await client.PostAsJsonAsync("/widgets", new { name = "test widget" });
        createResponse.EnsureSuccessStatusCode();

        var createdWidget = await createResponse.Content.ReadFromJsonAsync<JsonElement>();
        var widgetId = createdWidget.GetProperty("Id").GetInt32();

        // Now get the widget by ID
        var response = await client.GetAsync($"/widgets/{widgetId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var widget = json.RootElement;
        Assert.Equal(JsonValueKind.Object, widget.ValueKind);
        Assert.Equal(widgetId, widget.GetProperty("Id").GetInt32());
        Assert.Equal("test widget", widget.GetProperty("Name").GetString());
    }

    [Fact]
    public async Task GET_widgets_id_returns_404_when_widget_does_not_exist()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/widgets/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
