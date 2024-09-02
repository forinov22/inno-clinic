using Innowise.Common.Messages;
using MassTransit;
using MediatR;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;
using Profiles.Application.Patients.Common;
using Profiles.Application.Patients.Exceptions;
using Profiles.Domain.Entities;

namespace Profiles.Application.Patients.Commands.Create;

internal class CreatePatientProfileCommandHandler(
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreatePatientProfileCommand, PatientResult>
{
    public async Task<PatientResult> Handle(CreatePatientProfileCommand request, CancellationToken cancellationToken)
    {
        var patientProfile = new Patient()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
        };

        if (request.AccountId is not null)
        {
            var account = await unitOfWork.AccountRepository.GetByExternalIdAsync(request.AccountId.Value);
            if (account is null)
            {
                throw new AccountNotFoundException();
            }

            patientProfile.AccountId = account.AccountId;
        }

        unitOfWork.PatientRepository.Add(patientProfile);
        await unitOfWork.SaveAllAsync();

        await publishEndpoint.Publish(new PatientProfileCreated()
        {
            ProfileId = patientProfile.Id,
            FirstName = patientProfile.FirstName,
            LastName = patientProfile.LastName,
            MiddleName = patientProfile.MiddleName,
            Email = patientProfile.Account?.Email,
        }, cancellationToken);

        return patientProfile.ToPatientResult();
    }
}