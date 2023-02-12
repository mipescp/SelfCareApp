using AutoMapper;
using SelfCare.Api.Requests.Misc.Joke;
using SelfCare.Api.Requests.User;
using SelfCare.Application.Handlers.Misc.Joke;
using SelfCare.Application.Handlers.User.Login;
using SelfCare.Application.Handlers.User.Signup;
using SelfCare.Clients.JokeAPI;

namespace SelfCare.Api.Mapping
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
            CreateMap<SignupRequest, SignupClientRequest>().ReverseMap();
            CreateMap<LoginRequest, LoginClientRequest>().ReverseMap();

        }
    }
}