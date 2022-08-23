using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieClub.Infrastructure.External.TMDb;
using MovieClub.Infrastructure.Persistence.Access;
using MovieClub.Web.Areas.Clubs;

namespace MovieClub.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Access
        services.AddTransient<IAttendanceCommands, AttendanceCommands>();
        services.AddTransient<IClubCommands, ClubCommands>();
        services.AddTransient<IMeetupCommands, MeetupCommands>();
        services.AddTransient<IMembershipCommands, MembershipCommands>();
        services.AddTransient<IMovieCommands, MovieCommands>();
        services.AddTransient<IUserCommands, UserCommands>();

        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        // Identity
        services.AddTransient<ICurrentUserService, CurrentUserService>();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        services.AddDefaultIdentity<UserAccount>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });

        // TMDb
        services.AddHttpClient<ITMDbClient, TMDbClient>();

        return services;
    }
}
