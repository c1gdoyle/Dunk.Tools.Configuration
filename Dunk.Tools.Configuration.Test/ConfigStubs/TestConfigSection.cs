using System.Configuration;

namespace Dunk.Tools.Configuration.Test.ConfigStubs
{
    public class TestConfigSection : ConfigurationSection
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
    }
}
