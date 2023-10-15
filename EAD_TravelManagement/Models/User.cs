/*
 * File: Users.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the User model, which provides various utility functions.
 */


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
        [RegularExpression(@"^([0-9]{9}[x|X|v|V]|[0-9]{12})$", ErrorMessage = "Please enter a valid NIC.")]
        public string NIC { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string UserRole { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "Please enter a password with at least 8 characters with one uppercase letter, one digit and one special character.")]
        public string Password { get; set; } = null!;

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public string? AccountStatus { get; set; }



    }
}
