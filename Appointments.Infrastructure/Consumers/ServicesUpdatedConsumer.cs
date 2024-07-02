using Appointments.Application.Interfaces;
using Appointments.Application.Interfaces.Repositories;
using InnoClinic.Contracts;
using MassTransit;

namespace Appointments.Infrastructure.Consumers;

public class ServicesUpdatedConsumer(IUnitOfWork unitOfWork) : IConsumer<ServicesUpdated>
{
    public async Task Consume(ConsumeContext<ServicesUpdated> context)
    {
        await unitOfWork.ServiceRepository.FetchServicesAsync();
    }
}