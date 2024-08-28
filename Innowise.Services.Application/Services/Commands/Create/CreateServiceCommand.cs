using Innowise.Services.Application.Services.Common;
using MediatR;

namespace Innowise.Services.Application.Services.Commands.Create;

public record CreateServiceCommand(
    string ServiceName,
    decimal Price,
    bool IsActive,
    Guid ServiceCategoryId,
    Guid SpecializationId) : IRequest<ServiceResult>;