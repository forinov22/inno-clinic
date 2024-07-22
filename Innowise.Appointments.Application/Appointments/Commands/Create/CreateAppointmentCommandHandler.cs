using System.Text.Json;
using Appointments.Application.Appointments.Common;
using Appointments.Application.Doctors.Exceptions;
using Appointments.Application.Extensions;
using Appointments.Application.Interfaces;
using Appointments.Application.Interfaces.HttpClients.Services;
using Appointments.Application.Patients.Exceptions;
using Appointments.Application.Services.Exceptions;
using Appointments.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Polly.Registry;

namespace Appointments.Application.Appointments.Commands.Create;

internal class CreateAppointmentCommandHandler(
    IUnitOfWork unitOfWork,
    IServiceHttpClient serviceHttpClient,
    IDistributedCache distributedCache,
    ResiliencePipelineProvider<string> pipelineProvider)
    : IRequestHandler<CreateAppointmentCommand, AppointmentResult>
{
    public async Task<AppointmentResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var doctor = await unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
        var patient = await unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        var service = await FetchService(request.ServiceId);

        if (doctor is null)
        {
            throw new DoctorNotFoundException();
        }

        if (patient is null)
        {
            throw new PatientNotFoundException();
        }

        if (service is null)
        {
            throw new ServiceNotFoundException();
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

        return appointment.ToAppointmentResult();
    }

    private async Task<Service?> FetchService(Guid serviceId)
    {
        var serviceKey = $"service-{serviceId}";
        var serviceString = await distributedCache.GetStringAsync(serviceKey);

        if (serviceString is null)
        {
            var pipeline = pipelineProvider.GetPipeline("services-client");
            var serviceResponse = await pipeline.ExecuteAsync(async (token) =>
            {
                return await serviceHttpClient.GetServiceByIdAsync(new ServiceRequest(serviceId));
            });

            await distributedCache.SetStringAsync(serviceKey, JsonSerializer.Serialize(serviceResponse));
            return serviceResponse?.ToService();
        }

        return JsonSerializer.Deserialize<Service>(serviceString);
    }
}