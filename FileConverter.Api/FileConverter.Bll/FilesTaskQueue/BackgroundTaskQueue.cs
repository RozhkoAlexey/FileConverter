using System.Threading.Channels;

namespace FileConverter.Bll.FilesTaskQueue;

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<Func<BackgroundTaskQueueArguments, ValueTask>> _queue;

    public BackgroundTaskQueue(int capacity)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<Func<BackgroundTaskQueueArguments, ValueTask>>(options);
    }

    public async ValueTask QueueBackgroundWorkItemAsync(Func<BackgroundTaskQueueArguments, ValueTask> workItem)
    {
        if (workItem == null)
        {
            throw new ArgumentNullException(nameof(workItem));
        }

        await _queue.Writer.WriteAsync(workItem);
    }

    public async ValueTask<Func<BackgroundTaskQueueArguments, ValueTask>> DequeueAsync(CancellationToken cancellationToken) => 
        await _queue.Reader.ReadAsync(cancellationToken);
}
