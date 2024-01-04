using FileConverter.DataLayer.Model;
using FileConverter.DataLayer.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FileConverter.DataLayer;

public class FileConverterDbContext : DbContext
{
    public FileConverterDbContext(DbContextOptions<FileConverterDbContext> options) :base(options)
    { }

    public DbSet<FileModel> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new FileModelConfig());
    }
}