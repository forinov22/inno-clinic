using Appointments.Application.Appointments.Commands.Approve;
using Appointments.Application.Appointments.Exceptions;
using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Innowise.Appointments.Tests.Unit.Appointments.Commands;

public class ApproveAppointmentCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public ApproveAppointmentCommandHandlerTests()
    {
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task ApproveAppointment_ApprovesAppointment_WhenSuccess()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var command = new ApproveAppointmentCommand(appointmentId);
        var handler = new ApproveAppointmentCommandHandler(_unitOfWorkMock.Object);

        _unitOfWorkMock
            .Setup(x => x.AppointmentRepository.GetByIdAsync(It.Is<Guid>(value => value == appointmentId)))
            .ReturnsAsync(new Appointment() { Id = appointmentId });

        _unitOfWorkMock
            .Setup(x => x.AppointmentRepository.GetDoctorDateAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .ReturnsAsync([]);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().Be(appointmentId);
    }

    [Fact]
    public async Task ApproveAppointment_ThrowsTimeSlotIsAlreadyInUseException_WhenTimeSlotIsAlreadyBooked()
    {
        var appointmentId = Guid.NewGuid();
        var command = new ApproveAppointmentCommand(appointmentId);
        var handler = new ApproveAppointmentCommandHandler(_unitOfWorkMock.Object);

        var appointmentStartDate = DateTime.Now;

        _unitOfWorkMock
            .Setup(x => x.AppointmentRepository.GetByIdAsync(It.Is<Guid>(value => value == appointmentId)))
            .ReturnsAsync(new Appointment()
            {
                Id = appointmentId, StartDate = appointmentStartDate,
                Service = new Service() { TimeSlotSize = 3 }
            });

        _unitOfWorkMock
            .Setup(x => x.AppointmentRepository.GetDoctorDateAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .ReturnsAsync([
                new Appointment()
                    { StartDate = appointmentStartDate, Service = new Service() { TimeSlotSize = 1 } }
            ]);

        await handler.Invoking(x => x.Handle(command, default))
            .Should()
            .ThrowExactlyAsync<TimeSlotIsAlreadyInUseException>();
    }
}