using Innowise.Services.Application.Interfaces.Repositories;

namespace Innowise.Services.Application.Interfaces;

public interface IUnitOfWork
{
    IServiceCategoryRepository ServiceCategoryRepository { get; }
    IServiceRepository ServiceRepository { get; }
    ISpecializationRepository SpecializationRepository { get; }
    Task SaveAllAsync();
}