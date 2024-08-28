using Innowise.Services.Application.Specializations.Common;
using MediatR;

namespace Innowise.Services.Application.Specializations.Commands.Edit;

public record EditSpecializationCommand(
    Guid SpecializationId,
    string SpecializationName,
    bool IsActive) : IRequest<SpecializationResult>;