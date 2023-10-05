using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EAD_TravelManagement.Models
{
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ScheduleId { get; set; }
        public string TrainId { get; set; } = null!;
        public DateTime Day { get; set; }
        public string StartPoint { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string DepTime { get; set; } = null!;
        public string ArrivalTime { get; set; } = null!;
        public string[] StopStations { get; set; } = null!;
    }
}
