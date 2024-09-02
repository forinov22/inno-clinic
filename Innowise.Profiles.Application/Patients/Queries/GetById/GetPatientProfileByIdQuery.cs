using MediatR;
using Profiles.Application.Patients.Common;

namespace Profiles.Application.Patients.Queries.GetById;

public record GetPatientProfileByIdQuery(Guid PatientId) : IRequest<PatientResult>;