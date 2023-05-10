using HackApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HackTest;

public class UnitTest2 : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UnitTest2(ApiWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Test1()
    {

        var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

        Assert.Equal(5, forecasts?.Length);
    }

    [Fact]
    public async Task Test2()
    {
        var forecast = await _client.GetFromJsonAsync<WeatherForecastDto>("/WeatherForecast/One");

        Assert.Equal("2000-01-01T00:00:00", forecast.Date);
        Assert.Equal(10, forecast.TemperatureC);
        Assert.Equal(49, forecast.TemperatureF);
        Assert.Equal("Cool", forecast.Summary);
    }
}

public class WeatherForecastDto
{
    public string Date { get; init; }
    
    public int TemperatureC { get; init; }

    public int TemperatureF { get; init; }

    public string? Summary { get; init;  }
}