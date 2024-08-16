using Appointments.Application.Appointments.Commands.UpdateResult;
using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using FluentAssertions;
using MassTransit;
using Moq;

namespace Innowise.Appointments.Tests.Unit.Appointments.Commands;

public class UpdateAppointmentResultCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;

    public UpdateAppointmentResultCommandHandlerTests()
    {
        _unitOfWorkMock = new();
        _publishEndpointMock = new();
    }

    [Fact]
    public async Task UpdateAppointmentResult_CreatesResult_WhenDoesNotExist()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var command =
            new UpdateAppointmentResultCommand(appointmentId, "complaints", "conclusion", "recommendations");

        var handler =
            new UpdateAppointmentResultCommandHandler(_unitOfWorkMock.Object, _publishEndpointMock.Object);

        _unitOfWorkMock.Setup(x => x.AppointmentRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Appointment()
            {
                Id = appointmentId, Patient = new Patient(), Doctor = new Doctor(), Service = new Service()
            });
        _unitOfWorkMock.Setup(x => x.ResultRepository.GetByAppointmentIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Result);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(x => x.ResultRepository.Add(It.IsAny<Result>()), Times.Once);
        result.Complaints.Should().Be("complaints");
        result.Conclusion.Should().Be("conclusion");
        result.Recommendations.Should().Be("recommendations");
    }

    [Fact]
    public async Task UpdateAppointmentResult_UpdatesResult_WhenExist()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var command =
            new UpdateAppointmentResultCommand(appointmentId, "complaints1", "conclusion1",
                "recommendations1");

        var handler =
            new UpdateAppointmentResultCommandHandler(_unitOfWorkMock.Object, _publishEndpointMock.Object);

        _unitOfWorkMock.Setup(x => x.AppointmentRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Appointment()
            {
                Id = appointmentId, Patient = new Patient(), Doctor = new Doctor(), Service = new Service()
            });
        _unitOfWorkMock.Setup(x => x.ResultRepository.GetByAppointmentIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Result()
            {
                Complaints = "complaints", Conclusion = "conclusion", Recommendations = "recommendations",
                Appointment = new Appointment()
                    { Doctor = new Doctor(), Patient = new Patient(), Service = new Service() }
            });

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(x => x.ResultRepository.Add(It.IsAny<Result>()), Times.Never);
        result.Complaints.Should().Be("complaints1");
        result.Conclusion.Should().Be("conclusion1");
        result.Recommendations.Should().Be("recommendations1");
    }
}