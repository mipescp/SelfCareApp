using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelfCare.Api.Requests.Misc.Joke;
using SelfCare.Application.Handlers.Misc.Joke;

namespace SelfCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MiscController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MiscController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Joke")]
        public async Task<JokeResponse> Joke([FromQuery] JokeClientRequest jokeClientRequest)
        {
            return await _mediator.Send(_mapper.Map<JokeRequest>(jokeClientRequest));
        }
    }
}
