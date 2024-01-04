using FileConverter.Bll.Dto;

namespace FileConverter.Bll.Services.Interfaces;

public interface IFileService
{
    Task<FileStatusResponseDio> AddNewFileAsync(byte[] fileData, string fileName, Guid sessionKey, Guid fileId);
    Task<FileStatusResponseDio> GetFileStatusAsync(Guid sessionKey, Guid fileId);
    Task<FileResponseDto> GetFileAsync(Guid sessionKey, Guid fileId);
    Task DeleteFileAsync(Guid sessionKey, Guid fileId);
}
