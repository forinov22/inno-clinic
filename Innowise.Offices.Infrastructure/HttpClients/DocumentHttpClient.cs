using System.Net.Http.Headers;
using Innowise.Offices.Application.Interfaces.HttpClients.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Innowise.Offices.Infrastructure.HttpClients;

public class DocumentHttpClient(HttpClient httpClient, ILogger<DocumentHttpClient> logger) : IDocumentHttpClient
{
    public async Task<string> UploadPhotoAsync(IFormFile photo)
    {
        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(photo.OpenReadStream());
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(photo.ContentType);
        content.Add(fileContent, "file", photo.FileName);

        try
        {
            var response = await httpClient.PostAsync("api/blobs", content);
            response.EnsureSuccessStatusCode();

            var photoId = await response.Content.ReadAsStringAsync();
            return photoId;
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "An error occurred while uploading photo.");
            throw;
        }
    }
}