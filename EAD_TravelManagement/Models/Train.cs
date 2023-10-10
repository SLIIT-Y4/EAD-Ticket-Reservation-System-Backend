using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace EAD_TravelManagement.Models
{
    public class Train
    {
        [BsonId(IdGenerator = typeof(CustomIdGenerator))]
        [BsonRepresentation(BsonType.String)]
        public string? TrainId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string StartPoint { get; set; } = null!;

        [Required]
        public string Destination { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        public string RouteCategory { get; set; } = null!;
    }
}
