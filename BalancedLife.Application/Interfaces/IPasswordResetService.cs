public interface IPasswordResetService {
    Task GenerateResetCodeAsync(long userId);
    Task<long> VerifyResetCodeAsync(string verificationCode);
}