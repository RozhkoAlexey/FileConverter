using FileConverter.DataLayer.Model;
using FileConverter.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileConverter.DataLayer.Repositories;

public class FileRepository : AbstractRepository<FileModel>, IFileRepository
{
    public FileRepository(FileConverterDbContext context) : base(context)
    { }

    public async Task<FileModel?> GetFileAsync(Guid sessionKey, Guid fileId) =>
        await Filter(x => x.FileId == fileId && x.SessionKey == sessionKey)
            .AsNoTracking()
            .FirstOrDefaultAsync();
}