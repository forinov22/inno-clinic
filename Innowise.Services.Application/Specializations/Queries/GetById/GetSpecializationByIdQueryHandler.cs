using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Specializations.Common;
using Innowise.Services.Application.Specializations.Exceptions;
using MediatR;

namespace Innowise.Services.Application.Specializations.Queries.GetById;

public class GetSpecializationByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSpecializationByIdQuery, SpecializationResult>
{
    public async Task<SpecializationResult> Handle(GetSpecializationByIdQuery request, CancellationToken cancellationToken)
    {
        var specialization = await unitOfWork.SpecializationRepository.GetByIdAsync(request.SpecializationId);
        if (specialization is null)
        {
            throw new SpecializationNotFoundException();
        }

        return specialization.ToSpecializationResult();
    }
}