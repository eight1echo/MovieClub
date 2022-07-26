﻿namespace MovieClub.Domain;
public class UserProfile : BaseEntity
{
    public UserProfile(string userAccountId, string displayName)
    {
        UserAccountId = userAccountId;
        DisplayName = displayName;
        _attendance = new List<Attendance>();
        _memberships = new List<Membership>();
    }
    public string UserAccountId { get; private set; }
    public string DisplayName { get; private set; }


    private readonly List<Attendance> _attendance;
    public virtual IReadOnlyCollection<Attendance> Attendance => _attendance;


    private readonly List<Membership> _memberships;
    public virtual IReadOnlyCollection<Membership> Memberships => _memberships;


    public void SetName(string newName)
    {
        DisplayName = newName;
    }
}
