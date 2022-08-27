using Gambling.Model.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gambling.Data;

public class GamblingDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public GamblingDbContext(DbContextOptions<GamblingDbContext> options)
        : base(options)
    {
    }
}
