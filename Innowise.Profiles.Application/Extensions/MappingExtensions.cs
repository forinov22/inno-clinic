using Profiles.Application.Doctors.Common;
using Profiles.Application.Patients.Common;
using Profiles.Application.Receptionists.Common;
using Profiles.Domain.Entities;

namespace Profiles.Application.Extensions;

public static class MappingExtensions
{
    public static DoctorResult ToDoctorResult(this Doctor doctor)
    {
        return new DoctorResult(
            doctor.Id,
            doctor.FirstName,
            doctor.LastName,
            doctor.MiddleName,
            doctor.DateOfBirth,
            doctor.WorkerStatus.ToString(),
            doctor.CareerStartYear,
            doctor.PhotoUrl,
            doctor.AccountId,
            doctor.OfficeId,
            doctor.OfficeAddress,
            doctor.SpecializationId,
            doctor.SpecializationName);
    }

    public static PatientResult ToPatientResult(this Patient patient)
    {
        return new PatientResult(patient.Id, patient.FirstName, patient.LastName, patient.MiddleName,
            patient.DateOfBirth, patient.IsLinkedToAccount, patient.AccountId);
    }

    public static ReceptionistResult ToReceptionistResult(this Receptionist receptionist)
    {
        return new ReceptionistResult(receptionist.Id, receptionist.FirstName, receptionist.LastName,
            receptionist.MiddleName, receptionist.WorkerStatus.ToString(), receptionist.AccountId);
    }
}