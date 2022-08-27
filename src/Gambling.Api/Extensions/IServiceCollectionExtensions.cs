using Gambling.Data;
using Gambling.Model.Account;
using Microsoft.AspNetCore.Identity;

namespace Gambling.Api.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddGamblingIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = 3;
        }).AddEntityFrameworkStores<GamblingDbContext>().AddDefaultTokenProviders();
    }
}
