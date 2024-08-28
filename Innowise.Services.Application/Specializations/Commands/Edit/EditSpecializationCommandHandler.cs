using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Specializations.Common;
using Innowise.Services.Application.Specializations.Exceptions;
using MediatR;

namespace Innowise.Services.Application.Specializations.Commands.Edit;

public class EditSpecializationCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<EditSpecializationCommand, SpecializationResult>
{
    public async Task<SpecializationResult> Handle(EditSpecializationCommand request,
                                                   CancellationToken cancellationToken)
    {
        var specialization = await unitOfWork.SpecializationRepository.GetByIdAsync(request.SpecializationId);
        if (specialization is null)
        {
            throw new SpecializationNotFoundException();
        }

        specialization.SpecializationName = request.SpecializationName;
        specialization.IsActive = request.IsActive;

        unitOfWork.SpecializationRepository.Update(specialization);
        await unitOfWork.SaveAllAsync();

        return specialization.ToSpecializationResult();
    }
}