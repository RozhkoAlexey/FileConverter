using FileConverter.Bll.FilesTaskQueue;
using FileConverter.DataLayer.Model;

namespace FileConverter.Bll.FileConverters;

public abstract class FileConverter(IBackgroundTaskQueue taskQueue) : IFileConverter
{
    protected abstract string Extension { get; }

    protected string GetNewFileName(string fileName)
    {
        var fileInfo = new FileInfo(fileName);

        return fileInfo.Name.Replace(fileInfo.Extension, string.Empty) + Extension;
    }

    protected abstract ValueTask DoConvertAsync(BackgroundTaskQueueArguments args, FileModel file);

    public async ValueTask Convert(FileModel file) => await taskQueue.QueueBackgroundWorkItemAsync(async (args) => await DoConvertAsync(args, file));
}
