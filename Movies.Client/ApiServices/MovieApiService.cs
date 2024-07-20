using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            
        }
        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovie(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var httpClient = _httpClientFactory.CreateClient("MovieApiClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/movies/");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            
            response.EnsureSuccessStatusCode();            
            
            List<Movie> moviesResult = new List<Movie>();

            if (response.IsSuccessStatusCode)
            {                
                moviesResult = JsonConvert.DeserializeObject<List<Movie>>
                    (await response.Content.ReadAsStringAsync())!;
            }

            return await Task.FromResult(moviesResult);
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
