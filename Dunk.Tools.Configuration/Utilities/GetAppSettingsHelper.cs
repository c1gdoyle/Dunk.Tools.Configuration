using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Dunk.Tools.Configuration.Utilities
{
    /// <summary>
    /// A helper class responsible for retrieving a specified AppSetting as a
    /// specified type.
    /// </summary>
    internal static class GetAppSettingsHelper
    {
        /// <summary>
        /// Gets a specific AppSetting as a specified type given the AppSettings and the key.
        /// </summary>
        /// <typeparam name="T">The type that is being requested.</typeparam>
        /// <param name="settings">The configuration AppSettings.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as the type then the converted value; otherwise an exception will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> was null or empty.</exception>
        /// <exception cref="ArgumentException">Configuration does not contain any App settings for given key.</exception>
        /// <exception cref="ConfigurationParsingException">App setting could not be resovled to an instance of T.</exception>
        internal static T AsType<T>(NameValueCollection settings, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), $"{nameof(key)} parameter cannot be null.");
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            var value = settings[key];

            if (value == null)
            {
                throw new ArgumentException($"No configuration setting found for key {key}", nameof(key));
            }

            try
            {
                return (T)converter.ConvertFromString(value);
            }
            catch (Exception ex)
            {
                throw new ConfigurationParsingException($"Unable to convert configuration setting {key} to type {typeof(T).FullName}", ex);
            }
        }

        /// <summary>
        /// Gets a specific AppSetting as a specified type, or default value for that type, given the AppSettings and the key.
        /// </summary>
        /// <typeparam name="T">The type that is being requested.</typeparam>
        /// <param name="settings">The configuration AppSettings.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <returns>
        /// If able to return as the type then the converted value; otherwise default value for the type.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> was null or empty.</exception>
        internal static T AsTypeOrDefault<T>(NameValueCollection settings, string key)
        {
            return AsTypeOrDefault(settings, key, default(T));
        }

        /// <summary>
        /// Gets a specific AppSetting as a specified type, or default value for that type, given the AppSettings, the key
        /// and a default value for the type.
        /// </summary>
        /// <typeparam name="T">The type that is being requested.</typeparam>
        /// <param name="settings">The configuration AppSettings.</param>
        /// <param name="key">The key of the AppSetting to retrieve.</param>
        /// <param name="defaultValue">The default instance of T.</param>
        /// <returns>
        /// If able to return as the type then the converted value; otherwise <paramref name="defaultValue"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> was null or empty.</exception>
        internal static T AsTypeOrDefault<T>(NameValueCollection settings, string key, T defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), $"{nameof(key)} parameter cannot be null");
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            var value = settings[key];

            if (value == null)
            {
                return defaultValue;
            }

            try
            {
                return (T)converter.ConvertFromString(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
