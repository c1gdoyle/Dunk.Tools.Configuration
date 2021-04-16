using System;
using System.Configuration;
using System.Linq;
using Dunk.Tools.Configuration.Core;
using Dunk.Tools.Configuration.Extensions;
using Dunk.Tools.Configuration.Test.ConfigStubs;
using Dunk.Tools.Configuration.Test.TestUtilities;
using NUnit.Framework;

namespace Dunk.Tools.Configuration.Test.Extensions
{
    [TestFixture]
    public class ConfigurationElementExtensionsTests
    {
        private const string CustomSectionAppConfigFile = "TestConfigs\\CustomSectionApp.config";
        private const string CustomConfigCollectionAppConfigFile = "TestConfigs\\CustomConfigCollectionApp.config";

        private const string TestConfigElementXml =
            "<testSection global=\"true\">\r\n" +
            "  <testElement size=\"13\" />\r\n" +
            "</testSection>";

        private const string TestConfigCollectionElementXml =
                "<testSection global=\"true\">\r\n" +
                "  <testElement size=\"13\" />\r\n" +
                "  <urls>\r\n" +
                "    <url name=\"url1\" url=\"http://www.testurl1.com\" port=\"4041\" />\r\n" +
                "    <url name=\"url2\" url=\"http://www.testurl2.com\" port=\"4042\" />\r\n" +
                "    <url name=\"url3\" url=\"http://www.testurl3.com\" port=\"4043\" />\r\n" +
                "    <url name=\"url4\" url=\"http://www.testurl4.com\" port=\"4044\" />\r\n" +
                "  </urls>\r\n" +
                "</testSection>";

        [Test]
        public void ConfigSectionToXElementThrowsIfConfigSectionIsNull()
        {
            TestConfigSection configSection = null;

            Assert.Throws<ArgumentNullException>(() => configSection.ToXElement());
        }

        [Test]
        public void ConfigurationElementToXElementThrowsIfConfigurationElementIsNull()
        {
            ConfigurationElement configElement = null;

            Assert.Throws<ArgumentNullException>(() => configElement.ToXElement());
        }

        [Test]
        public void ConfigSectionConvertsToXElement()
        {
            var configSection = new TestConfigSection();

            var xEl = configSection.ToXElement();

            Assert.IsNotNull(xEl);
        }

        [Test]
        public void XElementContainsConfigSectionNames()
        {
            var configSection = new TestConfigSection();

            var xEl = configSection.ToXElement();

            Assert.AreEqual("TestConfigSection", xEl.Name.ToString());
        }

        [Test]
        public void XElementContainsConfigSectionInformationNameIfAvailable()
        {
            const string testSectionName = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var configSection = configManager.GetSection<TestConfigSection>(testSectionName);

            var xEl = configSection.ToXElement();

            Assert.AreEqual(testSectionName, xEl.Name.ToString());
        }

        [Test]
        public void XElementMatchesConfigurationSection()
        {
            const string testSectionName = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomSectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var configSection = configManager.GetSection<TestConfigSection>(testSectionName);

            var xEl = configSection.ToXElement();

            Assert.AreEqual(TestConfigElementXml, xEl.ToString());
        }

        [Test]
        public void XElementContainsConfigSectionAttributes()
        {
            var configSection = new TestConfigSection
            {
                Global = true
            };

            var xEl = configSection.ToXElement();
            var attribute = xEl.Attributes("global")
                .FirstOrDefault();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("true", attribute.Value);
        }

        [Test]
        public void XElementContainsConfigSectionSubElements()
        {
            var configSection = new TestConfigSection
            {
                Global = true,
                TestElement = new TestConfigElement { Size = 10 }
            };

            var xEl = configSection.ToXElement();

            var subElement = xEl.Elements().FirstOrDefault(e => e.Name == "testElement");

            Assert.IsNotNull(subElement);
        }

        [Test]
        public void XElementContainsConfigSectionSubElementAttributes()
        {
            var configSection = new TestConfigSection
            {
                Global = true,
                TestElement = new TestConfigElement { Size = 10 }
            };

            var xEl = configSection.ToXElement();

            var subElement = xEl.Elements().FirstOrDefault(e => e.Name == "testElement");
            Assert.AreEqual("10", subElement.Attribute("size").Value);
        }

        [Test]
        public void XElementContainsConfigCollectionSubElements()
        {
            const string testSectionName = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomConfigCollectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var configSection = configManager.GetSection<TestConfigSectionCollection>(testSectionName);

            var xEl = configSection.ToXElement();

            var subElement = xEl.Elements().FirstOrDefault(e => e.Name == "urls");
            Assert.IsNotNull(subElement);
        }

        [Test]
        public void XElementContainsConfigCollectionWithCollectionElements()
        {
            const string testSectionName = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomConfigCollectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var configSection = configManager.GetSection<TestConfigSectionCollection>(testSectionName);

            var xEl = configSection.ToXElement();

            var subElement = xEl.Elements().FirstOrDefault(e => e.Name == "urls");

            Assert.AreEqual(4, subElement.Elements().Count());
        }

        [Test]
        public void XElementContainsCollectionElementName()
        {
            const string testSectionName = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomConfigCollectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var configSection = configManager.GetSection<TestConfigSectionCollection>(testSectionName);

            var xEl = configSection.ToXElement();

            var collectionElement = xEl.Elements().FirstOrDefault(e => e.Name == "urls");
            var subElement = collectionElement.Elements().FirstOrDefault();

            Assert.AreEqual("url", subElement.Name.LocalName);
        }

        [Test]
        public void XElementContainsCollectionElementAttributes()
        {
            const string testSectionName = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomConfigCollectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var configSection = configManager.GetSection<TestConfigSectionCollection>(testSectionName);

            var xEl = configSection.ToXElement();

            var collectionElement = xEl.Elements().FirstOrDefault(e => e.Name == "urls");
            var subElement = collectionElement.Elements().FirstOrDefault();

            Assert.AreEqual("url1", subElement.Attribute("name").Value);
            Assert.AreEqual("http://www.testurl1.com", subElement.Attribute("url").Value);
            Assert.AreEqual("4041", subElement.Attribute("port").Value);
        }

        [Test]
        public void XElementMatchesConfigurationSectionWithCollectionElement()
        {
            const string testSectionName = "testSection";

            ConfigurationFileLoader.LoadConfigurationFile(CustomConfigCollectionAppConfigFile);
            var configManager = new ConfigurationManagerAdapter();

            var configSection = configManager.GetSection<TestConfigSectionCollection>(testSectionName);

            var xEl = configSection.ToXElement();

            Assert.AreEqual(TestConfigCollectionElementXml, xEl.ToString());
        }
    }
}
