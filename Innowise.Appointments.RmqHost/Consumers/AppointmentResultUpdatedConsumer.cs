using Innowise.Common.Messages;
using Innowise.Appointments.RmqHost.Extensions;
using MassTransit;
using MediatR;

namespace Innowise.Appointments.RmqHost.Consumers;

public class AppointmentResultUpdatedConsumer(ISender sender) : IConsumer<AppointmentResultUpdated>
{
    public async Task Consume(ConsumeContext<AppointmentResultUpdated> context)
    {
        await sender.Send(context.Message.MapToCommand());
    }
}