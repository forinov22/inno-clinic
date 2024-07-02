using Appointments.Application.Interfaces;
using Appointments.Application.Results.Common;
using Auth.Domain.Exceptions;
using MediatR;

namespace Appointments.Application.Results.Commands.Edit;

public class EditAppointmentResultCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditAppointmentResultCommand, ResultResult>
{
    public async Task<ResultResult> Handle(EditAppointmentResultCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.ResultRepository.GetByIdAsync(request.ResultId);
        if (result is null)
        {
            throw new NotFoundException("Result not found");
        }

        result.Complaints = request.Complaints;
        result.Conclusion = request.Conclusion;
        result.Recommendations = request.Recommendations;
        
        unitOfWork.ResultRepository.Update(result);
        await unitOfWork.SaveAllAsync();
        
        return ResultResult.MapFromResult(result);
    }
}