using Appointments.Application.Appointments.Commands.SendResultToEmail;
using Appointments.Application.Appointments.Commands.UpdateResult;
using Appointments.Application.Doctors.Commands.ProfileCreated;
using Appointments.Application.Patients.Commands.ProfileCreated;
using Appointments.Application.Patients.Commands.ProfileLinkedToAccount;
using InnoClinic.Contracts;

namespace Innowise.Appointments.RmqHost.Extensions;

public static class ContractsExtensions
{
    public static DoctorProfileCreatedCommand MapToCommand(this DoctorProfileCreated doctorProfileCreated)
    {
        return new DoctorProfileCreatedCommand(doctorProfileCreated.ProfileId, doctorProfileCreated.FirstName,
                                               doctorProfileCreated.LastName,
                                               doctorProfileCreated.MiddleName);
    }

    public static PatientProfileCreatedCommand MapToCommand(this PatientProfileCreated patientProfileCreated)
    {
        return new PatientProfileCreatedCommand(patientProfileCreated.ProfileId,
                                                patientProfileCreated.FirstName,
                                                patientProfileCreated.LastName,
                                                patientProfileCreated.MiddleName,
                                                patientProfileCreated.Email);
    }

    public static PatientProfileLinkedToAccountCommand MapToCommand(
        this PatientProfileLinkedToAccount patientProfileLinkedToAccount)
    {
        return new PatientProfileLinkedToAccountCommand(patientProfileLinkedToAccount.ProfileId,
                                                        patientProfileLinkedToAccount.Email);
    }

    public static SendAppointmentResultToEmailCommand MapToCommand(
        this AppointmentResultUpdated appointmentResultUpdated)
    {
        return new SendAppointmentResultToEmailCommand(appointmentResultUpdated.Email,
                                                       appointmentResultUpdated.Result);
    }
}