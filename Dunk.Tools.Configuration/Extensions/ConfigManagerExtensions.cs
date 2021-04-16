using System;
using Dunk.Tools.Configuration.Base;

namespace Dunk.Tools.Configuration.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for a <see cref="IConfigurationManager"/> instance.
    /// </summary>
    public static class ConfigManagerExtensions
    {
        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="short"/>.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="short"/> then the converted value; otherwise an exception will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static short GetAppSettingsAsInt16(this IConfigurationManager configManager, string key)
        {
            if(configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsType<short>(key);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="short"/> or the default value for a Short.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="short"/> then the converted value; otherwise default value for a Short.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static short GetAppSettingsAsInt16OrDefault(this IConfigurationManager configManager, string key)
        {
            return GetAppSettingsAsInt16OrDefault(configManager, key, default);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="short"/> or a default value.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <param name="defaultValue">The default.</param>
        /// <returns>
        /// If able to return as a <see cref="short"/> then the converted value; otherwise default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static short GetAppSettingsAsInt16OrDefault(this IConfigurationManager configManager, string key, short defaultValue)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsTypeOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="int"/>.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="int"/> then the converted value; otherwise an exception will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static int GetAppSettingsAsInt32(this IConfigurationManager configManager, string key)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsType<int>(key);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="int"/> or the default value for a Int.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="int"/> then the converted value; otherwise default value for a Int.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static int GetAppSettingsAsInt32OrDefault(this IConfigurationManager configManager, string key)
        {
            return GetAppSettingsAsInt32OrDefault(configManager, key, default);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="int"/> or a default value.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <param name="defaultValue">The default.</param>
        /// <returns>
        /// If able to return as a <see cref="int"/> then the converted value; otherwise default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static int GetAppSettingsAsInt32OrDefault(this IConfigurationManager configManager, string key, int defaultValue)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsTypeOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="long"/>.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="long"/> then the converted value; otherwise an exception will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static long GetAppSettingsAsInt64(this IConfigurationManager configManager, string key)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsType<long>(key);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="long"/> or the default value for a Long.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="long"/> then the converted value; otherwise default value for a Long.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static long GetAppSettingsAsInt64OrDefault(this IConfigurationManager configManager, string key)
        {
            return GetAppSettingsAsInt64OrDefault(configManager, key, default);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="long"/> or a default value.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <param name="defaultValue">The default.</param>
        /// <returns>
        /// If able to return as a <see cref="long"/> then the converted value; otherwise default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static long GetAppSettingsAsInt64OrDefault(this IConfigurationManager configManager, string key, long defaultValue)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsTypeOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="double"/>.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="double"/> then the converted value; otherwise an exception will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static double GetAppSettingsAsDouble(this IConfigurationManager configManager, string key)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsType<double>(key);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="double"/> or the default value for a Double.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="double"/> then the converted value; otherwise default value for a Double.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static double GetAppSettingsAsDoubleOrDefault(this IConfigurationManager configManager, string key)
        {
            return GetAppSettingsAsDoubleOrDefault(configManager, key, default);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="double"/> or a default value.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <param name="defaultValue">The default.</param>
        /// <returns>
        /// If able to return as a <see cref="double"/> then the converted value; otherwise default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static double GetAppSettingsAsDoubleOrDefault(this IConfigurationManager configManager, string key, double defaultValue)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsTypeOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="float"/>.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="float"/> then the converted value; otherwise an exception will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static float GetAppSettingsAsFloat(this IConfigurationManager configManager, string key)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsType<float>(key);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="float"/> or the default value for a Float.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="float"/> then the converted value; otherwise default value for a Float.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static float GetAppSettingsAsFloatOrDefault(this IConfigurationManager configManager, string key)
        {
            return GetAppSettingsAsFloatOrDefault(configManager, key, default);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="float"/> or a default value.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <param name="defaultValue">The default.</param>
        /// <returns>
        /// If able to return as a <see cref="float"/> then the converted value; otherwise default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static float GetAppSettingsAsFloatOrDefault(this IConfigurationManager configManager, string key, float defaultValue)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsTypeOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="bool"/>.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="bool"/> then the converted value; otherwise an exception will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static bool GetAppSettingsAsBoolean(this IConfigurationManager configManager, string key)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsType<bool>(key);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="bool"/> or the default value for a Boolean.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as a <see cref="bool"/> then the converted value; otherwise default value for a Boolean.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static bool GetAppSettingsAsBooleanOrDefault(this IConfigurationManager configManager, string key)
        {
            return GetAppSettingsAsBooleanOrDefault(configManager, key, default);
        }

        /// <summary>
        /// Retrieves a specific AppSetting as a <see cref="bool"/> or a default value.
        /// </summary>
        /// <param name="configManager">The config manager.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <param name="defaultValue">The default.</param>
        /// <returns>
        /// If able to return as a <see cref="bool"/> then the converted value; otherwise default value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configManager"/> was null.</exception>
        public static bool GetAppSettingsAsBooleanOrDefault(this IConfigurationManager configManager, string key, bool defaultValue)
        {
            if (configManager == null)
            {
                throw new ArgumentNullException(nameof(configManager),
                    $"Unable to retrieve app-setting, {nameof(configManager)} parameter cannot be null.");
            }
            return configManager.GetAppSettingsAsTypeOrDefault<bool>(key, defaultValue);
        }
    }
}
