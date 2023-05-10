using FluentAssertions;
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
    public async Task The_user_can_get_a_weather_forecast1()
    {
        var forecast = await _systemUnderTest.GetFromJsonAsync<WeatherForecastViewModel>("/WeatherForecast/One");

        Assert.Equal("2000-01-01T00:00:00", forecast.Date);
        Assert.Equal(10, forecast.TemperatureC);
        Assert.Equal(49, forecast.TemperatureF);
        Assert.Equal("Cool", forecast.Summary);
    }

    [Fact]
    public async Task The_user_can_get_a_weather_forecast2()
    {
        var forecast = await _systemUnderTest.GetFromJsonAsync<WeatherForecastViewModel>("/WeatherForecast/One");

        forecast!.Date.Should().Be("2000-01-01T00:00:00", forecast.Date);
        forecast.TemperatureC.Should().Be(10);
        forecast.TemperatureF.Should().Be(49);
        forecast.Summary.Should().Be("Cool");
    }
}

public class WeatherForecastViewModel
{
    public WeatherForecastViewModel()
    {
        
    }
    
    public WeatherForecastViewModel(User[] users)
    {
        Users = Array.ConvertAll(users, user => new UserViewObject(user));
    }

    public string Date { get; init; }

    public int TemperatureC { get; init; }

    public int TemperatureF { get; init; }

    public string? Summary { get; init; }

    public UserViewObject[] Users { get; init; }
}

public class User
{
    public string Name { get; set; }
}

public class UserViewObject
{
    public string Name { get; init; }

    public UserViewObject(User user)
    {
        Name = user.Name;
    }
}