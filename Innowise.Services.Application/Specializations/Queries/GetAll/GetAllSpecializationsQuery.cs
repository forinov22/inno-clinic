using Innowise.Services.Application.Specializations.Common;
using MediatR;

namespace Innowise.Services.Application.Specializations.Queries.GetAll;

public record GetAllSpecializationsQuery() : IRequest<IEnumerable<SpecializationResult>>;