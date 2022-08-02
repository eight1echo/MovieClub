namespace MovieClub.Infrastructure.Common.Interfaces;

public interface IMembershipCommands
{
    Task AcceptMembership(int clubId, int userId);
    Task CreatePendingMembership(int clubId, int userId);
    Task DeleteMembership(int membershipId);
}