using Innowise.Services.Application.Services.Common;
using MediatR;

namespace Innowise.Services.Application.Services.Queries.GetById;

public record GetServiceByIdQuery(Guid ServiceId) : IRequest<ServiceResult>;