using System;
using System.IO;
using Dunk.Tools.Configuration.Utilities;

namespace Dunk.Tools.Configuration.Test.TestUtilities
{
    internal static class ConfigurationFileLoader
    {
        public static void LoadConfigurationFile(string config)
        {
            AppConfig.Change(ResolveFilePath(config));
        }

        private static string ResolveFilePath(string path)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(Path.Combine(currentPath, path));
        }
    }
}
