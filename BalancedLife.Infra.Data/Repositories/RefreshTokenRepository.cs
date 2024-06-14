using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BalancedLife.Infra.Data.Repositories {
    public class RefreshTokenRepository : IRefreshTokenRepository {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context) {
            _context = context;
        }
        public async Task AddAsync(RefreshToken refreshToken) {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }
        public async Task<RefreshToken> GetByTokenAsync(string token) {
            return await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == token);
        }
        public async Task UpdateAsync(RefreshToken refreshToken) {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
