using BalancedLife.Application.DTOs.Body;
using BalancedLife.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.API.Controllers {

    [Route("api")]
    [ApiController]
    [Authorize]
    public class BodyController : ControllerBase {
        private readonly IBodyService _bodyService;

        public BodyController(IBodyService bodyService) {
            _bodyService = bodyService;
        }

        [HttpGet("body/{id}")]
        public async Task<IActionResult> GetBodyById(int id) {
            try {
                var result = await _bodyService.GetBodyById(id);
                if ( result == null ) {
                    return NotFound(new { message = "Os dados do corpo não encontrado." });
                }

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível encontrar os dados do corpo, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpPost("body")]
        public async Task<IActionResult> Add([FromBody] BodyDTO body) {
            try {
                body.Datetime = DateTime.Now;
                var result = await _bodyService.Add(body);
                if ( result != null ) {
                    return CreatedAtAction(nameof(Add), result);
                }

                return BadRequest(new { message = "Não foi possível registrar os dados do corpo, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }


        [HttpPut("body/{id}")]
        public async Task<IActionResult> UpdateBody([FromBody] BodyDTO body) {
            try {
                var result = await _bodyService.Update(body);
                if ( result == null ) {
                    return NotFound(new { message = "Corpo não encontrado." });
                }

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível atualizar os dados do corpo, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpGet("body/last/{userId}")]
        public async Task<IActionResult> GetLastFourBodies(int userId) {
            try {
                var result = await _bodyService.GetLastFourBodies(userId);
                if ( result == null ) {
                    return NotFound(new { message = "Os dados do corpo não encontrado." });
                }

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível encontrar os dados do corpo, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }
    }
}
