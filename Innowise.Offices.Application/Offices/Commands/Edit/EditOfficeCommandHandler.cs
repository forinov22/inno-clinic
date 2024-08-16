using Innowise.Offices.Application.Extensions;
using Innowise.Offices.Application.Interfaces;
using Innowise.Offices.Application.Offices.Common;
using Innowise.Offices.Application.Offices.Exceptions;
using Innowise.Offices.Domain.Entities;
using MassTransit;
using MediatR;

namespace Innowise.Offices.Application.Offices.Commands.Edit;

public class EditOfficeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<EditOfficeCommand, OfficeResult>
{
    public async Task<OfficeResult> Handle(EditOfficeCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<OfficeStatus>(request.OfficeStatus, out var officeStatus))
            throw new InvalidOfficeStatusException();

        var office = await unitOfWork.OfficeRepository.GetByIdAsync(request.OfficeId);
        if (office is null)
        {
            throw new OfficeNotFoundException();
        }

        office.Address = new Address()
        {
            City = request.City,
            HouseNumber = request.HouseNumber,
            OfficeNumber = request.OfficeNumber,
            Street = request.Street
        };
        office.RegistryPhoneNumber = request.RegistryPhoneNumber;
        office.OfficeStatus = officeStatus;

        unitOfWork.OfficeRepository.Update(office);
        await unitOfWork.SaveAllAsync();

        return office.ToOfficeResult();
    }
}