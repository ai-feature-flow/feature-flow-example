using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

/// <summary>
/// POST /widgets, called in-process through the real pipeline. No Docker and no network.
/// </summary>
[Trait("Category", "unit")]
public sealed class WidgetsEndpointTests
{
    [Fact]
    public async Task Post_refuses_a_101_character_name_with_400()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var name = new string('a', 101);

        var response = await client.PostAsJsonAsync("/widgets", new { name });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(
            "{\"error\":\"name must be at most 100 characters\"}",
            await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Post_accepts_a_100_character_name_with_201()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var name = new string('a', 100);

        var response = await client.PostAsJsonAsync("/widgets", new { name });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var widget = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal(name, widget.GetProperty("name").GetString());
    }
}
