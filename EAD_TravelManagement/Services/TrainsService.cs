/*
 * File: TrainsService.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the Train Service, which provides various utility functions.
 */


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

        //Get all trains
        public async Task<List<Train>> GetAsync() =>
            await _trainsCollection.Find(_ => true).ToListAsync();

        //Get a specific train
        public async Task<Train?> GetAsync(string id) =>
            await _trainsCollection.Find(x => x.TrainId == id).FirstOrDefaultAsync();

        //Add a new train
        public async Task CreateAsync(Train newTrain) =>
            await _trainsCollection.InsertOneAsync(newTrain);

        //Update a specific train
        public async Task UpdateAsync(string id, Train updatedTrain) =>
            await _trainsCollection.ReplaceOneAsync(x => x.TrainId == id, updatedTrain);

        //Delete a specific train
        public async Task RemoveAsync(string id) =>
            await _trainsCollection.DeleteOneAsync(x => x.TrainId == id);
    }
}

