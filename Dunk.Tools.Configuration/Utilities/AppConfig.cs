using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Dunk.Tools.Configuration.Utilities
{
    /// <summary>
    /// Supports changing the default App config file at runtime.
    /// </summary>
    /// <remarks>
    /// Based on Daniel Hilgarth's post at
    /// 
    /// </remarks>
    public sealed class AppConfig : IDisposable
    {
        private readonly string _oldConfigFile = AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString();
        private bool _disposed;

        private AppConfig(string path)
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", path);
            ResetConfigMechanism();
        }

        /// <summary>
        /// Changes the AppDomain config to a specified file.
        /// </summary>
        /// <param name="path">The file to change the AppDomain config to.</param>
        /// <returns>
        /// An instance of <see cref="AppConfig"/>
        /// While the instance is alive the AppDomain config will be the specified file,
        /// once this instance is disposed the AppDomain config will be changed back to 
        /// the original.
        /// </returns>
        public static AppConfig Change(string path)
        {
            return new AppConfig(path);
        }

        private void ResetConfigMechanism()
        {
            typeof(ConfigurationManager)
                .GetField("s_initState", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, 0);

            typeof(ConfigurationManager)
                .GetField("s_configSystem", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, null);

            typeof(ConfigurationManager)
                .Assembly
                .GetTypes()
                .First(x => x.FullName == "System.Configuration.ClientConfigPaths")
                .GetField("s_current", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, null);
        }

        #region IDisposable Members
        /// <inheritdoc />
        public void Dispose()
        {
            if (!_disposed)
            {
                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", _oldConfigFile);
                ResetConfigMechanism();

                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Members
    }
}
