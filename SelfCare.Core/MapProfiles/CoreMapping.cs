using AutoMapper;
using SelfCare.Api.Requests.Misc.Joke;
using SelfCare.Application.Handlers.Misc.Joke;
using SelfCare.Clients.JokeAPI;

namespace SelfCare.Core.MapProfiles
{
    public class CoreMapping : Profile
    {
        public CoreMapping()
        {
            CreateMap<JokeClientRequest, JokeRequest>().ReverseMap();
            CreateMap<JokeApiResponse, JokeResponse>()
                .ForMember(dest => dest.Category,
                    src => src.MapFrom(f => Enum.Parse<JokeCategory>(f.Category)))
                .ReverseMap();
        }
    }
}