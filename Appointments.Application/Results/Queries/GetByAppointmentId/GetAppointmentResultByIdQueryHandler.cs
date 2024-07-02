using Appointments.Application.Interfaces;
using Appointments.Application.Results.Common;
using Auth.Domain.Exceptions;
using MediatR;

namespace Appointments.Application.Results.Queries.GetByAppointmentId;

internal class GetAppointmentResultByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAppointmentResultByIdQuery, ResultResult>
{
    public async Task<ResultResult> Handle(GetAppointmentResultByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.ResultRepository.GetByAppointmentIdAsync(request.AppointmentId);
        if (result is null)
        {
            throw new NotFoundException("Result not found");
        }
        
        return ResultResult.MapFromResult(result);
    }
}