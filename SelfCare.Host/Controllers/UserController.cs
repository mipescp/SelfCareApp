using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelfCare.Api.Requests.User;
using SelfCare.Application.Handlers.User.Get;
using SelfCare.Application.Handlers.User.Login;
using SelfCare.Application.Handlers.User.Signup;
using SelfCare.Repository.MongoDB;
using System.Security.Claims;

namespace SelfCare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginClientRequest loginClientRequest)
        {

            if (loginClientRequest == null || string.IsNullOrEmpty(loginClientRequest.Username) || string.IsNullOrEmpty(loginClientRequest.Password))
            {
                return BadRequest("Bad request. Please try again");
            }

            var response = await _mediator.Send(_mapper.Map<LoginRequest>(loginClientRequest));

            if (response == null || !response.Success)
            {
                return BadRequest("Bad credentials. Please try again");
            }

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Signup")]
        public async Task<SignupResponse> SignUp([FromBody] SignupClientRequest signupClientRequest)
        {
            return await _mediator.Send(_mapper.Map<SignupRequest>(signupClientRequest));
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> GetUserById([FromRoute] GetUserClientRequest getUserClientRequest)
        {
            // Introspect token to get userId added to Identity claims
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier));

            if (claims == null)
            {
                return NotFound();
            }

            return Ok(await _mediator.Send(new GetUserRequest
            {
                Id = claims.Value
            }));
        }
    }
}
