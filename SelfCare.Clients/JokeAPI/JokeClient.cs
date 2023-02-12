using System.Net.Http.Json;

namespace SelfCare.Clients.JokeAPI
{
    public class JokeClient : IJokeClient
    {
        private readonly HttpClient _httpClient;

        public JokeClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public Task<JokeApiResponse?> GetJokeAsync(CancellationToken cancellationToken = default)
        {
            return _httpClient.GetFromJsonAsync<JokeApiResponse>("https://v2.jokeapi.dev/joke/Programming", cancellationToken);
        }
    }
}
