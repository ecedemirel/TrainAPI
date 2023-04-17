using Microsoft.AspNetCore.Mvc;
using TrainAPI.Entities;
using TrainAPI.Model;

namespace TrainAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly TrainService _trainService;

        public TrainController(TrainService trainService)
        {
            _trainService = trainService;
        }

        [HttpGet]
        public async Task<List<Train>> Get() =>
            await _trainService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetById(string id)
        {
            var train = await _trainService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            return train;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] TrainModel model)
        {
            var train = await _trainService.CreateAsync(model);

            return Ok(train);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Train model)
        {
            var train = await _trainService.GetAsync(model.Id);

            if (train is null)
            {
                return NotFound();
            }


            await _trainService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var train = await _trainService.GetAsync(id);

            if (train is null)
            {
                return NotFound();
            }

            await _trainService.RemoveAsync(train.Id!);

            return Ok();
        }
    }
}