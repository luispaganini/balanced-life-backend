using BalancedLife.Domain.Entities;
using BalancedLife.Domain.Interfaces;

public class PasswordResetService : IPasswordResetService {
    private readonly IPasswordResetCodeRepository _passwordResetCodeRepository;
    private readonly IUserInfoRepository _userRepository;
    private readonly IEmailService _emailService;

    public PasswordResetService(IPasswordResetCodeRepository passwordResetCodeRepository, IUserInfoRepository userRepository, IEmailService emailService) {
        _passwordResetCodeRepository = passwordResetCodeRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task GenerateResetCodeAsync(long userId) {
        var random = new Random();
        var verificationCode = random.Next(100000, 999999).ToString();
        var code = new PasswordResetCode {
            IdUser = userId,
            VerificationCode = verificationCode,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15),
            IsUsed = false
        };

        await _passwordResetCodeRepository.AddAsync(code);
        var user = await _userRepository.GetById(userId);

        // Enviar o código para o usuário via e-mail (ou outro método)
        var subject = "Recuperação de Senha - Balanced Life";

        var body = $@"
        <html>
        <body>
            <h2>Olá!</h2>
            <p>Recebemos uma solicitação para recuperação de senha para a sua conta no <strong>Balanced Life</strong>.</p>
            <p>Para prosseguir com a recuperação da sua senha, por favor, utilize o código de verificação abaixo:</p>
            <h3 style='background-color: #f2f2f2; padding: 10px; border: 1px solid #ddd; border-radius: 5px;'>
                {code.VerificationCode}
            </h3>
            <p>Se você não solicitou a recuperação de senha, por favor, ignore este e-mail. O código de verificação é válido por 15 minutos.</p>
            <p>Atenciosamente,<br/>Equipe Balanced Life</p>
            <hr />
            <p><small>Este é um e-mail automático, por favor, não responda.</small></p>
        </body>
        </html>";

        await _emailService.SendEmailAsync(user.Email, subject, body);
    }

    public async Task<long> VerifyResetCodeAsync(string verificationCode) {
        if ( await _passwordResetCodeRepository.IsValidAsync(verificationCode) ) {
            var code = await _passwordResetCodeRepository.GetByCodeAsync(verificationCode);
            await _passwordResetCodeRepository.MarkAsUsedAsync(code.Id);
            return code.IdUser;
        }
        return -1;
    }
}
