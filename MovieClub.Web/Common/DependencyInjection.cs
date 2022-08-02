namespace MovieClub.Web.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IClubService, ClubService>();
        services.AddTransient<IMeetupService, MeetupService>();
        services.AddTransient<IMovieService, MovieService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
