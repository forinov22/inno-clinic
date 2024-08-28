using Innowise.Common.Messages;
using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Services.Common;
using Innowise.Services.Application.Services.Exceptions;
using Innowise.Services.Application.Specializations.Exceptions;
using MassTransit;
using MediatR;
using Services.Domain;

namespace Innowise.Services.Application.Services.Commands.Create;

public class CreateServiceCommandHandler(
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreateServiceCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceCategory = await unitOfWork.ServiceCategoryRepository.GetByIdAsync(request.ServiceCategoryId);
        if (serviceCategory is null)
        {
            throw new ServiceNotFoundException();
        }

        var specialization = await unitOfWork.SpecializationRepository.GetByIdAsync(request.SpecializationId);
        if (specialization is null)
        {
            throw new SpecializationNotFoundException();
        }

        var service = new Service()
        {
            ServiceName = request.ServiceName,
            IsActive = request.IsActive,
            Price = request.Price,
            ServiceCategory = serviceCategory,
            Specialization = specialization
        };

        unitOfWork.ServiceRepository.Add(service);
        await unitOfWork.SaveAllAsync();

        await publishEndpoint.Publish(new ServicesUpdated(), cancellationToken);

        return service.ToServiceResult();
    }
}