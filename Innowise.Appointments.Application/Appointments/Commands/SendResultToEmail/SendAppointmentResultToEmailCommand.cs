using Appointments.Domain.Entities;
using MediatR;

namespace Appointments.Application.Appointments.Commands.SendResultToEmail;

public record SendAppointmentResultToEmailCommand(string Email, Result Result) : IRequest;