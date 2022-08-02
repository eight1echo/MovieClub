namespace MovieClub.Web.Areas.Clubs.Pages.Edit
{
    public class EditClubPage : PageModel
    {
        public readonly IClubCommands _clubCommands;
        public readonly IClubService _clubPages;
        private readonly ICurrentUserService _currentUser;

        public EditClubPage(IClubService clubPages, IClubCommands clubCommands, ICurrentUserService currentUser)
        {
            _clubCommands = clubCommands;
            _clubPages = clubPages;
            _currentUser = currentUser;
        }

        [BindProperty]
        public EditClubModel? PageData { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _clubPages.EditPage(userProfileId, id);

                if (PageData is null)
                    return RedirectToPage("/NotFound");

                if (PageData.UserMembership.Rank != MembershipRank.Leader)
                    return RedirectToPage("/AccessDenied");
                    
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostDeleteClub()
        {
            try
            {
                if (PageData is null)
                    return RedirectToPage("/NotFound");

                if (PageData.UserMembership.Rank != MembershipRank.Leader)
                    return RedirectToPage("/AccessDenied");

                await _clubCommands.DeleteClub(PageData.Club.Id);
                return RedirectToPage("/Profile/UserProfilePage", new { area = "Users" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostUpdateClub()
        {
            try
            {
                if (PageData is null)
                    return RedirectToPage("/NotFound");

                if (PageData.UserMembership.Rank != MembershipRank.Leader)
                    return RedirectToPage("/AccessDenied");

                await _clubCommands.UpdateClub(PageData.Club.Id, PageData.NewName);
                return RedirectToPage("/Details/ClubDetailsPage", new { id = PageData.Club.Id, area = "Clubs" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
