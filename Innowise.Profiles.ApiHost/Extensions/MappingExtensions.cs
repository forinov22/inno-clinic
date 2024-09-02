using Innowise.Profiles.Contracts.Doctors;
using Innowise.Profiles.Contracts.Patients;
using Innowise.Profiles.Contracts.Receptionists;
using Profiles.Application.Doctors.Commands.Create;
using Profiles.Application.Doctors.Common;
using Profiles.Application.Patients.Commands.Create;
using Profiles.Application.Patients.Common;
using Profiles.Application.Patients.Queries.FindMatches;
using Profiles.Application.Receptionists.Commands.Create;
using Profiles.Application.Receptionists.Common;

namespace Profiles.API.Extensions;

public static class MappingExtensions
{
    public static DoctorResponse ToDoctorResponse(this DoctorResult doctorResult)
    {
        return new DoctorResponse(doctorResult.Id, doctorResult.FirstName, doctorResult.LastName,
            doctorResult.MiddleName, doctorResult.DateOfBirth, doctorResult.WorkerStatus,
            doctorResult.CareerStartYear, doctorResult.PhotoUrl, doctorResult.AccountId,
            doctorResult.OfficeId, doctorResult.OfficeAddress.City, doctorResult.OfficeAddress.Street,
            doctorResult.OfficeAddress.HouseNumber, doctorResult.OfficeAddress.OfficeNumber,
            doctorResult.SpecializationId, doctorResult.SpecializationName);
    }

    public static CreateDoctorProfileCommand ToCreateDoctorProfileCommand(
        this CreateDoctorRequest createDoctorRequest)
    {
        return new CreateDoctorProfileCommand(createDoctorRequest.Email, createDoctorRequest.FirstName,
            createDoctorRequest.LastName, createDoctorRequest.MiddleName, createDoctorRequest.DateOfBirth,
            createDoctorRequest.Photo, createDoctorRequest.SpecializationId, createDoctorRequest.OfficeId,
            createDoctorRequest.CareerStartYear, createDoctorRequest.WorkerStatus);
    }

    public static ReceptionistResponse ToReceptionistResponse(this ReceptionistResult receptionistResult)
    {
        return new ReceptionistResponse(receptionistResult.Id, receptionistResult.FirstName,
            receptionistResult.LastName, receptionistResult.MiddleName,
            receptionistResult.WorkerStatus, receptionistResult.AccountId);
    }

    public static CreateReceptionistProfileCommand ToCreateReceptionistProfileCommand(
        this CreateReceptionistRequest createReceptionistRequest)
    {
        return new CreateReceptionistProfileCommand(createReceptionistRequest.Email,
            createReceptionistRequest.FirstName, createReceptionistRequest.LastName,
            createReceptionistRequest.MiddleName, createReceptionistRequest.WorkerStatus);
    }

    public static PatientResponse ToPatientResponse(this PatientResult patientResult)
    {
        return new PatientResponse(patientResult.Id, patientResult.FirstName, patientResult.LastName,
            patientResult.MiddleName, patientResult.DateOfBirth, patientResult.IsLinkedToAccount,
            patientResult.AccountId);
    }

    public static CreatePatientProfileCommand ToCreatePatientProfileCommand(
        this CreatePatientRequest createPatientRequest)
    {
        return new CreatePatientProfileCommand(createPatientRequest.FirstName, createPatientRequest.LastName,
            createPatientRequest.MiddleName, createPatientRequest.DateOfBirth,
            createPatientRequest.AccountId);
    }

    public static FindPatientProfileMatchesQuery ToFindPatientProfileMatchesQuery(
        this ProfileMatchesRequest profileMatchesRequest)
    {
        return new FindPatientProfileMatchesQuery(profileMatchesRequest.FirstName,
            profileMatchesRequest.LastName, profileMatchesRequest.MiddleName,
            profileMatchesRequest.DateOfBirth);
    }
}