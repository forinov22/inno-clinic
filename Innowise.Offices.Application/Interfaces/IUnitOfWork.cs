using Innowise.Offices.Application.Interfaces.Repositories;

namespace Innowise.Offices.Application.Interfaces;

public interface IUnitOfWork
{
    IOfficeRepository OfficeRepository { get; }
    Task SaveAllAsync();
}