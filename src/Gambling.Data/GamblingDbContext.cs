using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gambling.Model.Identity;
using Gambling.Model.Game;
using Gambling.Model.Account;

namespace Gambling.Data;

public class GamblingDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public GamblingDbContext(DbContextOptions<GamblingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<AccountTransaction> AccountTransactions { get; set; } = default!;
    public DbSet<Play> Plays { get; set; } = default!;
}
