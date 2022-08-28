try
{
    var builder = WebApplication.CreateBuilder(args);

    Gambling.Api.Startup.Services.Add(builder.Services, builder.Configuration);

    var app = builder.Build();

    Gambling.Api.Startup.Middlewares.Use(app, builder.Environment);

    app.Run();
}
catch
{
    //TODO: Log "An unhandled exception occured."
}
finally
{
    //TODO: Flush
}