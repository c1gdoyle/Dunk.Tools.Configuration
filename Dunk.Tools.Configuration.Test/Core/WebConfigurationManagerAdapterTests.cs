using System;
using Dunk.Tools.Configuration.Core;
using Dunk.Tools.Configuration.Test.ConfigStubs;
using Dunk.Tools.Configuration.Test.TestUtilities;
using Dunk.Tools.Configuration.Utilities;
using NUnit.Framework;

namespace Dunk.Tools.Configuration.Test.Core
{
    [TestFixture]
    public class WebConfigurationManagerAdapterTests
    {
        private const string EmptyConfigFile = "TestConfigs\\EmptyApp.config";
        private const string EmptySectionConfigFile = "TestConfigs\\EmptySectionsApp.config";
        private const string SingleValueAppConfigFile = "TestConfigs\\SingleValueApp.config";
        private const string CustomSectionAppConfigFile = "TestConfigs\\CustomSectionApp.config";
        private const string ValuesForGivenTypeAppConfigFile = "TestConfigs\\ValuesForGivenTypeApp.config";

        [Test]
        public void WebConfigManagerReturnsNoAppSettingsForEmptyConfig()
        {
            ConfigurationFileLoader.LoadConfigurationFile(EmptyConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.AreEqual(0, configManager.AppSettings.Count);
        }

        [Test]
        public void WebConfigManagerReturnsNoAppSettingsForEmptySectionConfig()
        {
            ConfigurationFileLoader.LoadConfigurationFile(EmptySectionConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.AreEqual(0, configManager.AppSettings.Count);
        }

        [Test]
        public void WebConfigManagerReturnsAppSettingsForConfigWithAppSettings()
        {
            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.AreEqual(1, configManager.AppSettings.Count);
        }

        [Test]
        public void WebConfigManagerReturnsSpecifiedAppSettingValueFromConfig()
        {
            const string testKey = "TestKey";
            const string expectedTestValue = "TestValue";

            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var value = configManager.AppSettings[testKey];

            Assert.AreEqual(expectedTestValue, value);
        }

        [Test]
        public void WebConfigManagerReturnsConnectionStringsForConfigWithConnectionStrings()
        {
            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.AreEqual(1, configManager.ConnectionStrings.Count);
        }

        [Test]
        public void WebConfigManagerReturnsSpecifiedConnectionStringFromConfig()
        {
            const string testConnection = "TestConnection";
            const string expectedConnectionString = "Data Source=TestServerAddress;Initial Catalog=TestDataBase; Integrated Security=SSPI";

            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var connection = configManager.ConnectionStrings[testConnection];

            Assert.AreEqual(expectedConnectionString, connection.ConnectionString);
        }

        [Test]
        public void WebConfigManagerReturnsCustomConfigSectionFromConfig()
        {
            const string testCustomConfigSection = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);

            Assert.IsNotNull(customSection);
        }

        [Test]
        public void WebConfigManagerReturnsSpecifiedValueFromCustomConfigSection()
        {
            const string testCustomConfigSection = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);
            bool isGlobal = customSection.Global;

            Assert.IsTrue(isGlobal);
        }

        [Test]
        public void WebConfigManagerReturnsCustomConfigElementFromConfig()
        {
            const string testCustomConfigSection = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);

            Assert.IsNotNull(customSection.TestElement);
        }

        [Test]
        public void WebConfigManagerReturnsSpecifiedValueFromCustomConfigElement()
        {
            const string testCustomConfigSection = "testSection";
            const int expectedSize = 13;

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);

            Assert.AreEqual(expectedSize, customSection.TestElement.Size);
        }

        [Test]
        public void WebConfigManagerReturnsAppSettingAsSpecifiedType()
        {
            const string testKey = "int value";
            const int expectedValue = 123;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            int value = configManager.GetAppSettingsAsType<int>(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void WebConfigManagerThrowsIfAppSettingsKeyIsNull()
        {
            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.Throws<ArgumentNullException>(() => configManager.GetAppSettingsAsType<int>(null));
        }

        [Test]
        public void WebConfigManagerThrowsIfAppSettingsKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.Throws<ArgumentException>(() => configManager.GetAppSettingsAsType<int>(invalidTestKey));
        }

        [Test]
        public void WebConfigManagerThrowsIfAppSettingValueCannotBeConvertedToType()
        {
            const string testKey = "int value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.Throws<ConfigurationParsingException>(() => configManager.GetAppSettingsAsType<DateTime>(testKey));
        }

        [Test]
        public void WebConfigManagerThrowsSerialisableExceptionIfAppSettingValueCannotBeConvertedToType()
        {
            const string testKey = "int value";
            ConfigurationParsingException error = null;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            try
            {
                configManager.GetAppSettingsAsType<DateTime>(testKey);
            }
            catch (ConfigurationParsingException ex)
            {
                error = TestSerialisationHelper.SerialiseAndDeserialiseException(ex);
            }
            Assert.IsNotNull(error);
        }

        [Test]
        public void WebConfigManagerReturnsDefaultValueTypeIfAppSettingKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<int>(invalidTestKey);

            Assert.AreEqual(default(int), result);
        }

        [Test]
        public void WebConfigManagerReturnsDefaultValueTypeIfAppSettingValueCannotBeConvertedToType()
        {
            const string testKey = "int value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<DateTime>(testKey);

            Assert.AreEqual(default(DateTime), result);
        }

        [Test]
        public void WebConfigManagerReturnsSpecifiedDefaultValueIfAppSettingKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<bool>(invalidTestKey, true);

            Assert.IsTrue(result);
        }

        [Test]
        public void WebConfigManagerReturnsSpecifiedDefaultValueFromFactoryIfAppSettingKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<bool>(invalidTestKey, () => true);

            Assert.IsTrue(result);
        }

        [Test]
        public void WebConfigurationManagerGetAppSettingsAsTypeOrDefaultThrowsIfKeyIsNull()
        {
            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.Throws<ArgumentNullException>(() => configManager.GetAppSettingsAsTypeOrDefault<bool>(null));
        }

        [Test]
        public void WebConfigurationManagerGetAppSettingsAsTypeOrDefaultThrowsIfKeyIsEmpty()
        {
            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new WebConfigurationManagerAdapter();

            Assert.Throws<ArgumentNullException>(() => configManager.GetAppSettingsAsTypeOrDefault<bool>(string.Empty));
        }
    }
}
