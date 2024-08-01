using Appointments.Application.Interfaces;
using MediatR;

namespace Appointments.Application.Services.Commands.ServicesUpdated;

// public class ServicesUpdatedCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ServicesUpdatedCommand>
// {
//     public async Task Handle(ServicesUpdatedCommand request, CancellationToken cancellationToken)
//     {
//         await unitOfWork.ServiceRepository.FetchServicesAsync();
//     }
// }