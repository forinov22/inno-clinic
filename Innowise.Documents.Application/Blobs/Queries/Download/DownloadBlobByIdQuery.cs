using Azure.Storage.Blobs.Models;
using MediatR;

namespace Documents.Application.Blobs.Queries.Download;

public record DownloadBlobByIdQuery(string BlobId) : IRequest<BlobDownloadResult>;