using BalancedLife.Application.DTOs;
using BalancedLife.Application.interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [HttpOptions("login/verify")]
        public IActionResult Options() {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST,OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request) {
            try {
                if (request.Password == null)
                    return Unauthorized();

                var result = await _userService.Login(request.Cpf, request.Password);

                if ( result != null) 
                    return Ok(GenerateToken(result));
            
                return Unauthorized();
            } catch (Exception ex) {
                return StatusCode(500, $"Erro interno: {new { message = ex.Message }}");
            }
        }
        [EnableCors]
        [HttpPost("login/verify")]
        public async Task<IActionResult> VerifyCPF([FromBody] LoginDTO request) {
            try {
                var result = await _userService.VerifyCPF(request.Cpf);
                return Ok(result);
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
