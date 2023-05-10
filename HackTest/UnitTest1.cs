using HackApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HackTest;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task Test1()
    {
        var client = _factory.CreateClient();

        var forecasts = await client.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

        Assert.Equal(5, forecasts?.Length);
        
    }
    [Fact]
    public async Task Test2()
    {
        var client = _factory.CreateClient();

        var forecasts = await client.GetFromJsonAsync<WeatherForecast>("/WeatherForecast/One");

        Assert.Equal(5, forecasts.TemperatureC);
        
    }
}