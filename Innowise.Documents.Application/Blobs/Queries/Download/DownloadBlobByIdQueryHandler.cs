using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;

namespace Documents.Application.Blobs.Queries.Download;

public class DownloadBlobByIdQueryHandler(BlobContainerClient blobContainerClient) : IRequestHandler<DownloadBlobByIdQuery, BlobDownloadResult>
{
    public async Task<BlobDownloadResult> Handle(DownloadBlobByIdQuery request, CancellationToken cancellationToken)
    {
        var blobClient = blobContainerClient.GetBlobClient(request.BlobId);
        var blobInfo = await blobClient.DownloadContentAsync(cancellationToken);
        return blobInfo.Value;
    }
}