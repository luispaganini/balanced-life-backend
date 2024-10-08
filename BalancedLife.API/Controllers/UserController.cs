using BalancedLife.Application.DTOs.User;
using BalancedLife.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BalancedLife.API.Controllers {
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(long id) {
            try {
                var result = await _userService.GetUserById(id);
                if ( result == null ) {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível encontrar o usuário, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [HttpPost("user")]
        public async Task<IActionResult> Register([FromBody] UserDTO user) {
            try {
                var result = await _userService.Add(user);
                if ( result != null ) {
                    return CreatedAtAction(nameof(Register), result);
                }

                return BadRequest(new { message = "Não foi possível registrar o usuário, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [Authorize]
        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UserDTO user) {
            try {
                var result = await _userService.Update(id, user);
                if ( result == null ) {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível atualizar o usuário, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [Authorize]
        [HttpPatch("user/{id}")]
        public async Task<IActionResult> PatchUser(long id, [FromBody] Dictionary<string, object> updates) {
            try {
                var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
                if ( id != long.Parse(userId) )
                    return StatusCode(403, new { message = "Você não tem permissão para atualizar este usuário." });

                var result = await _userService.PatchUpdate(id, updates);

                if ( result == null )
                    return NotFound(new { message = "Usuário não encontrado." });

                return Ok(result);
            } catch ( DbUpdateConcurrencyException ) {
                return BadRequest(new { message = "Não foi possível atualizar o usuário, por favor verifique os dados!" });
            } catch ( Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }
    }
}
