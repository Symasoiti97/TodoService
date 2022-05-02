using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Logger
{
    internal class FileLoggerProvider : ILoggerProvider
    {
        private readonly LoggerExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();
        private readonly ConcurrentDictionary<string, FileLogger> _loggers = new ConcurrentDictionary<string, FileLogger>(StringComparer.Ordinal);

        private readonly string _directory;

        public FileLoggerProvider(string directory)
        {
            _directory = directory;
        }

        public ILogger CreateLogger(string categoryName = "Unknown")
        {
            return _loggers.GetOrAdd(categoryName, category => new FileLogger(_directory, category, _scopeProvider));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}