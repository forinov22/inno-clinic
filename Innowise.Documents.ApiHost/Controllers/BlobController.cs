using Documents.Application.Blobs.Commands.Upload;
using Documents.Application.Blobs.Queries.Download;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Documents.API.Controllers;

[ApiController]
[Route("api/blobs")]
public class BlobController(ISender sender) : ControllerBase
{
    [HttpGet("{blobId}")]
    public async Task<IActionResult> DownloadBlob([FromRoute] string blobId)
    {
        var blob = await sender.Send(new DownloadBlobByIdQuery(blobId));
        return File(blob.Content.ToStream(), blob.Details.ContentType);
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadBlob(IFormFile file)
    {
        var blobId = await sender.Send(new UploadBlobCommand(file));
        return CreatedAtAction(nameof(DownloadBlob), new { blobId = blobId }, blobId);
    }
}