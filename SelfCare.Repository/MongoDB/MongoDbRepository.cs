using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SelfCare.Domain;

namespace SelfCare.Repository.MongoDB
{
    public class MongoDbRepository : IMongoDbRepository
    {
        MongoClient client;

        public MongoDbRepository(IOptions<MongoDbOptions> options)
        {
            var connectionString = options?.Value?.ConnectionString ?? throw new ArgumentNullException(nameof(options));

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            client = new MongoClient(settings);
        }

        public async Task<User> QueryUserAsync(string username)
        {
            var collection = client.GetDatabase("auth").GetCollection<User>("user");
            var filter = Builders<User>.Filter.Eq(x => x.Username, username);
            return await (await collection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var collection = client.GetDatabase("auth").GetCollection<User>("user");
            var id = (int)collection.EstimatedDocumentCount() + 1;
            await collection.InsertOneAsync(new User
            {
                DisplayName = user.DisplayName,
                Username = user.Username,
                Password = user.Password,
                Id = id
            });

            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            var result = await collection.FindAsync(filter);

            return await result.FirstAsync();
        }

        public async Task<User> QueryUserByIdAsync(int id)
        {
            var collection = client.GetDatabase("auth").GetCollection<User>("user");
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            return await (await collection.FindAsync(filter)).FirstOrDefaultAsync();
        }
    }
}
