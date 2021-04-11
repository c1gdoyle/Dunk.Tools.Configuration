using Dunk.Tools.Configuration.Core;
using Dunk.Tools.Configuration.Extensions;
using Dunk.Tools.Configuration.Test.TestUtilities;
using NUnit.Framework;

namespace Dunk.Tools.Configuration.Test.Extensions
{
    [TestFixture]
    public class ConfigManagerExtensionsTests
    {
        private const string ValuesForGivenTypeAppConfigFile = "TestConfigs\\ValuesForGivenTypeApp.config";

        [Test]
        public void ConfigManagerReturnsShortValue()
        {
            const string testKey = "short value";
            const short expectedValue = 123;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            short value = configManager.GetAppSettingsAsInt16(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void ConfigManagerReturnsDefaultShortValue()
        {
            const string testKey = "foo bar value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            short value = configManager.GetAppSettingsAsInt16OrDefault(testKey);

            Assert.AreEqual(default(short), value);
        }

        [Test]
        public void ConfigManagerReturnsIntValue()
        {
            const string testKey = "int value";
            const int expectedValue = 123;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            int value = configManager.GetAppSettingsAsInt32(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void ConfigManagerReturnsDefaultIntValue()
        {
            const string testKey = "foo bar value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            int value = configManager.GetAppSettingsAsInt32OrDefault(testKey);

            Assert.AreEqual(default(int), value);
        }

        [Test]
        public void ConfigManagerReturnsLongValue()
        {
            const string testKey = "long value";
            const long expectedValue = 4294967296;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            long value = configManager.GetAppSettingsAsInt64(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void ConfigManagerReturnsDefaultLongValue()
        {
            const string testKey = "foo bar value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            long value = configManager.GetAppSettingsAsInt64OrDefault(testKey);

            Assert.AreEqual(default(long), value);
        }

        [Test]
        public void ConfigManagerReturnsDoubleValue()
        {
            const string testKey = "double value";
            const double expectedValue = 123.4;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            double value = configManager.GetAppSettingsAsDouble(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void ConfigManagerReturnsDefaultDoubleValue()
        {
            const string testKey = "foo bar value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            double value = configManager.GetAppSettingsAsDoubleOrDefault(testKey);

            Assert.AreEqual(default(double), value);
        }

        [Test]
        public void ConfigManagerReturnsFloatValue()
        {
            const string testKey = "double value";
            const float expectedValue = 123.4f;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            float value = configManager.GetAppSettingsAsFloat(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void ConfigManagerReturnsDefaultFloatValue()
        {
            const string testKey = "foo bar value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            float value = configManager.GetAppSettingsAsFloatOrDefault(testKey);

            Assert.AreEqual(default(float), value);
        }

        [Test]
        public void ConfigManagerReturnsBooleanValue()
        {
            const string testKey = "boolean value";
            const bool expectedValue = true;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            bool value = configManager.GetAppSettingsAsBoolean(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void ConfigManagerReturnsDefaultBooleanValue()
        {
            const string testKey = "foo bar value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            bool value = configManager.GetAppSettingsAsBooleanOrDefault(testKey);

            Assert.AreEqual(default(bool), value);
        }
    }
}
