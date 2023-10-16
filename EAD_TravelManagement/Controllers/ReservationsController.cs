/*
 * File: ReservationsController.cs
 * Author: Abeywickrama C.P.
 * Date: October 4, 2023
 * Description: This file contains the definition of the ReservationsController class, which provides various utility functions.
 */
using EAD_TravelManagement.Models;
using EAD_TravelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;


namespace EAD_TravelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationsController(ReservationService reservationService) =>
            _reservationService = reservationService;

        //Get all reservations
        [HttpGet]
        public async Task<List<Reservation>> Get() =>
            await _reservationService.GetAsync();

        //Get specific reservation
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Reservation>> Get(string id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return reservation;
        }

        //Add a reservation
        [HttpPost]
        public async Task<IActionResult> Post(Reservation newReservation)
        {
            // Calculate the maximum allowed reservation date (30 days from booking date)
            DateTime maxReservationDate = newReservation.CreateDate.AddDays(30);

            // Check if the reference already has 4 reservations
            int reservationsCount = await _reservationService.GetReservationCountByReferenceId(newReservation.ReferenceId);

            if (reservationsCount < 4 && newReservation.ReservationDate <= maxReservationDate)
            {
                await _reservationService.CreateAsync(newReservation);
                return CreatedAtAction(nameof(Get), new { id = newReservation.BookingId }, newReservation);
            }
            else if (reservationsCount >= 4)
            {
                return BadRequest("This reference ID has already 4 reservations.");
            }
            else
            {
                return BadRequest("Reservation date is exceeded more than 30 days from the booking date.");
            }
        }
        

        //Update a specific reservation
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Reservation updatedReservation)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            // Calculate the minimum allowed reservation date (5 days from today)
            DateTime minReservationDate = DateTime.Today.AddDays(5);

            // Check if the reservation date is at least 5 days in the future
            if (updatedReservation.ReservationDate >= minReservationDate)
            {
                updatedReservation.BookingId = reservation.BookingId;
                await _reservationService.UpdateAsync(id, updatedReservation);

                return Ok(updatedReservation);
            }
            else
            {
                return BadRequest("Updated reservation is not in 5 days before the reservation date.");
            }
        }


        //Delete a specific reservation
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            await _reservationService.RemoveAsync(id);

            return Ok("Successfully Deleted");
        }

        //cancel a reservation
        [HttpPut("cancelReservation/{id:length(24)}")]
        public async Task<IActionResult> CancelReservation(string id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            // Calculate the minimum allowed reservation date (5 days from today)
            DateTime minReservationDate = DateTime.Today.AddDays(5);

            // Check if the reservation date is at least 5 days in the future
            if (reservation.ReservationDate >= minReservationDate)
            {
                reservation.ReservationStatus = false;

                await _reservationService.UpdateAsync(id, reservation);

                return Ok(reservation);
            }
            else
            {
                return BadRequest("Reservation can only be cancelled if it's at least 5 days before the reservation date.");
            }
        }

        //find reservations history based on nic,date
        [HttpGet("ReservationHistory")]
        public async Task<IActionResult> GetReservationHistory(string userNIC, DateTime day)
        {
            var reservationHistory = await _reservationService.GetHistoryAsync(userNIC, day);

            if (reservationHistory != null && reservationHistory.Any())
            {
                return Ok(reservationHistory);
            }
            else
            {
                return NotFound("No reservations found.");
            }
        }

        //find upcoming reservations based on nic,date
        [HttpGet("UpcomingReservation")]
        public async Task<IActionResult> GetUpcomingReservations(string userNIC, DateTime day)
        {
            var upcomingReservation = await _reservationService.GetUpcomingResAsync(userNIC, day);

            if (upcomingReservation != null && upcomingReservation.Any())
            {
                return Ok(upcomingReservation);
            }
            else
            {
                return NotFound("No reservations found.");
            }
        }

    }
}
