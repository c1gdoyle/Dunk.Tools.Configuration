using System.Collections.Specialized;
using System.Configuration;
using System.Web.Configuration;
using Dunk.Tools.Configuration.Base;

namespace Dunk.Tools.Configuration.Core
{
    /// <summary>
    /// An implementation of <see cref="IConfigurationManager"/> that serves as a wrapper
    /// over this application's default <see cref="WebConfigurationManager"/>.
    /// </summary>
    public sealed class WebConfigurationManagerAdapter : ConfigurationManagerAdapterBase
    {
        /// <summary>
        /// Gets the <see cref="System.Configuration.AppSettingsSection"/> data for the current application's
        /// default configuration.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="NameValueCollection"/> object that contains the contents of the 
        /// <see cref="AppSettingsSection"/> object for the current application's default
        /// configuration.
        /// </returns>

        public override NameValueCollection AppSettings
        {
            get { return WebConfigurationManager.AppSettings; }
        }

        /// <summary>
        /// Gets the <see cref="System.Configuration.ConnectionStringsSection"/> data for the current application's
        /// default configuration.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="ConnectionStringSettingsCollection"/> object that contains the contents of the 
        /// <see cref="ConnectionStringsSection"/> object for the current application's default
        /// configuration.
        /// </returns>
        public override ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return WebConfigurationManager.ConnectionStrings; }
        }

        /// <summary>
        /// Retrieves a specified configuration section for the current application's default
        /// configuration.
        /// </summary>
        /// <typeparam name="T">The type associated with the configuration section.</typeparam>
        /// <param name="sectionName">The configuration section path and name.</param>
        /// <returns>
        /// The specified <see cref="ConfigurationSection"/> object, or null if the
        /// section does not exist.
        /// </returns>
        public override T GetSection<T>(string sectionName)
        {
            return (T)WebConfigurationManager.GetSection(sectionName);
        }
    }
}
