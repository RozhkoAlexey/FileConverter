using FileConverter.DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileConverter.DataLayer.ModelConfigurations;

public class FileModelConfig : IEntityTypeConfiguration<FileModel>
{
    protected static string SqlGetDateFunc => "timezone('UTC', now())";

    public void Configure(EntityTypeBuilder<FileModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.CreateDt)
            .IsRequired()
            .HasDefaultValueSql(SqlGetDateFunc)
            .ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

        builder.Property(x => x.FileId).IsRequired();
        builder.Property(x => x.SessionKey).IsRequired();
        builder.Property(x => x.FileName).IsRequired();
        builder.Property(x => x.ResultFileName).IsRequired();
    }
}