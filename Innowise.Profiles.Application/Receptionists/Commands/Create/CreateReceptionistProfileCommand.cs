using MediatR;
using Profiles.Application.Receptionists.Common;

namespace Profiles.Application.Receptionists.Commands.Create;

public record CreateReceptionistProfileCommand(
    string Email,
    string FirstName,
    string LastName,
    string? MiddleName,
    string WorkerStatus) : IRequest<ReceptionistResult>;