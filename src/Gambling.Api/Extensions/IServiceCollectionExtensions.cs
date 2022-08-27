using Gambling.Data;
using Microsoft.EntityFrameworkCore;

namespace Gambling.Api.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddGamblingServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddGamblingDbContext(services, configuration);
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
}
