using FileConverter.DataLayer.Model;

namespace FileConverter.Bll.FileConverters;

public interface IFileConverter
{
    ValueTask Convert(FileModel file);
}