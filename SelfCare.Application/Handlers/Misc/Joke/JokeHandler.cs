using AutoMapper;
using MediatR;
using SelfCare.Api.Requests.Misc.Joke;
using SelfCare.Clients.JokeAPI;

namespace SelfCare.Application.Handlers.Misc.Joke
{
    public class JokeHandler : IRequestHandler<JokeRequest, JokeResponse>
    {
        private readonly IJokeClient _jokeClient;
        private readonly IMapper _mapper;

        public JokeHandler(IJokeClient jokeClient, IMapper mapper)
        {
            _jokeClient = jokeClient ?? throw new ArgumentNullException(nameof(jokeClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<JokeResponse> Handle(JokeRequest request, CancellationToken cancellationToken)
        {
            var response = await _jokeClient.GetJokeAsync();
            return _mapper.Map<JokeResponse>(response);
        }
    }
}
