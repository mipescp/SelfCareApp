using Newtonsoft.Json;
using SelfCare.Application.Handlers.Misc.Joke;

namespace SelfCare.Api.Requests.Misc.Joke
{
    public class JokeResponse
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("category")]
        public JokeCategory Category { get; set; }

        [JsonProperty("joke")]
        public string Joke { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}
