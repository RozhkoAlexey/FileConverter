using FileConverter.DataLayer.Model;

namespace FileConverter.DataLayer.Repositories.Interfaces;

public interface IFileRepository : IAbstractRepository<FileModel>
{
    Task<FileModel?> GetFileAsync(Guid sessionKey, Guid fileId);
}