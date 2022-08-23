using Microsoft.Extensions.Configuration;
using MovieClub.Infrastructure.External.TMDb.Models;

namespace MovieClub.Infrastructure.External.TMDb;
public class TMDbClient : ITMDbClient
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public TMDbClient(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<TMDbMovie?> GetMovie(int id)
    {
        string apiKey = _config["tmdbApiKey"];
        string url = $"movie/{id}?api_key={apiKey}&append_to_response=credits";

        var tmdbMovie = await _httpClient.GetFromJsonAsync<TMDbMovie>(url);
        return tmdbMovie;
    }

    public async Task<List<TMDbSearchResult>> SearchMovies(string searchString)
    {
        List<TMDbSearchResult> movies = new();

        string apiKey = _config["tmdbApiKey"];
        string url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={searchString}";

        var response = await _httpClient.GetFromJsonAsync<TMDbSearchResponse>(url);

        if (response is not null && response.Results is not null)
            movies.AddRange(response.Results);

        return movies;
    }
}
