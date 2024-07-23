using Innowise.Appointments.RmqHost.Extensions;
using Innowise.Common.Messages;
using MassTransit;
using MediatR;

namespace Innowise.Appointments.RmqHost.Consumers;

public class PatientProfileLinkedToAccountConsumer(ISender sender) : IConsumer<PatientProfileLinkedToAccount>
{
    public async Task Consume(ConsumeContext<PatientProfileLinkedToAccount> context)
    {
        await sender.Send(context.Message.MapToCommand());
    }
}