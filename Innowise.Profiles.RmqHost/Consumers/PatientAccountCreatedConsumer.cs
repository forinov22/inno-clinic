using Innowise.Common.Messages;
using MassTransit;
using Profiles.Application.Interfaces;
using Profiles.Domain.Entities;

namespace Innowise.Profiles.RmqHost.Consumers;

internal class PatientAccountCreatedConsumer(IUnitOfWork unitOfWork) : IConsumer<PatientAccountCreated>
{
    public async Task Consume(ConsumeContext<PatientAccountCreated> context)
    {
        var account = new Account()
        {
            ExternalId = context.Message.AccountId,
            Email = context.Message.Email,
            ActivationLink = context.Message.ActivationLink,
            IsEmailVerified = context.Message.IsEmailVerified,
            PhotoUrl = context.Message.PhotoUrl,
            PhoneNumber = context.Message.PhoneNumber,
            CreatedAt = context.Message.CreatedAt,
            UpdatedAt = context.Message.UpdatedAt,
            Role = context.Message.Role,
        };

        unitOfWork.AccountRepository.Add(account);
        await unitOfWork.SaveAllAsync();
    }
}