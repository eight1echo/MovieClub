using MovieClub.Web.Areas.Users.Models;

namespace MovieClub.Web.Common.Interfaces;

public interface IMembershipService
{
    Task<UserMembershipsModel> GetUserMemberships(int userProfileId);
}