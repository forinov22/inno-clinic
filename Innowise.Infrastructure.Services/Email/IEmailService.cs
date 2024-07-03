namespace InnoClinic.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendEmailWithPdfAttachmentAsync(string to, string subject, string body, byte[] pdfBytes);
}