using Appointments.Application.Interfaces;
using Innowise.Common.Services.Email;
using MediatR;

namespace Appointments.Application.Appointments.Commands.SendResultToEmail;

public class SendAppointmentResultToEmailCommandHandler(IPdfService pdfService, IEmailService emailService)
    : IRequestHandler<SendAppointmentResultToEmailCommand>
{
    public async Task Handle(SendAppointmentResultToEmailCommand request, CancellationToken cancellationToken)
    {
        var pdfBytes = pdfService.GenerateResultPdf(request.Result);

        await emailService.SendEmailWithPdfAttachmentAsync(request.Email, "Appointment Result",
                                                           "<p>Your appointment result:</p>", pdfBytes);
    }
}