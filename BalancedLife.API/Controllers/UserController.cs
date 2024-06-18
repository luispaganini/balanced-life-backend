﻿using BalancedLife.Application.DTOs;
using BalancedLife.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(int id) {
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
    }
}
