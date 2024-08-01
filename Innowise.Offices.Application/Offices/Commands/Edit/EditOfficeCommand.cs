using Innowise.Offices.Application.Offices.Common;
using MediatR;

namespace Innowise.Offices.Application.Offices.Commands.Edit;

public record EditOfficeCommand(Guid OfficeId,
                                string City,
                                string Street,
                                string HouseNumber,
                                string OfficeNumber,
                                string RegistryPhoneNumber,
                                string OfficeStatus) : IRequest<OfficeResult>;