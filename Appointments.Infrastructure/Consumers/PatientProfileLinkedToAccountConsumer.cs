using Appointments.Application.Interfaces;
using Auth.Domain.Exceptions;
using InnoClinic.Contracts;
using MassTransit;

namespace Appointments.Infrastructure.Consumers;

public class PatientProfileLinkedToAccountConsumer(IUnitOfWork unitOfWork) : IConsumer<PatientProfileLinkedToAccount>
{
    public async Task Consume(ConsumeContext<PatientProfileLinkedToAccount> context)
    {
        var patient = await unitOfWork.PatientRepository.GetByIdAsync(context.Message.ProfileId);
        if (patient is null)
        {
            throw new NotFoundException("Patient not found");
        }
        
        patient.Email = context.Message.Email;
        
        await unitOfWork.SaveAllAsync();
    }
}