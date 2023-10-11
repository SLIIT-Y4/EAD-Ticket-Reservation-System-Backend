/*
 * File: UsersServices.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the User Service, which provides various utility functions.
 */


using EAD_TravelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EAD_TravelManagement.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersService(
            IOptions<TicketReservationDatabaseSettings> ticketReservationDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ticketReservationDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ticketReservationDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                ticketReservationDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() => 
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string nic) =>
            await _usersCollection.Find(x => x.NIC == nic).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string nic, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.NIC == nic, updatedUser);

        public async Task RemoveAsync(string nic) =>
            await _usersCollection.DeleteOneAsync(x => x.NIC == nic);
    }
}
