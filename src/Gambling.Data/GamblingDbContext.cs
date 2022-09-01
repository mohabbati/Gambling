using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gambling.Models.Identity;
using Gambling.Models.Game;
using Gambling.Models.Account;

namespace Gambling.Data;

public class GamblingDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public GamblingDbContext(DbContextOptions<GamblingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; } = default!;
    public virtual DbSet<AccountTransaction> AccountTransactions { get; set; } = default!;
    public virtual DbSet<Play> Plays { get; set; } = default!;
}
