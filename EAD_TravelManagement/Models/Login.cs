/*
 * File: Login.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the Login model, which provides various utility functions.
 */


using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EAD_TravelManagement.Models
{
    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string NIC { get; set; }

        public string? UserRole { get; set; }
        public string Password { get; set; }
    }
}
