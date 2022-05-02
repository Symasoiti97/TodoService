using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Logger
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFileProvider(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<FileLoggerOptions>(options => configuration.GetSection(nameof(FileLoggerOptions)).Bind(options));
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>(provider =>
            {
                var location = Assembly.GetEntryAssembly()?.Location;

                var currentDirectory = Path.GetDirectoryName(location);
                if (currentDirectory == null) throw new FileNotFoundException();

                var options = provider.GetRequiredService<IOptions<FileLoggerOptions>>();

                var directoryLog = Path.Combine(currentDirectory, options.Value.Directory);
                if (!Directory.Exists(directoryLog))
                {
                    Directory.CreateDirectory(directoryLog);
                }

                return new FileLoggerProvider(directoryLog);
            }));

            return builder;
        }
    }
}