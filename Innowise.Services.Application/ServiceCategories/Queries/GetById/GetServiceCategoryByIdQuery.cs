using Innowise.Services.Application.ServiceCategories.Common;
using MediatR;

namespace Innowise.Services.Application.ServiceCategories.Queries.GetById;

public record GetServiceCategoryByIdQuery(Guid ServiceCategoryId) : IRequest<ServiceCategoryResult>;