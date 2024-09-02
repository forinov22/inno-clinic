using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories;

public interface IReceptionistRepository
{
    Task<Receptionist?> GetByIdAsync(Guid receptionistId);
    void Add(Receptionist receptionist);
}