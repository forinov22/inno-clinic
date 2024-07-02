﻿using Appointments.Domain.Entities;

namespace Appointments.Application.Appointments.Common;

public class AppointmentResult(
    Guid id,
    Guid patientId,
    string patientFirstName,
    string patientLastName,
    string? patientMiddleName,
    Guid serviceId,
    string serviceName,
    string doctorFirstName,
    string doctorLastName,
    string? doctorMiddleName,
    DateTime dateStart,
    DateTime dateEnd)
{
    private const int TimeSlotSize = 10;

    public Guid Id { get; set; } = id;
    public Guid PatientId { get; set; } = patientId;
    public string PatientFirstName { get; set; } = patientFirstName;
    public string PatientLastName { get; set; } = patientLastName;
    public string? PatientMiddleName { get; set; } = patientMiddleName;
    public Guid ServiceId { get; set; } = serviceId;
    public string ServiceName { get; set; } = serviceName;
    public string DoctorFirstName { get; set; } = doctorFirstName;
    public string DoctorLastName { get; set; } = doctorLastName;
    public string? DoctorMiddleName { get; set; } = doctorMiddleName;
    public DateTime DateStart { get; set; } = dateStart;
    public DateTime DateEnd { get; set; } = dateEnd;

    public static AppointmentResult MapFromAppointment(Appointment appointment)
    {
        return new AppointmentResult(
            appointment.Id,
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
            appointment.StartDate.Add(TimeSpan.FromMinutes(TimeSlotSize * appointment.Service.TimeSlotSize)));
    }
}