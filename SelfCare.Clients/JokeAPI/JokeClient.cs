using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace SelfCare.Clients.JokeAPI
{
    public class JokeClient : IJokeClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<JokeApiOptions> _options;

        public JokeClient(HttpClient httpClient, IOptions<JokeApiOptions> options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Task<JokeApiResponse?> GetJokeAsync(CancellationToken cancellationToken = default)
        {
            return _httpClient.GetFromJsonAsync<JokeApiResponse>(_options.Value.Host, cancellationToken);
        }
    }
}
