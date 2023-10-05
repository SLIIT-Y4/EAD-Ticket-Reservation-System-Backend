using EAD_TravelManagement.Models;
using EAD_TravelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAD_TravelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly TrainsService _trainsService;

        public TrainsController(TrainsService trainsService) =>
            _trainsService = trainsService;

        [HttpGet]
        public async Task<List<Train>> Get() =>
            await _trainsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Train>> Get(string id)
        {
            var train = await _trainsService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            return train;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Train newTrain)
        {
            await _trainsService.CreateAsync(newTrain);

            return CreatedAtAction(nameof(Get), new { id = newTrain.TrainId }, newTrain);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Train updatedTrain)
        {
            var train = await _trainsService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            updatedTrain.TrainId = train.TrainId;

            await _trainsService.UpdateAsync(id, updatedTrain);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var train = await _trainsService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            await _trainsService.RemoveAsync(id);

            return NoContent();
        }
    }
}

