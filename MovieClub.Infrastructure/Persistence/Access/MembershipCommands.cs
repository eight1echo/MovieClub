﻿namespace MovieClub.Web.Areas.Clubs;

public class MembershipCommands : IMembershipCommands
{
    private readonly ApplicationDbContext _context;

    public MembershipCommands(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AcceptMembership(int membershipId)
    {
        var membership = await _context.Memberships
            .Where(m => m.Id == membershipId)
            .FirstOrDefaultAsync();

        if (membership is not null)
        {
            membership.AcceptMembership();

            var upcomingMeetups = await _context.Meetups
                .Include(m => m.Attendance)
                .Where(m => m.ClubId == membership.ClubId && m.Date > DateTime.Now)
                .ToListAsync();

            foreach (var meetup in upcomingMeetups)
            {
                meetup.InviteMember(membership.UserProfileId, meetup.Id);
            }

            await _context.SaveChangesAsync();
        }
        else
            throw new Exception("Membership could not be found.");
    }

    public async Task DeleteMembership(int membershipId)
    {
        var membership = await _context.Memberships
            .FirstOrDefaultAsync(m => m.Id == membershipId);

        if (membership is not null)
        {
            var attendance = await _context.Attendance
            .Where(a => a.UserProfileId == membership.UserProfileId && a.Meetup.ClubId == membership.ClubId)
            .Include(a => a.Meetup)
            .ToListAsync();

            if (attendance.Any())
            {
                foreach (var a in attendance.Where(a => a.Status == AttendanceStatus.Hosting))
                {
                    _context.Meetups.Remove(a.Meetup);
                }

                _context.Attendance.RemoveRange(attendance);
            }

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
        }
        else
            throw new Exception("Membership could not be found.");
    }

    public async Task CreatePendingMembership(int clubId, int userId)
    {
        var membership = await _context.Memberships
            .Where(m => m.ClubId == clubId && m.UserProfileId == userId)
            .FirstOrDefaultAsync();

        if (membership is null)
        {
            var newMembership = new Membership(clubId, userId, MembershipRank.Pending);

            await _context.Memberships.AddAsync(newMembership);
            await _context.SaveChangesAsync();
        }
        else
            throw new Exception("User already has a membership.");
    }
}