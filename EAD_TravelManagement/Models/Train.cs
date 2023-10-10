using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EAD_TravelManagement.Models
{
    public class Train
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TrainId { get; set; }
        public string Name { get; set; } = null!;
        public string StartPoint { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string RouteCategory { get; set; } = null!;
        public bool ActiveStatus { get; set; } = true;
        public bool PublishStatus { get; set; } = true;

    }
}
