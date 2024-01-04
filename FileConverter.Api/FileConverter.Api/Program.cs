using FileConverter.DataLayer;
using FileConverter.Bll.Services;
using FileConverter.Bll.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FileConverter.Bll;
using FileConverter.Api.HostedServices;
using FileConverter.Bll.FileConverters;
using FileConverter.Bll.FilesTaskQueue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsName = "AppCorName";

builder.Services.AddCors(o => o.AddPolicy(corsName, x =>
{
    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

IConfiguration configuration = builder.Configuration;

builder.Services.AddDbContext<FileConverterDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<ConvertFileHostedService>();

builder.Services.AddTransient<UnitOfWork>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFileConverter, PdfFileConverter>();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddLog4Net("log4net.config");
    logging.AddConsole();
});

var appSettingsSections = builder.Configuration.GetSection(nameof(AppSettings));
var appSections = new AppSettings();

appSettingsSections.Bind(appSections);

builder.Services.AddSingleton<IBackgroundTaskQueue>(ctx =>
{
    var queueCapacity = appSections.QueueCapacity == 0 ? int.MaxValue : appSections.QueueCapacity;

    return new BackgroundTaskQueue(queueCapacity);
});


builder.Services.Configure<AppSettings>(appSettingsSections);

var app = builder.Build();

app.UseCors(corsName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
