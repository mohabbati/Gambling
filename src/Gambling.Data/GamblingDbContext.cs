using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gambling.Models.Identity;
using Gambling.Models.Game;
using Gambling.Models.Account;
using Gambling.Models.Common;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        AddSequentialGuidForIds(builder);
    }

    private void AddSequentialGuidForIds(ModelBuilder builder)
    {
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            IMutableProperty? property = entityType.GetProperties().SingleOrDefault(p => p.Name.Equals(nameof(IGamblingEntity.Id), StringComparison.OrdinalIgnoreCase));
            if (property is not null && property.ClrType == typeof(Guid))
            {
                var p = builder.Entity(entityType.ClrType).Property(property.Name);

                p.ValueGeneratedOnAdd()
                    .HasDefaultValueSql("NewSequentialID()");
            }
        }
    }
}
