﻿using MovieClub.Web.Areas.Clubs.Pages.Index;
using MovieClub.Web.Areas.Home;

namespace MovieClub.Web.Areas.Clubs.Services;

public class ClubQueryService : IClubQueryService
{
    private readonly ApplicationDbContext _context;

    public ClubQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClubHomeModel> ClubHomeQuery(int userProfileId)
    {
        // Returns all Clubs where the User has an existing Membership.

        var clubs = await _context.Clubs
            .Where(c => c.Memberships.Any(m => m.UserProfileId == userProfileId))
            .Select(c => new ClubDTO
            {
                Id = c.Id,
                Name = c.Name,

                UserRank = c.Memberships
                    .Where(m => m.UserProfileId == userProfileId)
                    .Select(m => new MembershipDTO { Rank = m.Rank })
                    .First().Rank,

                Memberships = c.Memberships.Select(m => new MembershipDTO
                {
                    Rank = m.Rank,
                    UserProfile = new UserProfileDTO
                    {
                        Id = m.UserProfile.Id,
                        DisplayName = m.UserProfile.DisplayName
                    }
                }).ToList()

            }).ToListAsync();

        var model = new ClubHomeModel();

        foreach (var club in clubs)
        {
            switch (club.UserRank)
            {
                case MembershipRank.Leader:
                    model.ClubsLeader.Add(club);
                    break;
                case MembershipRank.Member:
                    model.ClubsMember.Add(club);
                    break;
                case MembershipRank.Pending:
                    model.ClubsPending.Add(club);
                    break;
                default:
                    break;
            }
        }

        return model;
    }

    public async Task<List<ClubDTO>> ClubSearch(int userProfileId, string searchValue)
    {
        // Returns existing Clubs where the User has no Membership.

        var clubs = await _context.Clubs
            .Where(c => EF.Functions.Like(c.Name, $"%{searchValue}%") && !c.Memberships.Any(m => m.UserProfileId == userProfileId))
            .Select(c => new ClubDTO
            {
                Id = c.Id,
                Name = c.Name,
                Memberships = c.Memberships.Select(m => new MembershipDTO
                {
                    Rank = m.Rank,
                    UserProfile = new UserProfileDTO
                    {
                        Id = m.UserProfile.Id,
                        DisplayName = m.UserProfile.DisplayName
                    }
                }).ToList()
            }).ToListAsync();

        return clubs;
    }
}
