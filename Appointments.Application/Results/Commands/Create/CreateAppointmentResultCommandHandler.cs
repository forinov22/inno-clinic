using Appointments.Application.Interfaces;
using Appointments.Application.Results.Common;
using Appointments.Domain.Entities;
using Auth.Domain.Exceptions;
using InnoClinic.Services.Email;
using MediatR;

namespace Appointments.Application.Results.Commands.Create;

public class CreateAppointmentResultCommandHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IPdfService pdfService) : IRequestHandler<CreateAppointmentResultCommand, ResultResult>
{
    public async Task<ResultResult> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
    {
        var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment is null)
        {
            throw new NotFoundException("Appointment not found");
        }

        var result = new Result()
        {
            Appointment = appointment,
            Complaints = request.Complaints,
            Conclusion = request.Conclusion,
            Recommendations = request.Recommendations
        };

        unitOfWork.ResultRepository.Add(result);
        await unitOfWork.SaveAllAsync();

        if (appointment.Patient.Email is not null)
        {
            var pdfBytes = pdfService.GenerateResultPdf(result);

            await emailService.SendEmailWithPdfAttachmentAsync(appointment.Patient.Email, "Appointment Result",
                                                          "<p>Your appointment result:</p>", pdfBytes);
        }

        return ResultResult.MapFromResult(result);
    }
}