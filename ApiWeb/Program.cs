using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Uibasoft.BaseLab.AppIntegra.Extensions;
using Uibasoft.BaseLab.AppIntegra.Filters;
using Uibasoft.BaseLab.Domain.CustomEntities;

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

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
    }).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddOptions(builder.Configuration);

    builder.Services.AddDbContexts(builder.Configuration);
    builder.Services.AddServices(builder.Configuration);

    builder.Services.AddSwaggers($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(jwt =>
    {
        var authJwt = builder.Configuration.GetSection(nameof(AuthJwtOptions)).Get<AuthJwtOptions>();

        var key = Encoding.ASCII.GetBytes(authJwt.Secret);
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true,
        };
    });

    builder.Services.AddMvc(options =>
    {
        options.Filters.Add<ValidationFilter>();
    }).AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    });

    
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    //using (var scope = app.Services.CreateScope())
    //{
    //    var context = scope.ServiceProvider.GetRequiredService<AppCoreEFContext>();
    //    //context.Database.Migrate();
    //    context.Database.EnsureCreated();
    //} 

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

    app.UseStaticFiles();

    app.UseAuthFarma();

    app.UseRouting();

    app.UseAuthorization();
    app.UseAuthentication();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();

        app.MapControllerRoute(
        name: "default",
        pattern: "{controller=home}/{action=index}/{id?}");

    });

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

