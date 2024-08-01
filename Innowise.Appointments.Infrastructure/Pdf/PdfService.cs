using Appointments.Application.Interfaces;
using Appointments.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Appointments.Infrastructure.Pdf;

public class ResultDocument(Result result) : IDocument
{
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Header()
                .Text("Medical Report")
                .FontSize(20)
                .Bold()
                .AlignCenter();

            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .Column(column =>
                {
                    column.Spacing(20);

                    column.Item()
                          .Text($"Date: {result.DateTime:dd MMMM yyyy}");
                    column.Item()
                          .Text(
                              $"Patient Name: {result.Appointment.Patient.FirstName} {result.Appointment.Patient.LastName} {result.Appointment.Patient.MiddleName}");
                    column.Item()
                          .Text(
                              $"Doctor Name: {result.Appointment.Doctor.FirstName} {result.Appointment.Doctor.LastName} {result.Appointment.Doctor.MiddleName}");
                    column.Item()
                          .Text($"Service: {result.Appointment.Service.ServiceName}");

                    column.Item()
                          .Text("Complaints")
                          .FontSize(16)
                          .Bold();

                    column.Item()
                          .Text(result.Complaints);

                    column.Item()
                          .Text("Conclusion")
                          .FontSize(16)
                          .Bold();

                    column.Item()
                          .Text(result.Conclusion);

                    column.Item()
                          .Text("Recommendations")
                          .FontSize(16)
                          .Bold();

                    column.Item()
                          .Text(result.Recommendations);
                });

            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                    x.Span(" of ");
                    x.TotalPages();
                });
        });
    }
}

public class PdfService : IPdfService
{
    public byte[] GenerateResultPdf(Result result)
    {
        var resultDocument = new ResultDocument(result);
        return resultDocument.GeneratePdf();
    }
}