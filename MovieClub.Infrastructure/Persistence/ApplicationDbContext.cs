using System.Reflection;

namespace MovieClub.Infrastructure.Persistence;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Club> Clubs => Set<Club>();
    public DbSet<Membership> Memberships => Set<Membership>();

    public DbSet<Meetup> Meetups => Set<Meetup>();
    public DbSet<Attendance> Attendance => Set<Attendance>();

    public DbSet<Movie> Movies => Set<Movie>();

    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
