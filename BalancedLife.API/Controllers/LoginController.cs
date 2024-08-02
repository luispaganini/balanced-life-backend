using BalancedLife.Application.DTOs;
using BalancedLife.Application.DTOs.User;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BalancedLife.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration) {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] LoginDTO request) {
            try {
                if ( string.IsNullOrEmpty(request.Password) )
                    return BadRequest(new { message = "Senha é obrigatória." });

                var result = await _loginService.Login(request.Cpf, request.Password);

                if ( result != null )
                    return Ok(await GenerateTokensAsync(result));

                return Unauthorized();
            } catch ( Exception ex ) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login/refresh")]
        public async Task<ActionResult<AuthenticationResultDTO>> Refresh([FromBody] RefreshTokenRequestDTO request) {
            try {
                var authResponse = await RefreshTokenAsync(request.AccessToken, request.RefreshToken);

                if ( !authResponse.Success )
                    return BadRequest(authResponse);

                return Ok(authResponse);
            } catch ( SecurityTokenException ex ) {
                return BadRequest(new AuthenticationResultDTO { Success = false, Errors = new[] { ex.Message } });
            } catch ( Exception ex ) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login/verify")]
        public async Task<ActionResult<bool>> VerifyCPF([FromBody] LoginDTO request) {
            try {
                var result = await _loginService.VerifyCPF(request.Cpf);
                return Ok(result);
            } catch ( Exception ex ) {
                return BadRequest(new { message = ex.Message });
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

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token) {
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                ValidateLifetime = false // We want to get the claims from an expired token
            };
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;

                if ( jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase) ) {
                    return principal;
                }

                throw new SecurityTokenException("Invalid token");
            } catch ( Exception ) {
                throw new SecurityTokenException("Invalid token");
            }
        }

        private async Task<AuthenticationResultDTO> RefreshTokenAsync(string accessToken, string refreshToken) {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            if ( principal == null ) {
                return new AuthenticationResultDTO { Success = false, Errors = new[] { "Invalid access token or refresh token." } };
            }

            var userId = principal.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var email = principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
            var savedRefreshToken = await _loginService.GetByTokenAsync(refreshToken);

            if ( savedRefreshToken == null || savedRefreshToken.IdUser != int.Parse(userId) || savedRefreshToken.ExpiryDate < DateTime.UtcNow || savedRefreshToken.Used || savedRefreshToken.Invalidated ) {
                return new AuthenticationResultDTO { Success = false, Errors = new[] { "Invalid refresh token." } };
            }

            savedRefreshToken.Used = true;

            await _loginService.UpdateTokenAsync(savedRefreshToken);

            var user = new UserInfoDTO {
                Id = int.Parse(userId),
                Email = email
            }; // Simplesmente cria um UserInfoDTO com o ID do usuário
            var tokens = await GenerateTokensAsync(user);

            return new AuthenticationResultDTO { Success = true, AccessToken = tokens.AccessToken, RefreshToken = tokens.RefreshToken };
        }
    }
}
