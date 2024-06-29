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

        [HttpGet("snack/{id}")]
        public async Task<IActionResult> GetSnackById(int id) {
            try {
                var result = await _snackService.GetSnackById(id);
                if ( result == null ) {
                    return NotFound(new { message = "Os dados do lanche não encontrado." });
                }

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível encontrar os dados do lanche, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpGet("snacks")]
        public async Task<IActionResult> GetSnacksByDate([FromBody] SnackDateRequest request) {
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

        //[HttpPost("snack")] 
        //public async Task<IActionResult> Add([FromBody] SnackDTO snack) {
        //    try {
        //        snack.Datetime = DateTime.Now;
        //        var result = await _snackService.Add(snack);
        //        if ( result != null ) {
        //            return CreatedAtAction(nameof(Add), result);
        //        }   

        //        return BadRequest(new { message = "Não foi possível registrar os dados do lanche, por favor verifique os dados!" });
        //    } catch ( Exception ex ) {
        //        return BadRequest(new { message = $"{ex.Message}" });
        //    }
        //}

        [HttpPut("snack/{id}")]
        public async Task<IActionResult> UpdateSnack([FromBody] SnackDTO snack) {
            try {
                var result = await _snackService.Update(snack);
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

    }
}
