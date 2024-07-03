using Appointments.Domain.Entities;

namespace Appointments.Application.Interfaces;

public interface IPdfService
{
    public byte[] GenerateResultPdf(Result result);
}