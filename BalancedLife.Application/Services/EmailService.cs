using System.Net;
using System.Net.Mail;
using System.Text;

public class EmailService : IEmailService {
    private readonly SmtpSettings _smtpSettings;

    public EmailService(SmtpSettings smtpSettings) {
        _smtpSettings = smtpSettings;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message) {
        using ( var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port) ) {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
            client.EnableSsl = true;

            var mailMessage = new MailMessage {
                From = new MailAddress(_smtpSettings.FromEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };

            mailMessage.To.Add(toEmail);

            try {
                await client.SendMailAsync(mailMessage);
            } catch ( SmtpException ex ) {
                throw new InvalidOperationException("Erro ao enviar e-mail", ex);
            }
        }
    }
}
