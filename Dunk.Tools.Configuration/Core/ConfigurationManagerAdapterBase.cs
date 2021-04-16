using System;
using System.Collections.Specialized;
using System.Configuration;
using Dunk.Tools.Configuration.Base;

namespace Dunk.Tools.Configuration.Core
{
    /// <summary>
    /// An abstract base class for types that implement <see cref="IConfigurationManager"/>.
    /// </summary>
    public abstract class ConfigurationManagerAdapterBase : IConfigurationManager
    {
        /// <inheritdoc />
        public abstract NameValueCollection AppSettings { get; }

        /// <inheritdoc />
        public abstract ConnectionStringSettingsCollection ConnectionStrings { get; }

        /// <inheritdoc />
        public abstract T GetSection<T>(string sectionName);

        /// <inheritdoc />
        public T GetAppSettingsAsType<T>(string key)
        {
            return Utilities.GetAppSettingsHelper.AsType<T>(AppSettings, key);
        }

        /// <inheritdoc />
        public T GetAppSettingsAsTypeOrDefault<T>(string key)
        {
            return Utilities.GetAppSettingsHelper.AsTypeOrDefault<T>(AppSettings, key);
        }

        /// <inheritdoc />
        public T GetAppSettingsAsTypeOrDefault<T>(string key, T defaultValue)
        {
            return Utilities.GetAppSettingsHelper.AsTypeOrDefault(AppSettings, key, defaultValue);
        }

        /// <inheritdoc />
        public T GetAppSettingsAsTypeOrDefault<T>(string key, Func<T> defaultFactory)
        {
            if(defaultFactory == null)
            {
                throw new ArgumentNullException(nameof(defaultFactory),
                    $"Unable to retrieve app-setting or default, {nameof(defaultFactory)} parameter cannot be null.");
            }
            return Utilities.GetAppSettingsHelper.AsTypeOrDefault(AppSettings, key, defaultFactory());
        }
    }
}
