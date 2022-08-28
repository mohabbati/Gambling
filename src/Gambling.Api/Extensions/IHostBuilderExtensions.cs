using Serilog;

namespace Microsoft.AspNetCore.Builder;

public static class IHostBuilderExtensions
{
    public static IHostBuilder ConfigureLogger(this IHostBuilder builder, IHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsDevelopment())
        {
            builder.UseSerilog((hostBuilder, loggerConfiguration) =>
            {
                loggerConfiguration.WriteTo.Console(
                        outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level:u3} {Message:lj}{NewLine}{Exception}");
            });
        }
        else
        {
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "log/Gambling-Log-.txt");

            builder.UseSerilog((hostBuilder, loggerConfiguration) =>
            {
                loggerConfiguration
                    .WriteTo.Console()
                    .WriteTo.File(logPath, rollingInterval: RollingInterval.Hour);
            });
        }

        return builder;
    }
}