using Appointments.Application.Patients.Commands.ProfileCreated;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Innowise.Appointments.Tests.Integration;

public class PatientsTests : BaseIntegrationTest
{
    public PatientsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreatePatientProfile_AdsPatientToDatabase()
    {
        var profileId = Guid.NewGuid();

        var command = new PatientProfileCreatedCommand(profileId, "FirstName", "LastName", "MiddleName", "test@mail.com");

        await _sender.Send(command);

        var result = await _context.Patients.FirstOrDefaultAsync(d => d.ExternalId == profileId);

        result.Should().NotBeNull();
    }
}