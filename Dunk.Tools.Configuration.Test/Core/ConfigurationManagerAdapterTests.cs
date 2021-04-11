using System;
using Dunk.Tools.Configuration.Core;
using Dunk.Tools.Configuration.Test.ConfigStubs;
using Dunk.Tools.Configuration.Test.TestUtilities;
using Dunk.Tools.Configuration.Utilities;
using NUnit.Framework;

namespace Dunk.Tools.Configuration.Test.Core
{
    [TestFixture]
    public class ConfigurationManagerAdapterTests
    {
        private const string EmptyConfigFile = "TestConfigs\\EmptyApp.config";
        private const string EmptySectionConfigFile = "TestConfigs\\EmptySectionsApp.config";
        private const string SingleValueAppConfigFile = "TestConfigs\\SingleValueApp.config";
        private const string CustomSectionAppConfigFile = "TestConfigs\\CustomSectionApp.config";
        private const string ValuesForGivenTypeAppConfigFile = "TestConfigs\\ValuesForGivenTypeApp.config";


        [Test]
        public void ConfigManagerReturnsNoAppSettingsForEmptyConfig()
        {
            ConfigurationFileLoader.LoadConfigurationFile(EmptyConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.AreEqual(0, configManager.AppSettings.Count);
        }

        [Test]
        public void ConfigManagerReturnsNoAppSettingsForEmptySectionConfig()
        {
            ConfigurationFileLoader.LoadConfigurationFile(EmptySectionConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.AreEqual(0, configManager.AppSettings.Count);
        }

        [Test]
        public void ConfigManagerReturnsAppSettingsForConfigWithAppSettings()
        {
            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.AreEqual(1, configManager.AppSettings.Count);
        }

        [Test]
        public void ConfigManagerReturnsSpecifiedAppSettingValueFromConfig()
        {
            const string testKey = "TestKey";
            const string expectedTestValue = "TestValue";

            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var value = configManager.AppSettings[testKey];

            Assert.AreEqual(expectedTestValue, value);
        }

        [Test]
        public void ConfigManagerReturnsConnectionStringsForConfigWithConnectionStrings()
        {
            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.AreEqual(1, configManager.ConnectionStrings.Count);
        }

        [Test]
        public void ConfigManagerReturnsSpecifiedConnectionStringFromConfig()
        {
            const string testConnection = "TestConnection";
            const string expectedConnectionString = "Data Source=TestServerAddress;Initial Catalog=TestDataBase; Integrated Security=SSPI";

            ConfigurationFileLoader.LoadConfigurationFile(SingleValueAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var connection = configManager.ConnectionStrings[testConnection];

            Assert.AreEqual(expectedConnectionString, connection.ConnectionString);
        }

        [Test]
        public void ConfigManagerReturnsCustomConfigSectionFromConfig()
        {
            const string testCustomConfigSection = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);

            Assert.IsNotNull(customSection);
        }

        [Test]
        public void ConfigManagerReturnsSpecifiedValueFromCustomConfigSection()
        {
            const string testCustomConfigSection = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);
            bool isGlobal = customSection.Global;

            Assert.IsTrue(isGlobal);
        }

        [Test]
        public void ConfigManagerReturnsCustomConfigElementFromConfig()
        {
            const string testCustomConfigSection = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);

            Assert.IsNotNull(customSection.TestElement);
        }

        [Test]
        public void ConfigManagerReturnsSpecifiedValueFromCustomConfigElement()
        {
            const string testCustomConfigSection = "testSection";
            const int expectedSize = 13;

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var customSection = configManager.GetSection<TestConfigSection>(testCustomConfigSection);

            Assert.AreEqual(expectedSize, customSection.TestElement.Size);
        }

        [Test]
        public void ConfigManagerReturnsAppSettingAsSpecifiedType()
        {
            const string testKey = "int value";
            const int expectedValue = 123;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            int value = configManager.GetAppSettingsAsType<int>(testKey);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void ConfigManagerThrowsIfAppSettingsKeyIsNull()
        {
            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.Throws<ArgumentNullException>(() => configManager.GetAppSettingsAsType<int>(null));
        }

        [Test]
        public void ConfigManagerThrowsIfAppSettingsKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.Throws<ArgumentException>(() => configManager.GetAppSettingsAsType<int>(invalidTestKey));
        }

        [Test]
        public void ConfigManagerThrowsIfAppSettingValueCannotBeConvertedToType()
        {
            const string testKey = "int value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.Throws<ConfigurationParsingException>(() => configManager.GetAppSettingsAsType<DateTime>(testKey));
        }

        [Test]
        public void ConfigManagerThrowsSerialisableExceptionIfAppSettingValueCannotBeConvertedToType()
        {
            const string testKey = "int value";
            ConfigurationParsingException error = null;

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            try
            {
                configManager.GetAppSettingsAsType<DateTime>(testKey);
            }
            catch(ConfigurationParsingException ex)
            {
                error = TestSerialisationHelper.SerialiseAndDeserialiseException(ex);
            }
            Assert.IsNotNull(error);
        }

        [Test]
        public void ConfigManagerReturnsDefaultValueTypeIfAppSettingKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<int>(invalidTestKey);

            Assert.AreEqual(default(int), result);
        }

        [Test]
        public void ConfigManagerReturnsDefaultValueTypeIfAppSettingValueCannotBeConvertedToType()
        {
            const string testKey = "int value";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<DateTime>(testKey);

            Assert.AreEqual(default(DateTime), result);
        }

        [Test]
        public void ConfigManagerReturnsSpecifiedDefaultValueIfAppSettingKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<bool>(invalidTestKey, true);

            Assert.IsTrue(result);
        }

        [Test]
        public void ConfigManagerReturnsSpecifiedDefaultValueFromFactoryIfAppSettingKeyIsNotPresentInConfig()
        {
            const string invalidTestKey = "foo bar key";

            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var result = configManager.GetAppSettingsAsTypeOrDefault<bool>(invalidTestKey, () => true);

            Assert.IsTrue(result);
        }

        [Test]
        public void ConfigurationManagerGetAppSettingsAsTypeOrDefaultThrowsIfKeyIsNull()
        {
            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.Throws<ArgumentNullException>(() => configManager.GetAppSettingsAsTypeOrDefault<bool>(null));
        }

        [Test]
        public void ConfigurationManagerGetAppSettingsAsTypeOrDefaultThrowsIfKeyIsEmpty()
        {
            ConfigurationFileLoader.LoadConfigurationFile(ValuesForGivenTypeAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            Assert.Throws<ArgumentNullException>(() => configManager.GetAppSettingsAsTypeOrDefault<bool>(string.Empty));
        }
    }
}
