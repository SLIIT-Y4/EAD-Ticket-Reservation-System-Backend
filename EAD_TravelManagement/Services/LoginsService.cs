/*
 * File: LoginsService.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the Login Service, which provides various utility functions.
 */


using EAD_TravelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EAD_TravelManagement.Services
{
    public class LoginsService
    {
        private readonly IMongoCollection<Login> _loginsCollection;

        public LoginsService(
            IOptions<TicketReservationDatabaseSettings> ticketReservationDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ticketReservationDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ticketReservationDatabaseSettings.Value.DatabaseName);

            _loginsCollection = mongoDatabase.GetCollection<Login>(
                ticketReservationDatabaseSettings.Value.LoginsCollectionName);
        }

        //Register a user
        public async Task RegisterUserAsync(Login login, string password)
        {
            login.SetPassword(password);
            await _loginsCollection.InsertOneAsync(login);
        }

        //Login function
        public async Task<Login> AuthenticateAsync(string nic, string password)
        {
            var login = await _loginsCollection.Find(l => l.NIC == nic && l.Password == password).FirstOrDefaultAsync();

            if (login != null && login.VerifyPassword(password))
            {
                return login;
            }

            return null;
        }
    }
}
