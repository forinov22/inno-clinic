using MediatR;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;
using Profiles.Application.Patients.Common;
using Profiles.Application.Patients.Exceptions;

namespace Profiles.Application.Patients.Queries.GetById;

internal class GetPatientProfileByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPatientProfileByIdQuery, PatientResult>
{
    public async Task<PatientResult> Handle(GetPatientProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var patientProfile = await unitOfWork.PatientRepository.GetByIdAsync(request.PatientId);
        if (patientProfile == null)
        {
            throw new PatientNotFoundException();
        }

        return patientProfile.ToPatientResult();
    }
}