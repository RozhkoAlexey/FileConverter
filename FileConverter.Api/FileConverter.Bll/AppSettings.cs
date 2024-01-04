namespace FileConverter.Bll;

public class AppSettings
{
    public string PathToWorkDir { get; set; } = string.Empty;
    public int QueueCapacity { get; set; } = int.MaxValue;
}
