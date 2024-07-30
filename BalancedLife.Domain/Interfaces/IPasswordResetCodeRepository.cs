using BalancedLife.Domain.Entities;

public interface IPasswordResetCodeRepository {
    Task AddAsync(PasswordResetCode code);
    Task<PasswordResetCode> GetByCodeAsync(string verificationCode);
    Task MarkAsUsedAsync(long id);
    Task<bool> IsValidAsync(string verificationCode);
}