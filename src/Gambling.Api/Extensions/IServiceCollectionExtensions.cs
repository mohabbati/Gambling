using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Gambling.Data;
using Gambling.Model;
using Gambling.Model.Identity;
using Gambling.Service;
using Gambling.Service.Implemetations;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static void AddGamblingServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void AddGamblingDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GamblingDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AppDbConnectionString"), sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
    }

    public static void AddGamblingIdentity(this IServiceCollection services, IdentitySettings identitySettings)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = identitySettings.PasswordRequiredLength;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
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

    public static void AddGamblingSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}
