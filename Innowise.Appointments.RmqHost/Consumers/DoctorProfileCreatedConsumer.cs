using Innowise.Common.Messages;
using Innowise.Appointments.RmqHost.Extensions;
using MassTransit;
using MediatR;

namespace Innowise.Appointments.RmqHost.Consumers;

public class DoctorProfileCreatedConsumer(ISender sender) : IConsumer<DoctorProfileCreated>
{
    public async Task Consume(ConsumeContext<DoctorProfileCreated> context)
    {
        await sender.Send(context.Message.MapToCommand());
    }
}