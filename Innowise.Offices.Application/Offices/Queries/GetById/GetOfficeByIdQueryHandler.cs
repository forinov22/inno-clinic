using Innowise.Offices.Application.Extensions;
using Innowise.Offices.Application.Interfaces;
using Innowise.Offices.Application.Offices.Common;
using Innowise.Offices.Application.Offices.Exceptions;
using MediatR;

namespace Innowise.Offices.Application.Offices.Queries.GetById;

public class GetOfficeByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOfficeByIdQuery, OfficeResult>
{
    public async Task<OfficeResult> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await unitOfWork.OfficeRepository.GetByIdAsync(request.OfficeId);
        if (office is null)
        {
            throw new OfficeNotFoundException();
        }

        return office.ToOfficeResult();
    }
}