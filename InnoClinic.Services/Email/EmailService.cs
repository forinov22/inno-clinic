using System.Net.Mail;

namespace InnoClinic.Services.Email;

internal class EmailService(SmtpClient smtpClient) : IEmailService
{
    private const string Sender = "forinov22@gmail.com";
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var emailMessage = new MailMessage()
        {
            From = new MailAddress(Sender),
            To = { to },
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        await smtpClient.SendMailAsync(emailMessage);
    }

    public async Task SendEmailWithPdfAttachmentAsync(string to, string subject, string body, byte[] pdfBytes)
    {
        using var emailMessage = new MailMessage();
        
        emailMessage.From = new MailAddress(Sender);
        emailMessage.To.Add(to);
        emailMessage.Subject = subject;
        emailMessage.Body = body;
        emailMessage.IsBodyHtml = true;

        using var pdfStream = new MemoryStream(pdfBytes);
        
        var pdfAttachment = new Attachment(pdfStream, "result.pdf", "application/pdf");
        emailMessage.Attachments.Add(pdfAttachment);
        
        await smtpClient.SendMailAsync(emailMessage);
    }
}