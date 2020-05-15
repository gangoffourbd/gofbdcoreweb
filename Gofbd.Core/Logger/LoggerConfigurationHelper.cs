namespace Gofbd.Core.Logger
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class LoggerConfigurationHelper
    {
        public static ILoggerFactory UseFileLoggin(this ILoggerFactory loggerFactory)
        {
            return loggerFactory;
        }
    }
}
