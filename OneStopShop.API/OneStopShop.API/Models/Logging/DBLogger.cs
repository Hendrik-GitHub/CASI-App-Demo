using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OneStopShop.API.Services.Logging;

namespace OneStopShop.API.Models.Logging
{
    public class DBLogger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private LoggingRepository _loggingRepository;
        private int MessageMaxLength = 4000;

        public DBLogger(string categoryName, Func<string, LogLevel, bool> filter, string connectionString)
        {
            _categoryName = categoryName;
            _filter = filter;
            _loggingRepository = new LoggingRepository(connectionString);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }

            message = message.Length > MessageMaxLength ? message.Substring(0, MessageMaxLength) : message;
            DateTime todaysDate = DateTime.UtcNow;

            Log eventLog = new Log
            {
                eventid = eventId.Id,
                priority = 1,
                severity = logLevel.ToString(),
                title = "",
                //TimeStamp = todaysDate,
                machinename = Process.GetCurrentProcess().MachineName,
                appdomainname = AppDomain.CurrentDomain.FriendlyName,
                processid = Process.GetCurrentProcess().Id.ToString(),
                processname = Process.GetCurrentProcess().ProcessName,
                threadname = Thread.CurrentThread.Name,
                win32threadid = Thread.CurrentThread.ManagedThreadId.ToString(),
                message = message,
                formattedmessage = "TimeStamp: " + todaysDate.ToString() + ". Message: " + message + "Category: OneStopShopAPI. " + "Priority: 1. " + "EventId: " + eventId.Id.ToString()
                 + ". Severity: " + logLevel.ToString() + ". Title: " + ". Machine: " + Process.GetCurrentProcess().MachineName + ". App Domain: " + AppDomain.CurrentDomain.FriendlyName
                 + ". ProcessId: " + Process.GetCurrentProcess().Id.ToString() + ". ProcessName: " + Process.GetCurrentProcess().ProcessName + ". Thread Name: " + Thread.CurrentThread.Name
                 + ". ThreadId: " + Thread.CurrentThread.ManagedThreadId.ToString() + ". Extended Properties:"
            };

            _loggingRepository.InsertLog(eventLog);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
