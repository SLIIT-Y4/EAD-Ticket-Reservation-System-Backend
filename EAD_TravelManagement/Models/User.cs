using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace EAD_TravelManagement.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string NIC { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string ContactNo { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public string AccountStatus { get; set; } = null!;



    }
}
