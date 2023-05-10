using HackApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HackTest;

public class UnitTest1 : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _factory;

    public UnitTest1(ApiWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsFiveForecasts()
    {
        var client = _factory.CreateClient();

        var forecasts = await client.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

        Assert.NotNull(forecasts);

        if (forecasts != null)
        {
            Assert.Equal(5, forecasts.Length);
        }
    }

    [Fact]
    public async Task UsesAppSetting()
    {
        var client = _factory.CreateClient();

        var forecasts = await client.GetFromJsonAsync<WeatherForecast>("/WeatherForecast/One");

        Assert.Equal("fromtestsettings", forecasts?.Summary);
    }
}

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration Configuration { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                // AddTestKeyVaultHere
                .Build();

            config.AddConfiguration(Configuration);
        });
    }
}