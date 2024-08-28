using Innowise.Services.Application.Specializations.Common;
using MediatR;

namespace Innowise.Services.Application.Specializations.Commands.Create;

public record CreateSpecializationCommand(
    string SpecializationName,
    bool IsActive) : IRequest<SpecializationResult>;