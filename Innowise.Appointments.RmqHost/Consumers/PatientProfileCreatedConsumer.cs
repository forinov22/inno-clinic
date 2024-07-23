using Innowise.Common.Messages;
using Innowise.Appointments.RmqHost.Extensions;
using MassTransit;
using MediatR;

namespace Innowise.Appointments.RmqHost.Consumers;

public class PatientProfileCreatedConsumer(ISender sender) : IConsumer<PatientProfileCreated>
{
    public async Task Consume(ConsumeContext<PatientProfileCreated> context)
    {
        await sender.Send(context.Message.MapToCommand());
    }
}