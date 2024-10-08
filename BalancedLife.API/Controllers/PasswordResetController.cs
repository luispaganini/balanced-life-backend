using BalancedLife.Application.DTOs.Auth;
using BalancedLife.Application.DTOs.User;
using BalancedLife.Application.interfaces;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BalancedLife.API.Controllers {
    [ApiController]
    [Route("api")]
    public class PasswordResetController : ControllerBase {
        private readonly IPasswordResetService _passwordResetService;
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public PasswordResetController(IPasswordResetService passwordResetService, IConfiguration configuration, ILoginService loginService, IUserService userService) {
            _passwordResetService = passwordResetService;
            _configuration = configuration;
            _loginService = loginService;
            _userService = userService;
        }

        [HttpPost("password/reset-code/generate")]
        public async Task<IActionResult> GenerateResetCode([FromBody] UserResetCodeDTO request) {
            try {
                await _passwordResetService.GenerateResetCodeAsync(request.IdUser);
                return Ok(new { message = "Código de verificação gerado e enviado." });
            } catch ( Exception ex ) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("password/reset-code/verify")]
        public async Task<IActionResult> VerifyResetCode([FromBody] UserVerifyCodeDTO request) {
            var userId = await _passwordResetService.VerifyResetCodeAsync(request.VerificationCode);
            if ( userId > 0 ) {
                var getUser = await _userService.GetUserById(userId);
                if ( getUser == null )
                    return NotFound(new { message = "Usuário não encontrado." });

                return Ok(await GenerateTokensAsync(getUser));
            } else {
                return BadRequest(new { message = "Código de verificação inválido ou expirado." });
            }
        }

        private async Task<TokenResponseDTO> GenerateTokensAsync(UserInfoDTO user) {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var accessTokenExpiration = DateTime.Now.AddMinutes(Double.Parse(_configuration["Jwt:Expiration"]));

            var accessToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: accessTokenExpiration,
                signingCredentials: credentials
            );

            var refreshToken = new RefreshToken {
                Token = Guid.NewGuid().ToString(),
                IdUser = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                JwtId = accessToken.Id
            };

            await _loginService.AddRefreshToken(refreshToken);

            return new TokenResponseDTO {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
