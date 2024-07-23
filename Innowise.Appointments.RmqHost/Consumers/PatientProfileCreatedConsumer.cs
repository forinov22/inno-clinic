using Innowise.Appointments.RmqHost.Extensions;
using Innowise.Common.Messages;
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