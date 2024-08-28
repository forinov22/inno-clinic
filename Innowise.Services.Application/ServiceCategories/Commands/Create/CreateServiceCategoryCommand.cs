using Innowise.Services.Application.ServiceCategories.Common;
using MediatR;

namespace Innowise.Services.Application.ServiceCategories.Commands.Create;

public record CreateServiceCategoryCommand(string CategoryName, int TimeSlotSize) : IRequest<ServiceCategoryResult>;