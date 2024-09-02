using MediatR;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;
using Profiles.Application.Receptionists.Common;
using Profiles.Application.Receptionists.Exceptions;

namespace Profiles.Application.Receptionists.Queries.GetById;

internal class GetReceptionistProfileByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetReceptionistProfileByIdQuery, ReceptionistResult>
{
    public async Task<ReceptionistResult> Handle(GetReceptionistProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var receptionistProfile = await unitOfWork.ReceptionistRepository.GetByIdAsync(request.ReceptionistId);
        if (receptionistProfile is null)
        {
            throw new ReceptionistNotFoundException();
        }

        return receptionistProfile.ToReceptionistResult();
    }
}