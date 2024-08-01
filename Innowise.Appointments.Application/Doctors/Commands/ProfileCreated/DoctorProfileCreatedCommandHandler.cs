using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using MediatR;

namespace Appointments.Application.Doctors.Commands.ProfileCreated;

public class DoctorProfileCreatedCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DoctorProfileCreatedCommand>
{
    public async Task Handle(DoctorProfileCreatedCommand request, CancellationToken cancellationToken)
    {
        var doctor = new Doctor()
        {
            ExternalId = request.ProfileId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName
        };

        unitOfWork.DoctorRepository.Add(doctor);
        await unitOfWork.SaveAllAsync();
    }
}