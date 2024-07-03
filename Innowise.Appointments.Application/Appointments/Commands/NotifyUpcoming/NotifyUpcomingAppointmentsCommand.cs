using MediatR;

namespace Appointments.Application.Appointments.Commands.NotifyUpcoming;

public record NotifyUpcomingAppointmentsCommand() : IRequest;