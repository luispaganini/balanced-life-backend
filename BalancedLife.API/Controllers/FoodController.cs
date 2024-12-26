using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
        public async Task<IActionResult> FindFoodBySearch(
            string food,
            int pageNumber,
            [FromQuery] int pageSize = 10,
            [FromQuery] string tables = null)
        {
            try {

                var tableIds = ParseTableIds(tables);
                var result = await _foodService.FindFoodBySearch(food, pageNumber, pageSize, tableIds);

                if ( result == null || !result.Any() ) 
                    return NotFound(new { message = "Alimento não encontrado." });

                return Ok(result);
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpGet("food/{id}")]
        public async Task<IActionResult> GetFoodById(int id) {
            try {
                var result = await _foodService.GetFoodById(id);
                if ( result == null )
                    return NotFound(new { message = "Alimento não encontrado." });

                return Ok(result);
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpGet("food/unit-measurement")]
        public async Task<IActionResult> GetUnitMeasurement() {
            try {
                var result = await _foodService.GetUnitsMeasurement();
                if ( result == null )
                    return NotFound(new { message = "Unidades de medida não encontradas." });

                return Ok(result);
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpGet("food/nutritional-composition")]
        public async Task<IActionResult> GetNutritionalComposition() {
            try {
                var result = await _foodService.GetNutritionalCompositions();
                if ( result == null )
                    return NotFound(new { message = "Composições nutricionais não encontradas." });

                return Ok(result);
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        /// <summary>
        /// Converte a string de tabelas em uma lista de IDs válidos.
        /// </summary>
        /// <param name="tables">String com os IDs separados por vírgulas.</param>
        /// <returns>Lista de IDs válidos ou null se houver erro de parsing.</returns>
        private List<int> ParseTableIds(string tables) {
            if ( string.IsNullOrEmpty(tables) )
                return null;

            var tableIds = tables
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?) null)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            return tableIds.Any() ? tableIds : null;
        }

    }
}
