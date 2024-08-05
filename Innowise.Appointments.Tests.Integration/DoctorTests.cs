using Appointments.Application.Doctors.Commands.ProfileCreated;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Innowise.Appointments.Tests.Integration;

public class DoctorTests : BaseIntegrationTest
{
    public DoctorTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateDoctorProfile_AdsDoctorToDatabase()
    {
        var profileId = Guid.NewGuid();

        var command = new DoctorProfileCreatedCommand(profileId, "FirstName", "LastName", "MiddleName");

        await _sender.Send(command);

        var result = await _context.Doctors.FirstOrDefaultAsync(d => d.ExternalId == profileId);

        result.Should().NotBeNull();
    }
}