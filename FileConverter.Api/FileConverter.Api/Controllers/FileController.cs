using FileConverter.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileConverter.Api.Controllers;

public record UploadRequest(IFormFile File, Guid FileId, Guid SessionKey);

[ApiController]
[Route("[controller]")]
public class FileController(IFileService fileService) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile([FromForm] UploadRequest request)
    {
        if (request.File is null or { Length: 0 })
            throw new ArgumentException("File cannot be empty");

        using var stream = new MemoryStream();

        await request.File.CopyToAsync(stream);

        var res = await fileService.AddNewFileAsync(stream.ToArray(), request.File.FileName, request.SessionKey,
            request.FileId);
        
        return Ok(res);
    }

    [HttpGet("[action]/{sessionKey}/{fileId}")]
    public async Task<IActionResult> GetFileStatus(Guid sessionKey, Guid fileId)
    {
        var filStatus = await fileService.GetFileStatusAsync(sessionKey, fileId);

        return Ok(filStatus);
    }

    [HttpDelete("[action]/{sessionKey}/{fileId}")]
    public async Task<IActionResult> DeleteFile(Guid sessionKey, Guid fileId)
    {
        await fileService.DeleteFileAsync(sessionKey, fileId);

        return Ok();
    }

    [HttpGet("[action]/{sessionKey}/{fileId}")]
    public async Task<IActionResult> DownloadFile(Guid sessionKey, Guid fileId)
    {
        var file = await fileService.GetFileAsync(sessionKey, fileId);

        return File(file.FileData, "application/pdf");
    }
}
