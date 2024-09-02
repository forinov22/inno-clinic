using MediatR;
using Profiles.Application.Doctors.Common;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;

namespace Profiles.Application.Doctors.Queries.GetAllAtWork;

internal class GetAllDoctorsAtWorkQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllDoctorsAtWorkQuery, IEnumerable<DoctorResult>>
{
    public async Task<IEnumerable<DoctorResult>> Handle(GetAllDoctorsAtWorkQuery request, CancellationToken cancellationToken)
    {
        var doctors = await unitOfWork.DoctorRepository.GetAllAtWorkAsync();
        return doctors.Select(doctor => doctor.ToDoctorResult()).ToList();
    }
}