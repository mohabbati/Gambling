using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Gambling.Data;
using Gambling.Model;
using Gambling.Model.Account;
using Gambling.Service;
using Gambling.Service.Implemetations;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static void AddAllGamblingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

        AddGamblingDbContext(services, configuration);
        AddGamblingIdentity(services, appSettings.IdentitySettings);
        AddGamblingJwt(services, appSettings.JwtSettings);
    }

    private static void AddGamblingDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GamblingDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AppDbConnectionString"), sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
    }

    private static void AddGamblingIdentity(IServiceCollection services, IdentitySettings identitySettings)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = identitySettings.PasswordRequiredLength;
        }).AddEntityFrameworkStores<GamblingDbContext>().AddDefaultTokenProviders();
    }

    public static void AddGamblingJwt(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddScoped<IJwtService, JwtService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var access_token = context.Request.Cookies["access_token"];

                    if (string.IsNullOrEmpty(access_token))
                    {
                        access_token = context.Request.Query["access_token"];
                    }

                    context.Token = access_token;

                    return Task.CompletedTask;
                }
            };

            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;
        });

        services.AddAuthorization();
    }
}
