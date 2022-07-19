using MovieClub.Web.Areas.Clubs.Services;
using MovieClub.Web.Areas.Meetups.Services;
using MovieClub.Web.Areas.Movies.Services;
using MovieClub.Web.Areas.Users.Services;

namespace MovieClub.Web.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IClubCommandService, ClubCommandService>();
        services.AddTransient<IClubQueryService, ClubQueryService>();

        services.AddTransient<IMembershipCommandService, MembershipCommandService>();
        services.AddTransient<IMembershipQueryService, MembershipQueryService>();

        services.AddTransient<IMeetupCommandService, MeetupCommandService>();
        services.AddTransient<IMeetupQueryService, MeetupQueryService>();

        services.AddTransient<IAttendanceCommandService, AttendanceCommandService>();
        services.AddTransient<IAttendanceQueryService, AttendanceQueryService>();

        services.AddTransient<IMovieCommandService, MovieCommandService>();
        services.AddTransient<IMovieQueryService, MovieQueryService>();

        services.AddTransient<IUserCommandService, UserCommandService>();
        services.AddTransient<IUserQueryService, UserQueryService>();

        services.AddTransient<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
