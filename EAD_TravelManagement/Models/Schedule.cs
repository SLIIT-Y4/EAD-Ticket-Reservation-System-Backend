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
        public DateTime Day { get; set; }

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
    }
}
