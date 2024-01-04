using FileConverter.DataLayer.Repositories;
using FileConverter.DataLayer.Repositories.Interfaces;

namespace FileConverter.DataLayer;

public class UnitOfWork(FileConverterDbContext dbContext)
{
    private IFileRepository? _fileRepository;

    public IFileRepository FileRepository => _fileRepository ??= new FileRepository(dbContext);
}