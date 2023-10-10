using EAD_TravelManagement.Models;
using EAD_TravelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAD_TravelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly SchedulesService _schedulesService;
        private readonly TrainsService _trainsService;
        private readonly ReservationService _reservationService;

        public SchedulesController(SchedulesService schedulesService, TrainsService trainsService, ReservationService reservationService)
        {
            _schedulesService = schedulesService; // Initialize SchedulesService
            _trainsService = trainsService; // Initialize TrainsService
            _reservationService = reservationService; // Initialize ReservationService
        }

        [HttpGet]
        public async Task<List<Schedule>> Get() =>
            await _schedulesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Schedule>> Get(string id)
        {
            var schedule = await _schedulesService.GetAsync(id);

            if (schedule is null)
            {
                return NotFound();
            }

            return schedule;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Schedule newSchedule)
        {
            await _schedulesService.CreateAsync(newSchedule);

            return CreatedAtAction(nameof(Get), new { id = newSchedule.ScheduleId }, newSchedule);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Schedule updatedSchedule)
        {
            var schedule = await _schedulesService.GetAsync(id);

            if (schedule is null)
            {
                return NotFound();
            }

            updatedSchedule.ScheduleId = schedule.ScheduleId;

            await _schedulesService.UpdateAsync(id, updatedSchedule);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var schedule = await _schedulesService.GetAsync(id);

            if (schedule is null)
            {
                return NotFound();
            }

            await _schedulesService.RemoveAsync(id);

            return NoContent();
        }

        //find scheduled trains based on startPoint,stopStation,date
        [HttpGet("ScheduledTrains")]
        public async Task<IActionResult> GetScheduledTrains(string startPoint, string stopStation, DateTime day)
        {
            var scheduledTrains = await _schedulesService.GetScheduledTrainsAsync(startPoint, stopStation, day);

            if (scheduledTrains != null && scheduledTrains.Any())
            {
               return Ok(scheduledTrains);
            }
            else
            {
                return NotFound("No scheduled trains found for the specified criteria.");
            }
        }

        //cancel a schedule
        [HttpPut("cancelSchedule/{id:length(24)}")]
        public async Task<IActionResult> cancelSchedule(string id)
        {
            // Check if there are any reservations to the schedule
            var reservations = await _reservationService.GetReservationsByScheduleId(id);
            
            if (reservations != null && reservations.Any())
            {
                return BadRequest("Cannot cancel a schedule with existing reservations.");
            }

            // If there are no reservations, proceed with the cancellation logic
            var schedule = await _schedulesService.GetAsync(id);

            if (schedule is null)
            {
                 return NotFound();
            }

            // setting ActiveStatus as false to cancel
            schedule.ActiveStatus = false;

            // Update the schedule to mark it as canceled
            await _schedulesService.UpdateAsync(id, schedule);

            return Ok("Schedule canceled successfully.");
        }
    }
}

