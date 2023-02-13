using MediatR;
using SelfCare.Application.Helpers;
using SelfCare.Repository.MongoDB;

namespace SelfCare.Application.Handlers.User.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        IMongoDbRepository _mongoDbRepository;

        public LoginHandler(IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository ?? throw new ArgumentNullException(nameof(mongoDbRepository));
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _mongoDbRepository.QueryUserAsync(request.Username);

            if (response is null)
            {
                return null;
            }

            bool success = Cypher.ByteComparison(response.Password, Cypher.GenerateSaltedHash(request.Password));

            var loginResponse = new LoginResponse
            {
                Success = success,
                Token = success ? TokenService.GenerateToken(response) : null,
            };

            return loginResponse;
        }
    }
}
