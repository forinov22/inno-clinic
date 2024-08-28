using MediatR;
using Microsoft.AspNetCore.Http;

namespace Documents.Application.Blobs.Commands.Upload;

public record UploadBlobCommand(IFormFile Blob) : IRequest<string>;