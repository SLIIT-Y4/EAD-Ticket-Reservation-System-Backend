using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

/*
 * File: Reservation.cs
 * Author: Abeywickrama C.P.
 * Date: October 4, 2023
 * Description: This file contains the definition of the Reservation model, which provides various utility functions.
 */

namespace EAD_TravelManagement.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? BookingId { get; set; }
        public string ReferenceId { get; set; } = null!;
        public DateTime ReservationDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string TrainId { get; set; } = null!;
        public string ScheduleId { get; set; } = null!;
        public int SeatCount { get; set; } = 0;
        public string ReservationType { get; set; } = null!;
        public bool ReservationStatus { get; set;} = true;

    }
}
