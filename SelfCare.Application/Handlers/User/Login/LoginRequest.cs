using MediatR;

namespace SelfCare.Application.Handlers.User.Login
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
