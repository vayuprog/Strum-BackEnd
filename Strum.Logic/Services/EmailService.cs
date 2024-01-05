using System.Net;
using System.Net.Mail;
using Task = System.Threading.Tasks.Task;

public class EmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUser = smtpUser;
        _smtpPass = smtpPass;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MailMessage(_smtpUser, email)
        {
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        using var client = new SmtpClient(_smtpServer, _smtpPort);
        client.Credentials = new NetworkCredential(_smtpUser, _smtpPass); 
        client.EnableSsl = true; 
        await client.SendMailAsync(emailMessage);
    }
}