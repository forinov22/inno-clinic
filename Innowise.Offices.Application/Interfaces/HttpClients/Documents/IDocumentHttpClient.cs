using Microsoft.AspNetCore.Http;

namespace Innowise.Offices.Application.Interfaces.HttpClients.Documents;

public interface IDocumentHttpClient
{
    Task<string> UploadPhotoAsync(IFormFile photo);
}