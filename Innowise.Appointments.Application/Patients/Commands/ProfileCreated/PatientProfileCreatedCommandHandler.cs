using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using MediatR;

namespace Appointments.Application.Patients.Commands.ProfileCreated;

public class PatientProfileCreatedCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<PatientProfileCreatedCommand>
{
    public async Task Handle(PatientProfileCreatedCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient()
        {
            ExternalId = request.ProfileId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Email = request.Email
        };
        
        unitOfWork.PatientRepository.Add(patient);
        await unitOfWork.SaveAllAsync();
    }
}