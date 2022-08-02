namespace MovieClub.Web.Common.Interfaces;

public interface IMovieService
{
    Task<List<SelectListItem>> GetSelectList(string searchValue);
}