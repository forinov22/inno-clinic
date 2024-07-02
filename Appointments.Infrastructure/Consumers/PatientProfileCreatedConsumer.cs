using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using InnoClinic.Contracts;
using MassTransit;

namespace Appointments.Infrastructure.Consumers;

public class PatientProfileCreatedConsumer(IUnitOfWork unitOfWork) : IConsumer<PatientProfileCreated>
{
    public async Task Consume(ConsumeContext<PatientProfileCreated> context)
    {
        var patient = new Patient()
        {
            ExternalId = context.Message.ProfileId,
            FirstName = context.Message.FirstName,
            LastName = context.Message.LastName,
            MiddleName = context.Message.MiddleName,
            Email = context.Message.Email
        };
        
        unitOfWork.PatientRepository.Add(patient);
        await unitOfWork.SaveAllAsync();
    }
}