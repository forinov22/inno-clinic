using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;

namespace Documents.Application.Blobs.Commands.Upload;

public class UploadBlobCommandHandler(BlobContainerClient blobContainerClient)
    : IRequestHandler<UploadBlobCommand, string>
{
    public async Task<string> Handle(UploadBlobCommand request, CancellationToken cancellationToken)
    {
        var blobName = Guid.NewGuid() + request.Blob.FileName;
        var blobClient = blobContainerClient.GetBlobClient(blobName);
        var memoryStream = new MemoryStream();
        await request.Blob.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;
        await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders() { ContentType = request.Blob.ContentType },
                                     cancellationToken: cancellationToken);
        return blobName;
    }
}