using System;
using Microsoft.Extensions.Hosting;

namespace TodoApi.Extensions
{
    internal static class HostEnvironmentEnvExtensions
    {
        /// <summary>
        /// Checks if the current host environment name is <see cref="Environments.Local"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name is <see cref="Environments.Local"/>, otherwise false.</returns>
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(Environments.Local);
        }
    }
}