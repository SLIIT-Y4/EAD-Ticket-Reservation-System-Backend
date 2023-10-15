/*
 * File: Schedule.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the Schedule model, which provides various utility functions.
 */


using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace EAD_TravelManagement.Models
{
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ScheduleId { get; set; }

        [Required]
        public string TrainId { get; set; } = null!;

        [Required]
        public string Day { get; set; } = null!;

        [Required]
        public string StartPoint { get; set; } = null!;

        [Required]
        public string Destination { get; set; } = null!;

        [Required]
        public string DepTime { get; set; } = null!;

        [Required]
        public string ArrivalTime { get; set; } = null!;

        [Required]
        public string[] StopStations { get; set; } = null!;
        public bool ActiveStatus { get; set; } = true;
        public string? TrainName { get; set; }
    }
}
