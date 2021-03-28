using System;

namespace Dunk.Tools.Configuration.Utilities
{
    /// <summary>
    /// An exception that is thrown  by a <see cref="Dunk.Tools.Configuration.Base.IConfigurationManager"/> class
    /// to indicate that can error occurred whilst parsing an AppSetting value.
    /// </summary>
    [Serializable]
    public sealed class ConfigurationParsingException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ConfigurationParsingException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the error.</param>
        public ConfigurationParsingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ConfigurationParsingException"/> class with a specified error
        /// message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ConfigurationParsingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
