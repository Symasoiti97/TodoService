using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Logger
{
    internal class FileLogger : ILogger
    {
        private readonly string _directory;
        private readonly string _category;
        private readonly IExternalScopeProvider _scopeProvider;

        private static readonly object Lock = new object();

        private static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
        {
            Converters = {new JsonStringEnumConverter()}
        };

        public FileLogger(string directory, string category, IExternalScopeProvider scopeProvider)
        {
            _directory = directory ?? throw new ArgumentNullException(nameof(directory));
            _category = category;
            _scopeProvider = scopeProvider ?? throw new ArgumentNullException(nameof(scopeProvider));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _scopeProvider.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var logInfo = new LogInfo
            {
                DateTime = DateTime.UtcNow,
                LogLevel = logLevel,
                EventId = eventId,
                ExceptionMessage = exception?.ToString(),
                State = state,
                Category = _category
            };
            _scopeProvider.ForEachScope((obj, info) => { info.Scope.Add(obj); }, logInfo);

            if (formatter != null)
            {
                logInfo.Message = formatter(state, exception);
            }

            lock (Lock)
            {
                File.AppendAllText(Path.Combine(_directory, $"{DateTime.UtcNow:yy-MM-dd}.log"), JsonSerializer.Serialize(logInfo, JsonSerializerOptions));
            }
        }

        private class LogInfo
        {
            public DateTime DateTime { get; set; }
            public LogLevel LogLevel { get; set; }
            public EventId EventId { get; set; }
            public string ExceptionMessage { get; set; }
            public object State { get; set; }
            public List<object> Scope { get; } = new List<object>();
            public string Message { get; set; }
            public string Category { get; set; }
        }
    }
}