using EAD_TravelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EAD_TravelManagement.Services
{
    public class TrainsService
    {
        private readonly IMongoCollection<Train> _trainsCollection;

        public TrainsService(
            IOptions<TicketReservationDatabaseSettings> ticketReservationDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ticketReservationDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ticketReservationDatabaseSettings.Value.DatabaseName);

            _trainsCollection = mongoDatabase.GetCollection<Train>(
                ticketReservationDatabaseSettings.Value.TrainsCollectionName);
        }

        public async Task<List<Train>> GetAsync() =>
            await _trainsCollection.Find(_ => true).ToListAsync();

        public async Task<Train?> GetAsync(string id) =>
            await _trainsCollection.Find(x => x.TrainId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Train newTrain) =>
            await _trainsCollection.InsertOneAsync(newTrain);

        public async Task UpdateAsync(string id, Train updatedTrain) =>
            await _trainsCollection.ReplaceOneAsync(x => x.TrainId == id, updatedTrain);

        public async Task RemoveAsync(string id) =>
            await _trainsCollection.DeleteOneAsync(x => x.TrainId == id);
    }
}

