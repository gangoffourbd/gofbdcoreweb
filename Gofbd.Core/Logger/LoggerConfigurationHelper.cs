namespace Gofbd.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Events;

    public static class LoggerConfigurationHelper
    {
        public static void AddSerilog(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("/logs/logfile.txt")
                .CreateLogger();
        }
    }
}