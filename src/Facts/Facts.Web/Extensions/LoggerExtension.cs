using Microsoft.Extensions.Logging;
using System;

namespace Facts.Web.Extensions
{
    public static class EventIdentifiers 
    {
        public static readonly EventId DatabaseSavingErrorId = new (50010001, "DatabaseSavingError");
        public static readonly EventId DatabaseAddedEntityId = new (50010002, "DatabaseAddedEntity");
    }

    public static class LoggerExtension
    {
        public static void LogDababaseSavingError(this ILogger source, string subject, Exception? exception = null)
        {
            LogDababaseSavingErrorExecute(source, subject, exception);
        }       
        private static readonly Action<ILogger, string, Exception?> LogDababaseSavingErrorExecute =
            LoggerMessage.Define<string>(LogLevel.Error, EventIdentifiers.DatabaseSavingErrorId, "{exception}");
        
        public static void NotificationAdded(this ILogger source, string entityName, Exception? exception = null)
        {
            LogDababaseAddedEntityExecute(source, entityName, exception);
        }
        private static readonly Action<ILogger, string, Exception?> LogDababaseAddedEntityExecute =
            LoggerMessage.Define<string>(LogLevel.Information, EventIdentifiers.DatabaseAddedEntityId, "{new notification created}");
       
    }
}
