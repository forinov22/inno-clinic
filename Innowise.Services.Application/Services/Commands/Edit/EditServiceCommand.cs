using Innowise.Services.Application.Services.Common;
using MediatR;

namespace Innowise.Services.Application.Services.Commands.Edit;

public record EditServiceCommand(
    Guid ServiceId,
    string ServiceName,
    decimal Price,
    bool IsActive,
    Guid ServiceCategoryId,
    Guid SpecializationId) : IRequest<ServiceResult>;