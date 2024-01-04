namespace FileConverter.DataLayer.Model;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreateDt { get; set; }
}