using HackApi;
using HackApi.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

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

        var forecasts = await client.GetFromJsonAsync<WeatherForecast>("/WeatherForecast/Two");

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

        builder.ConfigureTestServices(services =>
        {
            services.AddTransient<ITimeService, FakeTimeService>(_ => new FakeTimeService(new DateTime(2000, 1, 1)));
        });
    }
}

public class FakeTimeService : ITimeService
{
    private readonly DateTime _fakeTime;

    public FakeTimeService(DateTime fakeTime)
    {
        _fakeTime = fakeTime;
    }


    public DateTime GetCurrentTime()
    {
        return _fakeTime;
    }
}