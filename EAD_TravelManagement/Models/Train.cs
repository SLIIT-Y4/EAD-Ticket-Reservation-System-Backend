/*
 * File: Train.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the Train model, which provides various utility functions.
 */


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
        public bool ActiveStatus { get; set; } = true;

    }
}
