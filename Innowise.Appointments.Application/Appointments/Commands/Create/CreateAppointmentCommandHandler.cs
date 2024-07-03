using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using Auth.Domain.Exceptions;
using MediatR;

namespace Appointments.Application.Appointments.Commands.Create;

internal class CreateAppointmentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAppointmentCommand, AppointmentResult>
{
    public async Task<AppointmentResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var doctor = await unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
        var patient = await unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        var service = await unitOfWork.ServiceRepository.GetServiceByIdAsync(request.ServiceId);

        // await Task.WhenAll(doctorTask, patientTask, serviceTask);

        if (doctor is null)
        {
            throw new NotFoundException("Doctor not found");
        }

        if (patient is null)
        {
            throw new NotFoundException("Patient not found");
        }

        if (service is null)
        {
            throw new NotFoundException("Service not found");
        }

        var appointment = new Appointment()
        {
            StartDate = request.StartDate.ToUniversalTime(),
            IsApproved = false,
            Patient = patient,
            Doctor = doctor,
            Service = service
        };

        unitOfWork.AppointmentRepository.Add(appointment);
        await unitOfWork.SaveAllAsync();

        return appointment.MapToDto();
    }
}