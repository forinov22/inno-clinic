using Innowise.Services.Application.Specializations.Common;
using MediatR;

namespace Innowise.Services.Application.Specializations.Queries.GetById;

public record GetSpecializationByIdQuery(Guid SpecializationId) : IRequest<SpecializationResult>;