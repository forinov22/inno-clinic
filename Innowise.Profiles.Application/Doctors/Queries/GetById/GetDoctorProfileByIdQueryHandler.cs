using MediatR;
using Profiles.Application.Doctors.Common;
using Profiles.Application.Doctors.Exceptions;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;

namespace Profiles.Application.Doctors.Queries.GetById;

internal class GetDoctorProfileByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDoctorProfileByIdQuery, DoctorResult>
{
    public async Task<DoctorResult> Handle(GetDoctorProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var doctorProfile = await unitOfWork.DoctorRepository.GetByIdAsync(request.DoctorId);
        if (doctorProfile is null)
        {
            throw new DoctorNotFoundException();
        }

        return doctorProfile.ToDoctorResult();
    }
}