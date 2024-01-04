using FileConverter.Bll.FileConverters;
using FileConverter.Bll.FilesTaskQueue;
using FileConverter.DataLayer;
using FileConverter.DataLayer.Enums;

namespace FileConverter.Api.HostedServices;

public class ConvertFileHostedService(IBackgroundTaskQueue taskQueue, ILogger<ConvertFileHostedService> logger, IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation(
            $"Queued Hosted Service is running.{Environment.NewLine}" +
            $"{Environment.NewLine}Tap W to add a work item to the " +
            $"background queue.{Environment.NewLine}");

        using var scope = serviceProvider.CreateScope();

        var newFiles = scope.ServiceProvider.GetRequiredService<UnitOfWork>().FileRepository
            .Filter(x => x.Status == FileStatus.New).ToArray();

        if (newFiles.Length > 0)
        {
            var fileConverter = serviceProvider.GetRequiredService<IFileConverter>();

            foreach (var file in newFiles)
            {
                await fileConverter.Convert(file);
            }
        }

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem =
                await taskQueue.DequeueAsync(stoppingToken);

            try
            {
                Task.Run(async () => await workItem(new BackgroundTaskQueueArguments(stoppingToken, serviceProvider)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Error occurred executing {WorkItem}.", nameof(workItem));
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Queued Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}