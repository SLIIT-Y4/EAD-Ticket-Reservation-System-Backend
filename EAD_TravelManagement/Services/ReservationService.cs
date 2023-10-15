/*
 * File: ReservationService.cs
 * Author: Abeywickrama C.P.
 * Date: October 4, 2023
 * Description: This file contains the definition of the ReservationService, which provides various utility functions.
 */

using EAD_TravelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EAD_TravelManagement.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;

        public ReservationService(
            IOptions<TicketReservationDatabaseSettings> ticketReservationDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ticketReservationDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ticketReservationDatabaseSettings.Value.DatabaseName);

            _reservationCollection = mongoDatabase.GetCollection<Reservation>(
                ticketReservationDatabaseSettings.Value.ReservationsCollectionName);
        }

        //Get all reservations
        public async Task<List<Reservation>> GetAsync() =>
            await _reservationCollection.Find(_ => true).ToListAsync();

        //Get specific reservation
        public async Task<Reservation?> GetAsync(string id) =>
            await _reservationCollection.Find(x => x.BookingId == id).FirstOrDefaultAsync();

        //Add a reservation
        public async Task CreateAsync(Reservation newReservation) =>
            await _reservationCollection.InsertOneAsync(newReservation);

        //Update a specific reservation
        public async Task UpdateAsync(string id, Reservation updatedReservation) =>
            await _reservationCollection.ReplaceOneAsync(x => x.BookingId == id, updatedReservation);

        //Delete a specific reservation
        public async Task RemoveAsync(string id) =>
            await _reservationCollection.DeleteOneAsync(x => x.BookingId == id);

        // get the reservation count to check if the reference already has 4 reservations
        public async Task<int> GetReservationCountByReferenceId(string referenceId)
        {
            // to count the reservations with the given referenceId in reservations, using MongoDB C# driver:

            var filter = Builders<Reservation>.Filter.Eq(r => r.ReferenceId, referenceId);
            var count = await _reservationCollection.CountDocumentsAsync(filter);

            return (int)count;
        }

        // get the reservation that have schedules 
        public async Task<List<Reservation>> GetReservationsByScheduleId(string scheduleId)
        {
            var filter = Builders<Reservation>.Filter.Eq(x => x.ScheduleId, scheduleId);
            return await _reservationCollection.Find(filter).ToListAsync();
        }

        //find reservation history based on user,date
        public async Task<List<Reservation>> GetHistoryAsync(string userNIC, DateTime day)
        {
            // Load all reservations from the database
            var allReservations = await _reservationCollection.Find(Builders<Reservation>.Filter.Empty).ToListAsync();

            // Filter reservations to find those that match the above
            var matchingReservation = allReservations
                .Where(reservation =>
                    reservation.ReservationDate < day &&
                    reservation.UserNIC == userNIC
                 )
                .ToList();

            return matchingReservation;

        }

        //find upcoming reservations based on user,date
        public async Task<List<Reservation>> GetUpcomingResAsync(string userNIC, DateTime day)
        {
            // Load all reservations from the database
            var allReservations = await _reservationCollection.Find(Builders<Reservation>.Filter.Empty).ToListAsync();

            // Filter reservations to find those that match the above
            var matchingReservation = allReservations
                .Where(reservation =>
                    reservation.ReservationDate > day &&
                    reservation.UserNIC == userNIC &&
                    reservation.ReservationStatus == true
                 )
                .ToList();

            return matchingReservation;

        }
    }
}
