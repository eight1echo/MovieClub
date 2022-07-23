namespace MovieClub.Web.Common.Interfaces;

public interface IMembershipCommandService
{
    Task Accept(int clubId, int userId);
    Task Cancel(int clubId, int userId);
    Task CreatePending(int clubId, int userId);
}
