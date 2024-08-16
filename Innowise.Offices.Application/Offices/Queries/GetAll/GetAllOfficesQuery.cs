using Innowise.Offices.Application.Offices.Common;
using MediatR;

namespace Innowise.Offices.Application.Offices.Queries.GetAll;

public record GetAllOfficesQuery() : IRequest<IEnumerable<OfficeResult>>;