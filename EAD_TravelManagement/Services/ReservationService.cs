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

        public async Task<List<Reservation>> GetAsync() =>
            await _reservationCollection.Find(_ => true).ToListAsync();

        public async Task<Reservation?> GetAsync(string id) =>
            await _reservationCollection.Find(x => x.BookingId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Reservation newReservation) =>
            await _reservationCollection.InsertOneAsync(newReservation);

        public async Task UpdateAsync(string id, Reservation updatedReservation) =>
            await _reservationCollection.ReplaceOneAsync(x => x.BookingId == id, updatedReservation);

        public async Task RemoveAsync(string id) =>
            await _reservationCollection.DeleteOneAsync(x => x.BookingId == id);
    }
}
