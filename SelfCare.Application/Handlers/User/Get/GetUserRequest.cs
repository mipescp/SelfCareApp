using MediatR;

namespace SelfCare.Application.Handlers.User.Get
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public string Id { get; set; }
    }
}
