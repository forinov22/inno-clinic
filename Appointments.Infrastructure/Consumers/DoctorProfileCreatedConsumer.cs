using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using InnoClinic.Contracts;
using MassTransit;

namespace Appointments.Infrastructure.Consumers;

public class DoctorProfileCreatedConsumer(IUnitOfWork unitOfWork) : IConsumer<DoctorProfileCreated>
{
    public async Task Consume(ConsumeContext<DoctorProfileCreated> context)
    {
        var doctor = new Doctor()
        {
            ExternalId = context.Message.ProfileId,
            FirstName = context.Message.FirstName,
            LastName = context.Message.LastName,
            MiddleName = context.Message.MiddleName
        };
        
        unitOfWork.DoctorRepository.Add(doctor);
        await unitOfWork.SaveAllAsync();
    }
}