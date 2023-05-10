using FluentAssertions;
using HackApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HackTest;

public class UnitTest2 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _systemUnderTest;

    public UnitTest2(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _systemUnderTest = _factory.CreateClient();
    }

    [Fact]
    public async Task CanGetWeatherForecast1()
    {
        var forecast = await _systemUnderTest.GetFromJsonAsync<WeatherForecastDto>("/WeatherForecast/One");

        Assert.Equal("2000-01-01T00:00:00", forecast.Date);
        Assert.Equal(10, forecast.TemperatureC);
        Assert.Equal(49, forecast.TemperatureF);
        Assert.Equal("Cool", forecast.Summary);
    }
    
    [Fact]
    public async Task CanGetWeatherForecast2()
    {
        var forecast = await _systemUnderTest.GetFromJsonAsync<WeatherForecastDto>("/WeatherForecast/One");

        forecast!.Date.Should().Be("2000-01-01T00:00:00", forecast.Date);
        forecast.TemperatureC.Should().Be(10);
        forecast.TemperatureF.Should().Be(49);
        forecast.Summary.Should().Be("Cool");
    }
}

public class WeatherForecastDto
{
    public string Date { get; init; }
    
    public int TemperatureC { get; init; }

    public int TemperatureF { get; init; }

    public string? Summary { get; init;  }
}