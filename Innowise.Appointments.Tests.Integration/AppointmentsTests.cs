using System.Text;
using System.Text.Json;
using Appointments.Application.Appointments.Commands.Create;
using Appointments.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Innowise.Appointments.Tests.Integration;

public class AppointmentsTests : BaseIntegrationTest
{
    public AppointmentsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateAppointment_AdsAppointmentToDatabase()
    {
        var patient = new Patient() { ExternalId = Guid.NewGuid() };
        var doctor = new Doctor() { ExternalId = Guid.NewGuid() };

        _context.Add(patient);
        _context.Add(doctor);
        await _context.SaveChangesAsync();

        var service = new Service() { Id = Guid.NewGuid() };
        await _distributedCache.SetAsync($"service-{service.Id}",
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(service)), new DistributedCacheEntryOptions());

        var command =
            new CreateAppointmentCommand(patient.ExternalId, doctor.ExternalId, service.Id, DateTime.Now);

        var result = await _sender.Send(command);

        var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == result.Id);

        appointment.Should().NotBeNull();
    }
}