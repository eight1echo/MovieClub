namespace MovieClub.Web.Common.Interfaces;

public interface IMovieQueryService
{
    Task<List<SelectListItem>> MovieSelectQuery(string searchValue);
}
