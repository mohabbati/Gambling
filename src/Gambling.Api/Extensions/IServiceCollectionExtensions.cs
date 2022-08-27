using Gambling.Data;
using Gambling.Model.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static void AddGamblingServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddGamblingDbContext(services, configuration);
        AddGamblingIdentity(services);
    }

    private static void AddGamblingDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GamblingDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AppDbConnectionString"), sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
    }

    private static void AddGamblingIdentity(IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = 3;
        }).AddEntityFrameworkStores<GamblingDbContext>().AddDefaultTokenProviders();
    }
}
