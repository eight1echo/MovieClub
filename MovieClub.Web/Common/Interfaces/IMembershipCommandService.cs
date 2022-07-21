namespace MovieClub.Web.Common.Interfaces;

public interface IMembershipCommandService
{
    Task CancelMembership(int clubId, int userId);
    Task CreatePendingMembership(int clubId, int userId);
}
