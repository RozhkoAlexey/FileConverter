using FileConverter.Bll.FilesTaskQueue;
using FileConverter.DataLayer;
using FileConverter.DataLayer.Enums;
using FileConverter.DataLayer.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PuppeteerSharp;

namespace FileConverter.Bll.FileConverters;

public class PdfFileConverter(IOptions<AppSettings> options, ILogger<PdfFileConverter> logger, IBackgroundTaskQueue taskQueue) : FileConverter(taskQueue)
{
    protected override string Extension => ".pdf";

    private readonly AppSettings _appSettings = options.Value;

    protected override async ValueTask DoConvertAsync(BackgroundTaskQueueArguments args, FileModel file)
    {
        try
        {
            var currentPath = Path.Combine(_appSettings.PathToWorkDir, file.SessionKey.ToString(),
                file.FileId.ToString());

            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }

            var fileName = GetNewFileName(file.FileName);
            var fileData = await File.ReadAllTextAsync(Path.Combine(currentPath, file.FileName));
            var downloadResult = await new BrowserFetcher().DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = downloadResult.GetExecutablePath()
            });

            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(fileData);
            await page.PdfAsync(Path.Combine(currentPath, fileName));

            file.ResultFileName = fileName;
            file.Status = FileStatus.Completed;

            using var scope = args.ServiceProvider.CreateScope();

            await scope.ServiceProvider.GetRequiredService<UnitOfWork>().FileRepository.UpdateAsync(file);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message, e);
        }
    }
}