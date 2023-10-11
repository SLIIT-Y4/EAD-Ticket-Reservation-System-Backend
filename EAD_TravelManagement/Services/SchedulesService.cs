/*
 * File: SchedulesService.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the Schedule Service, which provides various utility functions.
 */


using EAD_TravelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

        //Get all schedules
        public async Task<List<Schedule>> GetAsync() =>
            await _schedulesCollection.Find(_ => true).ToListAsync();

        //Get a particular schedule
        public async Task<Schedule?> GetAsync(string id) =>
            await _schedulesCollection.Find(x => x.ScheduleId == id).FirstOrDefaultAsync();

        //Create a schedule
        public async Task CreateAsync(Schedule newSchedule) =>
            await _schedulesCollection.InsertOneAsync(newSchedule);

        //Update a particular schedule
        public async Task UpdateAsync(string id, Schedule updatedSchedule) =>
            await _schedulesCollection.ReplaceOneAsync(x => x.ScheduleId == id, updatedSchedule);

        //Remove a particular schedule
        public async Task RemoveAsync(string id) =>
            await _schedulesCollection.DeleteOneAsync(x => x.ScheduleId == id);

        //find scheduled trains based on startPoint,stopStation,date
        public async Task<List<Schedule>> GetScheduledTrainsAsync(string startPoint, string stopStation, DateTime day)
        {
            // Load all schedules from the database
            var allSchedules = await _schedulesCollection.Find(Builders<Schedule>.Filter.Empty).ToListAsync();

            // Filter schedules to find those that match the above
            var matchingSchedules = allSchedules
                .Where(schedule =>
                    schedule.StartPoint == startPoint &&
                    schedule.Day == day &&
                    schedule.ActiveStatus &&
                    schedule.StopStations.Contains(stopStation))
                .ToList();

            return matchingSchedules;
        }
    }
}

