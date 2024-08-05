using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces.HttpClients.Services;
using Appointments.Domain.Entities;

namespace Appointments.Application.Extensions;

public static class MappingExtensions
{
    public static AppointmentResult ToAppointmentResult(this Appointment appointment)
    {
        return new AppointmentResult(appointment.Id,
                                     appointment.Patient.ExternalId,
                                     appointment.Patient.FirstName,
                                     appointment.Patient.LastName,
                                     appointment.Patient.MiddleName,
                                     appointment.ServiceId,
                                     appointment.Service.ServiceName,
                                     appointment.Doctor.FirstName,
                                     appointment.Doctor.LastName,
                                     appointment.Doctor.MiddleName,
                                     appointment.StartDate,
                                     appointment.StartDate.Add(
                                         TimeSpan.FromMinutes(
                                             Appointment.TimeSlotSize * appointment.Service.TimeSlotSize)));
    }

    public static ResultResult ToResultResult(this Result result)
    {
        return new ResultResult(
            result.Id,
            result.Complaints,
            result.Conclusion,
            result.Recommendations,
            result.AppointmentId,
            result.DateTime,
            result.Appointment.PatientId,
            result.Appointment.Patient.FirstName,
            result.Appointment.Patient.LastName,
            result.Appointment.Patient.MiddleName,
            result.Appointment.DoctorId,
            result.Appointment.Doctor.FirstName,
            result.Appointment.Doctor.LastName,
            result.Appointment.Doctor.MiddleName,
            result.Appointment.ServiceId,
            result.Appointment.Service.ServiceName);
    }

    public static Service ToService(this ServiceResponse serviceResponse)
    {
        return new Service()
        {
            Id = serviceResponse.Id,
            IsActive = serviceResponse.IsActive,
            Price = serviceResponse.Price,
            ServiceCategoryId = serviceResponse.ServiceCategoryId,
            ServiceCategoryName = serviceResponse.ServiceCategoryName,
            ServiceName = serviceResponse.ServiceName,
            SpecializationId = serviceResponse.SpecializationId,
            SpecializationName = serviceResponse.SpecializationName,
            TimeSlotSize = serviceResponse.TimeSlotSize
        };
    }
}