using FileConverter.DataLayer.Enums;

namespace FileConverter.DataLayer.Model;

public class FileModel : BaseModel
{
    public Guid SessionKey { get; set; }
    public Guid FileId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ResultFileName { get; set; } = string.Empty;
    public FileStatus Status { get; set; }
}