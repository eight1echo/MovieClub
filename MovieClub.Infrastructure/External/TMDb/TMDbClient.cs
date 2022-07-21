using MovieClub.Infrastructure.External.TMDb.Models;

namespace MovieClub.Infrastructure.External.TMDb;
public class TMDbClient : ITMDbClient
{
    private readonly HttpClient _httpClient;

    public TMDbClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<TMDbMovie?> GetMovie(int id)
    {
        string apiKey = "bf314a905cb9d77922a9e0fbf0b729c2";
        string url = $"movie/{id}?api_key={apiKey}";

        var tmdbMovie = await _httpClient.GetFromJsonAsync<TMDbMovie>(url);
        return tmdbMovie;
    }

    public async Task<List<TMDbSearchResult>> SearchMovies(string searchString)
    {
        string apiKey = "bf314a905cb9d77922a9e0fbf0b729c2";
        string url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={searchString}";

        var movieResults = await _httpClient.GetFromJsonAsync<TMDbSearchResponse>(url);

        List<TMDbSearchResult> movies = new();

        if (movieResults is not null && movieResults.Results is not null)
        {
            foreach (var result in movieResults.Results)
            {
                movies.Add(result);
            }
        }

        return movies;
    }
}
