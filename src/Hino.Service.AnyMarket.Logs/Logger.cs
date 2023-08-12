using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Logs
{
    public static class Logger
    {
        private static readonly ILogger _logger;

        static Logger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.SQLite("./logs/logs.db")
                .CreateLogger();
        }

        public static void LogDebug(string message, Exception exception = null)
        {
            if (exception == null)
                _logger.Debug(message);
            else 
                _logger.Debug(exception, message);
        }

        public static void LogInformation(string message, Exception exception = null)
        {
            if (exception == null)
                _logger.Information(message);
            else
                _logger.Information(exception, message);
        }

        public static void LogWarning(string message, Exception exception = null)
        {
            if (exception == null)
                _logger.Warning(message);
            else
                _logger.Warning(exception, message);
        }

        public static void LogError(string message, Exception exception = null)
        {
            if (exception == null)
                _logger.Error(message);
            else
                _logger.Error(exception, message);
        }
    }
}
