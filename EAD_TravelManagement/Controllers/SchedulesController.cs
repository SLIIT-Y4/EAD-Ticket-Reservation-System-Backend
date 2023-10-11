/*
 * File: SchedulesController.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the SchedulesController class, which provides various utility functions.
 */

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

        public SchedulesController(SchedulesService schedulesService, TrainsService trainsService)
        {
            _schedulesService = schedulesService;
            _trainsService = trainsService; // Initialize TrainsService
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
    }
}

