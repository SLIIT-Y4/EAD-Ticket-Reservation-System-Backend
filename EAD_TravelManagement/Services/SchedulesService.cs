/*
 * File: SchedulesService.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the Schedule Service, which provides various utility functions.
 */


using EAD_TravelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EAD_TravelManagement.Services
{
    public class SchedulesService
    {
        private readonly IMongoCollection<Schedule> _schedulesCollection;

        public SchedulesService(
            IOptions<TicketReservationDatabaseSettings> ticketReservationDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ticketReservationDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ticketReservationDatabaseSettings.Value.DatabaseName);

            _schedulesCollection = mongoDatabase.GetCollection<Schedule>(
                ticketReservationDatabaseSettings.Value.SchedulesCollectionName);
        }

        public async Task<List<Schedule>> GetAsync() =>
            await _schedulesCollection.Find(_ => true).ToListAsync();

        public async Task<Schedule?> GetAsync(string id) =>
            await _schedulesCollection.Find(x => x.ScheduleId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Schedule newSchedule) =>
            await _schedulesCollection.InsertOneAsync(newSchedule);

        public async Task UpdateAsync(string id, Schedule updatedSchedule) =>
            await _schedulesCollection.ReplaceOneAsync(x => x.ScheduleId == id, updatedSchedule);

        public async Task RemoveAsync(string id) =>
            await _schedulesCollection.DeleteOneAsync(x => x.ScheduleId == id);

        //find scheduled trains based on startPoint,stopStation,date
        public async Task<List<Schedule>> GetScheduledTrainsAsync(string startPoint, string stopStation, DateTime day)
        {
            // query to get schedules with trains
            var filter = Builders<Schedule>.Filter.Eq(x => x.StartPoint, startPoint) &
                         Builders<Schedule>.Filter.ElemMatch(x => x.StopStations, s => s == stopStation) &
                         Builders<Schedule>.Filter.Eq(x => x.Day, day) &
                         Builders<Schedule>.Filter.Eq(x => x.ActiveStatus, true);

            // Use the filter to find matching schedules with trains
            var matchingSchedules = await _schedulesCollection.Find(filter).ToListAsync();

            return matchingSchedules;
        }

    }
}

