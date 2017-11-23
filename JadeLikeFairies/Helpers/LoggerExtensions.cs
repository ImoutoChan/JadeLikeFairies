using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace JadeLikeFairies.Helpers
{
    static class LoggerExtensions
    {
        public static void LogMethodError(this ILogger logger, Exception exception, [CallerMemberName] string methodName = "")
        {
            logger.LogError(exception, $"Error occurred in {methodName} method");
        }

        public static void TraceCommandReceived(this ILogger logger, string commandName)
        {
            logger.LogTrace($"{commandName} command recieved");
        }
    }
}
