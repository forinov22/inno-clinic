using Appointments.Application.Interfaces;
using Appointments.Application.Patients.Exceptions;
using MediatR;

namespace Appointments.Application.Patients.Commands.ProfileLinkedToAccount;

public class PatientProfileLinkedToAccountCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<PatientProfileLinkedToAccountCommand>
{
    public async Task Handle(PatientProfileLinkedToAccountCommand request, CancellationToken cancellationToken)
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