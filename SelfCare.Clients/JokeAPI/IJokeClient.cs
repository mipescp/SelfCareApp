namespace SelfCare.Clients.JokeAPI
{
    public interface IJokeClient
    {
        Task<JokeApiResponse?> GetJokeAsync(CancellationToken cancellationToken = default);
    }
}