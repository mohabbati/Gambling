using Microsoft.EntityFrameworkCore;

namespace Gambling.Data;

public class GamblingDbContext : DbContext
{
    public GamblingDbContext(DbContextOptions<GamblingDbContext> options)
        : base(options)
    {
    }
}
