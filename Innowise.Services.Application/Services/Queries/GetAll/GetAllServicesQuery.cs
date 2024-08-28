using Innowise.Services.Application.Services.Common;
using MediatR;

namespace Innowise.Services.Application.Services.Queries.GetAll;

public record GetAllServicesQuery() : IRequest<IEnumerable<ServiceResult>>;