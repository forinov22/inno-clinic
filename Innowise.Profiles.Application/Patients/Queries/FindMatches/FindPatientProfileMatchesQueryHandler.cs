using MediatR;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;
using Profiles.Application.Patients.Common;

namespace Profiles.Application.Patients.Queries.FindMatches;

internal class FindPatientProfileMatchesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<FindPatientProfileMatchesQuery, IEnumerable<PatientResult>>
{
    public async Task<IEnumerable<PatientResult>> Handle(FindPatientProfileMatchesQuery request, CancellationToken cancellationToken)
    {
        var matches = await unitOfWork.PatientRepository.FindMatchesAsync(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.DateOfBirth);

        return matches.Select(match => match.ToPatientResult()).ToList();
    }
}