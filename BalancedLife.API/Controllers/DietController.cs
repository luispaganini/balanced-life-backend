using BalancedLife.Application.DTOs.Diet;
using BalancedLife.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BalancedLife.API.Controllers {
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DietController : ControllerBase {
        private readonly IDietService _dietService;

        public DietController(IDietService dietService) {
            _dietService = dietService;
        }

        [HttpPost("diet")]
        public async Task<IActionResult> CreateDiet([FromBody] DietDTO diet) {
            try {
                var response  = await _dietService.AddDiet(diet);
                return Ok(response);
            } catch ( Exception e ) {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("diet/{id}")]
        public async Task<IActionResult> GetDietById(int id) {
            try {
                var response = await _dietService.GetDietById(id);
                return Ok(response);
            } catch (Exception e ) {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpGet("diet/patient/{id}")]
        public async Task<IActionResult> GetDietsByPatient(long id) {
            try {
                var idNutritionist = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

                if ( string.IsNullOrEmpty(idNutritionist) )
                    return Unauthorized();

                var response = await _dietService.GetDietsByPatient(id, long.Parse(idNutritionist));
                return Ok(response);
            } catch ( Exception e ) {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpGet("diet/day")]
        public async Task<IActionResult> GetDietsByDay([FromQuery] DateTime date, [FromQuery] long idPatient) {
            var response = await _dietService.GetDietsByDay(date, idPatient);
            return Ok(response);
        }

        [HttpPut("diet/{id}")]
        public async Task<IActionResult> UpdateDiet([FromBody] DietDTO diet, int id) {
            try {
                diet.Id = id;
                var response = await _dietService.UpdateDiet(diet);
                return Ok(response);
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("diet/{id}")]
        public async Task<IActionResult> DeleteDiet(int id) {
            try {
                var response = await _dietService.DeleteDiet(id);

                if ( !response )
                    return NotFound(new { message = "Dieta não encontrada" });

                return Ok();
            } catch ( Exception e ) {
                return BadRequest(new { message = "Não foi possível remover a dieta" });
            }   
        }
    }
}
