namespace MovieClub.Web.Common.Interfaces;

public interface IMembershipCommandService
{
    Task AcceptMembership(int clubId, int userId);
    Task CreatePending(int clubId, int userId);
    Task DeleteMembership(int membershipId);
}
