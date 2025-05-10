using System;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

class Program
{
    static async Task Main(string[] args)
    {
        var sender = new EmailSender();
        await sender.SendEmailAsync(
            to: "receiver@gmail.com",
            subject: "Test Email via MailKit",
            body: "This is a test email sent using MailKit."
        );
    }
}

public class EmailSender
{
    private string smtpHost = "smtp.gmail.com";
    private int smtpPort = 587;
    private string smtpUser = "SMTP EMAIL";
    private string smtpPass = "16 Degit Password";

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(smtpUser));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            try
            {
                await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUser, smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                Console.WriteLine("Email sent successfully via MailKit!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
        }
    }

}
