using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Specializations.Common;
using MediatR;
using Services.Domain;

namespace Innowise.Services.Application.Specializations.Commands.Create;

public class CreateSpecializationCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateSpecializationCommand, SpecializationResult>
{
    public async Task<SpecializationResult> Handle(CreateSpecializationCommand request,
                                                   CancellationToken cancellationToken)
    {
        var specialization = new Specialization()
        {
            SpecializationName = request.SpecializationName,
            IsActive = request.IsActive
        };

        unitOfWork.SpecializationRepository.Add(specialization);
        await unitOfWork.SaveAllAsync();

        return specialization.ToSpecializationResult();
    }
}