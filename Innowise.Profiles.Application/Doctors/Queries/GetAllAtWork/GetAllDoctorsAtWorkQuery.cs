using MediatR;
using Profiles.Application.Doctors.Common;

namespace Profiles.Application.Doctors.Queries.GetAllAtWork;

public record GetAllDoctorsAtWorkQuery() : IRequest<IEnumerable<DoctorResult>>;