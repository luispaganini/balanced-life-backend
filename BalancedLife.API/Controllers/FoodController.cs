using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalancedLife.API.Controllers {
    [Route("api")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService) {
            _foodService = foodService;
        }

        [HttpPost("food")]
        public async Task<IActionResult> Add([FromBody] FoodDTO food) {
            try {
                var result = await _foodService.Add(food);
                if ( result != null ) {
                    return CreatedAtAction(nameof(Add), result);
                }   

                return BadRequest(new { message = "Não foi possível registrar os dados do alimento, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpGet("food/search/{food}/{pageNumber}")]
        public async Task<IActionResult> FindFoodBySearch(string food, int pageNumber) {
            try {
                var result = await _foodService.FindFoodBySearch(food, pageNumber);
                if ( result == null )
                    return NotFound(new { message = "Alimento não encontrado." });
                

                return Ok(result);
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

    }
}
