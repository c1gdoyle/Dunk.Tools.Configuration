using System.Configuration;

namespace Dunk.Tools.Configuration.Test.ConfigStubs
{
    public class TestConfigSectionCollection : ConfigurationSection
    {
        [ConfigurationProperty("global", DefaultValue = "false", IsRequired = false)]
        public bool Global
        {
            get { return (bool)this["global"]; }
            set { this["global"] = value; }
        }

        [ConfigurationProperty("testElement")]
        public TestConfigElement TestElement
        {
            get { return (TestConfigElement)this["testElement"]; }
            set { this["testElement"] = value; }
        }

        [ConfigurationProperty("urls", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(TestUrlsCollection), AddItemName = "url", ClearItemsName = "clear", RemoveItemName = "remove")]
        public TestUrlsCollection Urls
        {
            get
            {
                TestUrlsCollection collection = (TestUrlsCollection)this["urls"];
                return collection;
            }
            set
            {
                this["urls"] = value;
            }
        }
    }
}
