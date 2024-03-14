using BalancedLife.Application.interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BalancedLife.Application.DTOs;
using BalancedLife.Domain.Entities;

namespace BalancedLife.API.Controllers {
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService loginService, IConfiguration configuration) {
            _userService = loginService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request) {
            try {
                var result = await _userService.Login(request.Cpf, request.Password);

                if ( result != null) 
                    return Ok(GenerateToken(result));
            
                return Unauthorized();
            } catch (Exception ex) {
                return StatusCode(500, $"Erro interno: {new { message = ex.Message }}");
            }
        }

        private object GenerateToken(UserInfoDTO user) {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
            };

            //generate private key to assign token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //generate digital assign
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //define token expiration
            var expiration = DateTime.Now.AddMinutes(Double.Parse(_configuration["Jwt:Expiration"]));

            //generate token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
