using Innowise.Offices.Application.Offices.Common;
using MediatR;

namespace Innowise.Offices.Application.Offices.Queries.GetById;

public record GetOfficeByIdQuery(Guid OfficeId) : IRequest<OfficeResult>;