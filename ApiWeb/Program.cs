using Serilog;
using System.Diagnostics;
using System.Reflection;
using Uibasoft.BaseLab.AppIntegra.Extensions;

try
{
    var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.UseConfiguration(configBuilder);

    builder.Host.UseSerilog((hostingContext, loggerConfig) =>
    {
        loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment)
        .WriteTo.Console();

#if DEBUG
        loggerConfig.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif

    });

    

    builder.Services.AddControllers();
    

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSwaggers($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/home/error");
        app.UseHsts();
    }

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("../swagger/v1/swagger.json", "API LAB v1");
    });


    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }
    Log.Fatal(ex, "Host Terminated Unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

