using MediatR;
using Profiles.Application.Patients.Common;

namespace Profiles.Application.Patients.Queries.FindMatches;

public record FindPatientProfileMatchesQuery(
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly DateOfBirth) : IRequest<IEnumerable<PatientResult>>;