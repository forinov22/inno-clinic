using System.Text;
using System.Text.Json;
using Appointments.Application.Appointments.Commands.Create;
using Appointments.Application.Doctors.Exceptions;
using Appointments.Application.Interfaces;
using Appointments.Application.Interfaces.HttpClients.Services;
using Appointments.Application.Patients.Exceptions;
using Appointments.Application.Services.Exceptions;
using Appointments.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Polly;
using Polly.Registry;

namespace Innowise.Appointments.Tests.Unit.Appointments.Commands;

public class CreateAppointmentCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IServiceHttpClient> _serviceHttpClientMock;
    private readonly Mock<IDistributedCache> _distributedCacheMock;
    private readonly Mock<ResiliencePipelineProvider<string>> _pipelineProviderMock;

    public CreateAppointmentCommandHandlerTests()
    {
        _unitOfWorkMock = new();
        _serviceHttpClientMock = new();
        _distributedCacheMock = new();
        _pipelineProviderMock = new();
    }

    [Fact]
    public async Task CreateAppointment_ThrowsException_WhenDoctorNotFound()
    {
        var command =
            new CreateAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now);

        var handler = new CreateAppointmentCommandHandler(_unitOfWorkMock.Object,
            _serviceHttpClientMock.Object,
            _distributedCacheMock.Object, _pipelineProviderMock.Object);

        _unitOfWorkMock.Setup(x => x.DoctorRepository.GetByIdAsync(It.IsAny<Guid>()));
        _unitOfWorkMock.Setup(x => x.PatientRepository.GetByIdAsync(It.IsAny<Guid>()));
        _distributedCacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Service())));

        await handler.Invoking(h => h.Handle(command, default))
            .Should()
            .ThrowAsync<DoctorNotFoundException>();
    }

    [Fact]
    public async Task CreateAppointment_ThrowsException_WhenPatientNotFound()
    {
        var command =
            new CreateAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now);

        var handler = new CreateAppointmentCommandHandler(_unitOfWorkMock.Object,
            _serviceHttpClientMock.Object,
            _distributedCacheMock.Object, _pipelineProviderMock.Object);

        _unitOfWorkMock.Setup(x => x.DoctorRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Doctor());
        _unitOfWorkMock.Setup(x => x.PatientRepository.GetByIdAsync(It.IsAny<Guid>()));
        _distributedCacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Service())));

        await handler.Invoking(h => h.Handle(command, default))
            .Should()
            .ThrowAsync<PatientNotFoundException>();
    }

    [Fact]
    public async Task CreateAppointment_ThrowsException_WhenServiceNotFound()
    {
        var command =
            new CreateAppointmentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now);

        var handler = new CreateAppointmentCommandHandler(_unitOfWorkMock.Object,
            _serviceHttpClientMock.Object,
            _distributedCacheMock.Object, _pipelineProviderMock.Object);

        _unitOfWorkMock.Setup(x => x.DoctorRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Doctor());
        _unitOfWorkMock.Setup(x => x.PatientRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Patient());
        _distributedCacheMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as byte[]);
        var pipeline = new ResiliencePipelineBuilder().Build();
        _pipelineProviderMock.Setup(x => x.GetPipeline(It.IsAny<string>())).Returns(pipeline);
        _serviceHttpClientMock.Setup(x => x.GetServiceByIdAsync(It.IsAny<ServiceRequest>()))
            .ReturnsAsync(null as ServiceResponse);

        await handler.Invoking(h => h.Handle(command, default))
            .Should()
            .ThrowAsync<ServiceNotFoundException>();
    }
}