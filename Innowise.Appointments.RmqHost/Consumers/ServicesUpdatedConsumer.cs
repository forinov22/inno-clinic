using Appointments.Application.Services.Commands.ServicesUpdated;
using InnoClinic.Contracts;
using MassTransit;
using MediatR;

namespace Innowise.Appointments.RmqHost.Consumers;

public class ServicesUpdatedConsumer(ISender sender) : IConsumer<ServicesUpdated>
{
    public async Task Consume(ConsumeContext<ServicesUpdated> context)
    {
        await sender.Send(new ServicesUpdatedCommand());
    }
}