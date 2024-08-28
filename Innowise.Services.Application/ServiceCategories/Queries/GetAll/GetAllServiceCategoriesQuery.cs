using Innowise.Services.Application.ServiceCategories.Common;
using MediatR;

namespace Innowise.Services.Application.ServiceCategories.Queries.GetAll;

public record GetAllServiceCategoriesQuery() : IRequest<IEnumerable<ServiceCategoryResult>>;