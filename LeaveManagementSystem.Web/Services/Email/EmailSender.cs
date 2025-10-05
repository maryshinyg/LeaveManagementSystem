using System.Net.Mail;

namespace LeaveManagementSystem.Web.Services.Email;

public class EmailSender(IConfiguration _configuration) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromAddress = _configuration["EmailSettings:DefaultEmailAddress"];
        var smtpServer = _configuration["EmailSettings:Server"];
        var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
        var message = new MailMessage 
        { 
            From = new MailAddress(fromAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        message.To.Add(new MailAddress(email));

        using var smtp = new SmtpClient(smtpServer, smtpPort);
        await smtp.SendMailAsync(message);
    }
}
