using BalancedLife.Domain.Entities;

namespace BalancedLife.Domain.Interfaces {
    public interface IRefreshTokenRepository {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task UpdateAsync(RefreshToken refreshToken);
    }
}
