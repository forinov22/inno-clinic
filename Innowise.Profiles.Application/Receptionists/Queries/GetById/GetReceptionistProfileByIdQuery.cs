using MediatR;
using Profiles.Application.Receptionists.Common;

namespace Profiles.Application.Receptionists.Queries.GetById;

public record GetReceptionistProfileByIdQuery(Guid ReceptionistId) : IRequest<ReceptionistResult>;