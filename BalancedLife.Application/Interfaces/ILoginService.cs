using BalancedLife.Application.DTOs;
using BalancedLife.Domain.Entities;

namespace BalancedLife.Application.Interfaces {
    public interface ILoginService {
        Task<UserInfoDTO> Login(string cpf, string password);
        Task<UserInfoDTO> VerifyCPF(string cpf);
        Task AddRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task UpdateTokenAsync(RefreshToken refreshToken);
    }
}
