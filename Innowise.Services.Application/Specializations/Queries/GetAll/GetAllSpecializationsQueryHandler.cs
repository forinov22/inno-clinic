using Innowise.Services.Application.Extensions;
using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Specializations.Common;
using MediatR;

namespace Innowise.Services.Application.Specializations.Queries.GetAll;

public class GetAllSpecializationsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSpecializationsQuery, IEnumerable<SpecializationResult>>
{
    public async Task<IEnumerable<SpecializationResult>> Handle(GetAllSpecializationsQuery request, CancellationToken cancellationToken)
    {
        var specializations = await unitOfWork.SpecializationRepository.GetAllAsync();
        return specializations.Select(specialization => specialization.ToSpecializationResult()).ToList();
    }
}