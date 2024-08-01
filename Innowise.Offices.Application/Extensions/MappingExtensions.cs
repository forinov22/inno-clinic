using Innowise.Offices.Application.Offices.Common;
using Innowise.Offices.Domain.Entities;

namespace Innowise.Offices.Application.Extensions;

public static class MappingExtensions
{
    public static OfficeResult ToOfficeResult(this Office office)
    {
        return new OfficeResult(office.Id, office.PhotoUrl, office.Address.City, office.Address.Street,
                                office.Address.HouseNumber, office.Address.OfficeNumber, office.RegistryPhoneNumber,
                                office.OfficeStatus.ToString());
    }
}