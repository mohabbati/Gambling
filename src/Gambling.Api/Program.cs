using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureLogger(builder.Environment);

    Gambling.Api.Startup.Services.Add(builder.Services, builder.Configuration);

    var app = builder.Build();

    Gambling.Api.Startup.Middlewares.Use(app, builder.Environment);

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred.");
}
finally
{
    Log.CloseAndFlush();
}