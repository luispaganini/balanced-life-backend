using BalancedLife.Domain.Entities;
using BalancedLife.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

public class PasswordResetCodeRepository : IPasswordResetCodeRepository {
    private readonly ApplicationDbContext _context;

    public PasswordResetCodeRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task AddAsync(PasswordResetCode code) {
        await _context.PasswordResetCodes.AddAsync(code);
        await _context.SaveChangesAsync();
    }

    public async Task<PasswordResetCode> GetByCodeAsync(string verificationCode) {
        return await _context.PasswordResetCodes
            .FirstOrDefaultAsync(x => x.VerificationCode == verificationCode && !x.IsUsed);
    }

    public async Task MarkAsUsedAsync(long id) {
        var code = await _context.PasswordResetCodes.FindAsync(id);
        if ( code != null ) {
            code.IsUsed = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsValidAsync(string verificationCode) {
        var code = await GetByCodeAsync(verificationCode);
        return code != null && code.ExpiresAt > DateTime.UtcNow;
    }
}
