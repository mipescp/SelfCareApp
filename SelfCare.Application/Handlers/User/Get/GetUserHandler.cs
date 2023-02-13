using MediatR;
using SelfCare.Repository.MongoDB;

namespace SelfCare.Application.Handlers.User.Get
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        IMongoDbRepository _mongoDbRepository;

        public GetUserHandler(IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository ?? throw new ArgumentNullException(nameof(mongoDbRepository));
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _mongoDbRepository.QueryUserByIdAsync(int.Parse(request.Id));

            return new GetUserResponse
            {
                DisplayName = user.DisplayName,
                Username = user.Username,
            };
        }
    }
}
