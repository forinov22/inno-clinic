using Innowise.Offices.Application.Offices.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Innowise.Offices.Application.Offices.Commands.Create;

public record CreateOfficeCommand(
    IFormFile Photo,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    string RegistryPhoneNumber,
    string OfficeStatus) : IRequest<OfficeResult>;