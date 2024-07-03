using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using Auth.Domain.Exceptions;
using InnoClinic.Services.Email;
using MediatR;

namespace Appointments.Application.Appointments.Commands.UpdateResult;

public class UpdateAppointmentResultCommandHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IPdfService pdfService) : IRequestHandler<UpdateAppointmentResultCommand, ResultResult>
{
    public async Task<ResultResult> Handle(UpdateAppointmentResultCommand request, CancellationToken cancellationToken)
    {
        var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment is null)
        {
            throw new NotFoundException("Appointment not found");
        }

        var result = await unitOfWork.ResultRepository.GetByAppointmentIdAsync(appointment.Id);

        if (result is null)
        {
            result = new Result()
            {
                Appointment = appointment,
                Complaints = request.Complaints,
                Conclusion = request.Conclusion,
                Recommendations = request.Recommendations
            };

            unitOfWork.ResultRepository.Add(result);
            
            if (appointment.Patient.Email is not null)
            {
                var pdfBytes = pdfService.GenerateResultPdf(result);

                await emailService.SendEmailWithPdfAttachmentAsync(appointment.Patient.Email, "Appointment Result",
                                                                   "<p>Your appointment result:</p>", pdfBytes);
            }
        }
        else
        {
            result.Complaints = request.Complaints;
            result.Conclusion = request.Conclusion;
            result.Recommendations = request.Recommendations;
        }

        await unitOfWork.SaveAllAsync();

        return result.MapToDto();
    }
}