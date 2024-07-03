using Appointments.Application.Appointments.Common;
using Appointments.Application.Interfaces;
using Auth.Domain.Exceptions;
using MediatR;

namespace Appointments.Application.Appointments.Queries.GetResultByAppointmentId;

public class GetResultByAppointmentIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetResultByAppointmentIdQuery, ResultResult>
{
    public async Task<ResultResult> Handle(GetResultByAppointmentIdQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.ResultRepository.GetByAppointmentIdAsync(request.AppointmentId);
        if (result is null)
        {
            throw new NotFoundException("Result not found");
        }
        
        return result.MapToDto();
    }
}