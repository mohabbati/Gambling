using Gambling.Model;

namespace Gambling.Api.Startup;

public static class Services
{
    public static void Add(IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddGamblingDbContext(configuration);

        services.AddGamblingSwaggerGen();

        services.AddGamblingIdentity(appSettings.IdentitySettings);

        services.AddGamblingJwt(appSettings.JwtSettings);

        services.AddGamblingServices();
    }
}
