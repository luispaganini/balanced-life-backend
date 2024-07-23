using BalancedLife.Application.DTOs.Snack;
using BalancedLife.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BalancedLife.API.Controllers {
    [Route("api")]
    [ApiController]
    [Authorize]
    public class SnackController : ControllerBase {
        private readonly ISnackService _snackService;

        public SnackController(ISnackService snackService) {
            _snackService = snackService;
        }

        [HttpGet("meal/{idMeal}/type-snack/{idTypeSnack}")]
        public async Task<IActionResult> GetMealById(int idMeal, int idTypeSnack) {
            try {
                var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
                var result = await _snackService.GetMealById(idMeal, idTypeSnack, int.Parse(userId));
                if ( result == null ) 
                    return NotFound(new { message = "Os dados do lanche não encontrado." });
                
                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível encontrar os dados do lanche, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpGet("snacks")]
        public async Task<IActionResult> GetSnacksByDate([FromQuery] SnackDateRequest request) {
            try {
                var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
                if ( string.IsNullOrEmpty(userId) )
                    return Unauthorized(new { message = "Usuário não autorizado." });
                
                var result = await _snackService.GetSnacksByDate(request.Date, int.Parse(userId));
                if ( result == null ) 
                    return NotFound(new { message = "Os dados dos lanches não encontrado." });
                

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível encontrar os dados dos lanches, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpPost("snack")]
        public async Task<IActionResult> AddSnack([FromBody] SnackFullDTO snack) {
            try {
                var result = await _snackService.AddSnack(snack);
                return CreatedAtAction(nameof(AddSnack), result);
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpDelete("snack/{id}")]
        public async Task<IActionResult> DeleteSnack(long id) {
            try {
                await _snackService.DeleteSnack(id);
                return Ok();
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpPut("snack/{id}")]
        public async Task<IActionResult> UpdateSnack([FromBody] SnackFullDTO snack, int id) {
            try {
                snack.Id = id;
                var result = await _snackService.UpdateSnack(snack);
                if ( result == null ) {
                    return NotFound(new { message = "Lanche não encontrado." });
                }

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível atualizar os dados do lanche, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpPut("meal/status/{id}")]
        public async Task<IActionResult> UpdateMealStatus([FromBody] MealStatusDTO mealStatus, long id) {
            try {
                await _snackService.UpdateMealStatus(id, mealStatus);

                return Ok();
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível atualizar os dados do lanche, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }
    }
}
