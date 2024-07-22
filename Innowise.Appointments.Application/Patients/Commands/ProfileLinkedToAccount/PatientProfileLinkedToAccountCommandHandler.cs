using Appointments.Application.Interfaces;
using Appointments.Application.Patients.Commands.ProfileCreated;
using Appointments.Application.Patients.Exceptions;
using Auth.Domain.Exceptions;
using MediatR;

namespace Appointments.Application.Patients.Commands.ProfileLinkedToAccount;

public class PatientProfileLinkedToAccountCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<PatientProfileCreatedCommand>
{
    public async Task Handle(PatientProfileCreatedCommand request, CancellationToken cancellationToken)
    {
        var patient = await unitOfWork.PatientRepository.GetByIdAsync(request.ProfileId);
        if (patient is null)
        {
            throw new PatientNotFoundException();
        }

        patient.Email = request.Email;

        await unitOfWork.SaveAllAsync();
    }
}