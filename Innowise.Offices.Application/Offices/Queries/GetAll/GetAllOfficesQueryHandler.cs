using Innowise.Offices.Application.Extensions;
using Innowise.Offices.Application.Interfaces;
using Innowise.Offices.Application.Offices.Common;
using MediatR;

namespace Innowise.Offices.Application.Offices.Queries.GetAll;

public class GetAllOfficesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOfficesQuery, IEnumerable<OfficeResult>>
{
    public async Task<IEnumerable<OfficeResult>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
    {
        var offices = await unitOfWork.OfficeRepository.GetAllAsync();
        return offices.Select(office => office.ToOfficeResult());
    }
}