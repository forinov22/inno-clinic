using Appointments.Application.Appointments.Commands.Create;
using Appointments.Application.Appointments.Commands.UpdateResult;
using Appointments.Application.Appointments.Common;
using Innowise.Appointments.Contracts.Appointments;

namespace Innowise.Appointments.Contracts.Extensions;

public static class MappingExtensions
{
    public static AppointmentResponse ToAppointmentResponse(this AppointmentResult appointmentResult)
    {
        return new AppointmentResponse(appointmentResult.Id, appointmentResult.PatientId,
                                       appointmentResult.PatientFirstName, appointmentResult.PatientLastName,
                                       appointmentResult.PatientMiddleName, appointmentResult.ServiceId,
                                       appointmentResult.ServiceName, appointmentResult.DoctorFirstName,
                                       appointmentResult.DoctorLastName, appointmentResult.DoctorMiddleName,
                                       appointmentResult.DateStart, appointmentResult.DateEnd);
    }

    public static ResultResponse ToResultResponse(this ResultResult resultResult)
    {
        return new ResultResponse(resultResult.Id, resultResult.Complaints, resultResult.Recommendations,
                                  resultResult.AppointmentId, resultResult.DateTime, resultResult.PatientId,
                                  resultResult.PatientFirstName, resultResult.PatientLastName,
                                  resultResult.PatientMiddleName, resultResult.DoctorId,
                                  resultResult.DoctorFirstName, resultResult.DoctorLastName,
                                  resultResult.DoctorMiddleName, resultResult.ServiceId,
                                  resultResult.ServiceName);
    }

    public static CreateAppointmentCommand ToCreateAppointmentCommand(
        this CreateAppointmentRequest createAppointmentContract)
    {
        return new CreateAppointmentCommand(createAppointmentContract.PatientId,
                                            createAppointmentContract.DoctorId,
                                            createAppointmentContract.ServiceId,
                                            createAppointmentContract.StartDate);
    }

    public static UpdateAppointmentResultCommand ToUpdateAppointmentResultCommand(
        this UpdateAppointmentResultRequest updateAppointmentResultContract, Guid appointmentId)
    {
        return new UpdateAppointmentResultCommand(appointmentId, updateAppointmentResultContract.Complaints,
                                                  updateAppointmentResultContract.Conclusion,
                                                  updateAppointmentResultContract.Recommendations);
    }
}