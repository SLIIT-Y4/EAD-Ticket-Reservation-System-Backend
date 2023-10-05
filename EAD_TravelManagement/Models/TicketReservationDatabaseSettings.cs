﻿namespace EAD_TravelManagement.Models
{
    public class TicketReservationDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string TrainsCollectionName { get; set; } = null!;
        public string SchedulesCollectionName { get; set; } = null!;

    }
}
