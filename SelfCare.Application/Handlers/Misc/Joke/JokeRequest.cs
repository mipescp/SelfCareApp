using MediatR;
using SelfCare.Api.Requests.Misc.Joke;

namespace SelfCare.Application.Handlers.Misc.Joke
{
    public class JokeRequest : IRequest<JokeResponse>
    {
        public JokeCategory Category { get; set; }
        public int Amount { get; set; }
    }

    public enum JokeCategory
    {
        Programming,
        Miscellaneous,
        Dark,
        Pun,
        Spooky,
        Christmas
    }
}
