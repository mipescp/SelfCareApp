using MediatR;

namespace SelfCare.Application.Handlers.User.Signup
{
    public class SignupRequest : IRequest<SignupResponse>
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
