/*
 * File: TrainsController.cs
 * Author: De Silva H.L.D.P.
 * Date: October 10, 2023
 * Description: This file contains the definition of the TrainsController class, which provides various utility functions.
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
    public class TrainsController : ControllerBase
    {
        private readonly TrainsService _trainsService;

        public TrainsController(TrainsService trainsService) =>
            _trainsService = trainsService;

        //Get all trains
        [HttpGet]
        public async Task<List<Train>> Get() =>
            await _trainsService.GetAsync();

        //Get a particular train
        [HttpGet("{id:length(4)}")]
        public async Task<ActionResult<Train>> Get(string id)
        {
            var train = await _trainsService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            return train;
        }

        //Add a new train
        [HttpPost]
        public async Task<IActionResult> Post(Train newTrain)
        {
            await _trainsService.CreateAsync(newTrain);

            return CreatedAtAction(nameof(Get), new { id = newTrain.TrainId }, newTrain);
        }

        //Update a particular train
        [HttpPut("{id:length(4)}")]
        public async Task<IActionResult> Update(string id, Train updatedTrain)
        {
            var train = await _trainsService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            updatedTrain.TrainId = train.TrainId;

            await _trainsService.UpdateAsync(id, updatedTrain);

            return Ok(updatedTrain);
        }

        //Delete a particular train
        [HttpDelete("{id:length(4)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var train = await _trainsService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            await _trainsService.RemoveAsync(id);

            return Ok("Successfully Deleted");
        }
    }
}

