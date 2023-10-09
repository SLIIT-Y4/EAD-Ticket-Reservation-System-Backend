/*
 * File: TicketReservationDatabaseSettings.cs
 * Author: Abeywickrama C.P.
 * Date: October 4, 2023
 * Description: This file contains the definition of the TicketReservationDatabaseSettings, which provides various utility functions.
 */

namespace EAD_TravelManagement.Models
{
    public class TicketReservationDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string TrainsCollectionName { get; set; } = null!;
        public string SchedulesCollectionName { get; set; } = null!;


        public string ReservationsCollectionName { get; set; } = null!;

    }
}
