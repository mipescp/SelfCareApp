using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelfCare.Api.Requests.User;
using SelfCare.Application.Handlers.User.Login;
using SelfCare.Application.Handlers.User.Signup;
using SelfCare.Repository.MongoDB;

namespace SelfCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMongoDbRepository _mongoDbRepository;

        public UserController(IMediator mediator, IMapper mapper, IMongoDbRepository mongoDbRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._mongoDbRepository = mongoDbRepository ?? throw new ArgumentNullException(nameof(mongoDbRepository));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<LoginResponse> Login([FromQuery] LoginClientRequest loginClientRequest)
        {
            return await _mediator.Send(_mapper.Map<LoginRequest>(loginClientRequest));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Signup")]
        public async Task<SignupResponse> SignUp([FromBody] SignupClientRequest signupClientRequest)
        {
            return await _mediator.Send(_mapper.Map<SignupRequest>(signupClientRequest));
        }
    }
}
