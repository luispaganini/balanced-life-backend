using BalancedLife.Application.DTOs;
using BalancedLife.Application.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.API.Controllers {
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost("user")]
        public async Task<IActionResult> Register([FromBody] UserDTO user) {
            try {
                var result = await _userService.Add(user);
                if ( result != null )
                    return CreatedAtAction("Register", result);

                return BadRequest();
            } catch ( Exception ex ) {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO user) {
            try {
                try {
                    var result = await _userService.Update(id, user);

                    return Ok(result);
                } catch ( DbUpdateConcurrencyException ex) {
                    return BadRequest(new { message = "Não foi possível atualizar o usuário, por favor verifique os dados!" });
                }
            } catch ( Exception ex ) {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
