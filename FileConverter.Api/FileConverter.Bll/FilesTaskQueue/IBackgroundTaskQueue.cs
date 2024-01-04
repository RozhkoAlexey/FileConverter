namespace FileConverter.Bll.FilesTaskQueue;

public record BackgroundTaskQueueArguments(CancellationToken Token, IServiceProvider ServiceProvider);

public interface IBackgroundTaskQueue
{
    ValueTask QueueBackgroundWorkItemAsync(Func<BackgroundTaskQueueArguments, ValueTask> workItem);

    ValueTask<Func<BackgroundTaskQueueArguments, ValueTask>> DequeueAsync(
        CancellationToken cancellationToken);
}