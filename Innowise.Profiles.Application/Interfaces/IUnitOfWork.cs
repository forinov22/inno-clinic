using Profiles.Application.Interfaces.Repositories;

namespace Profiles.Application.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    IDoctorRepository DoctorRepository { get; }
    IOfficeRepository OfficeRepository { get; }
    IPatientRepository PatientRepository { get; }
    IReceptionistRepository ReceptionistRepository { get; }
    ISpecializationRepository SpecializationRepository { get; }
    Task SaveAllAsync();
}