using MediatR;
using SelfCare.Application.Helpers;
using SelfCare.Repository.MongoDB;

namespace SelfCare.Application.Handlers.User.Signup
{
    public class SignupHandler : IRequestHandler<SignupRequest, SignupResponse>
    {
        IMongoDbRepository _mongoDbRepository;

        public SignupHandler(IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository ?? throw new ArgumentNullException(nameof(mongoDbRepository));
        }

        public async Task<SignupResponse> Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            var response = await _mongoDbRepository.CreateUserAsync(new Domain.User
            {
                DisplayName = request.DisplayName,
                Password = Cypher.GenerateSaltedHash(request.Password),
                Username = request.Username,
            });
            return new SignupResponse { Id = response.Id };
        }
    }
}
