using SelfCare.Domain;

namespace SelfCare.Repository.MongoDB
{
    public interface IMongoDbRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> QueryUserAsync(string username);
        Task<User> QueryUserByIdAsync(int id);
    }
}