using Microsoft.Extensions.Logging;
using System;

namespace Facts.Web.Extensions
{
    public static class EventIdentifiers
    {
        public static readonly EventId DatabaseSavingErrorId = new(50010001, "DatabaseSavingError");
        public static readonly EventId DatabaseAddedEntityId = new(50010002, "DatabaseAddedEntity");
        public static readonly EventId NotificationProcessedId = new(50010003, "NotificationProcessed");
    }

    public static class LoggerExtension
    {
        public static void NotificationProcessed(this ILogger source, string message, Exception? exception = null)
        {
            NotificationProcessedExecute(source, message, null);
        }
        private static readonly Action<ILogger, string, Exception?> NotificationProcessedExecute =
            LoggerMessage.Define<string>(LogLevel.Error, EventIdentifiers.DatabaseSavingErrorId, "Processing for notification  started {message}");


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
