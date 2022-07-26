﻿namespace MovieClub.Infrastructure.Persistence.Access;

public class MeetupCommands : IMeetupCommands
{
    private readonly ApplicationDbContext _context;

    public MeetupCommands(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateMeetup(int userId, int clubId, int movieId, DateTime date, string location)
    {
        var club = await _context.Clubs.Where(c => c.Id == clubId && c.Memberships.Any(m => m.UserProfileId == userId)).FirstOrDefaultAsync();

        if (club is not null)
        {
            var meetup = new Meetup(userId, clubId, movieId, date, location);

            _context.Meetups.Add(meetup);
            await _context.SaveChangesAsync();

            return meetup.Id;
        }

        throw new Exception("User tried to create a Meetup for a Club they are not a member of.");
    }
}
