using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainAPI.Entities;
using TrainAPI.Model;

namespace TrainAPI
{
    public class TrainService
    {
        private readonly IMongoCollection<Train> _trainCollection;

        public TrainService(IOptions<TrainDbSettings> settings)
        {
            var mongoClient = new MongoClient(
                settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.Value.DatabaseName);

            _trainCollection = mongoDatabase.GetCollection<Train>(
                settings.Value.CollectionName);
        }

        public async Task<List<Train>> GetAsync() =>
            await _trainCollection.Find(_ => true).ToListAsync();

        public async Task<Train?> GetAsync(string id) =>
            await _trainCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Train> CreateAsync(TrainModel model)
        {
            var train = new Train()
            {
                Id = Guid.NewGuid().ToString(),
                From = model.From,
                To = model.To,
                Date = model.Date,
            };
            await _trainCollection.InsertOneAsync(train);

            return train;
        }

        public async Task UpdateAsync(Train model) =>
            await _trainCollection.ReplaceOneAsync(x => x.Id == model.Id, model);

        public async Task RemoveAsync(string id) =>
            await _trainCollection.DeleteOneAsync(x => x.Id == id);
    }
}