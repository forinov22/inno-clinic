using MediatR;
using Profiles.Application.Doctors.Common;

namespace Profiles.Application.Doctors.Queries.GetById;

public record GetDoctorProfileByIdQuery(Guid DoctorId) : IRequest<DoctorResult>;