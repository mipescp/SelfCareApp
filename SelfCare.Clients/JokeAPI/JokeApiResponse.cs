using Newtonsoft.Json;

namespace SelfCare.Clients.JokeAPI
{
    public class JokeApiResponse
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("joke")]
        public string Joke { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("safe")]
        public bool Safe { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}