using System.Configuration;

namespace Dunk.Tools.Configuration.Test.ConfigStubs
{
    public class TestUrlConfigElement : ConfigurationElement
    {
        public TestUrlConfigElement()
        {
        }

        public TestUrlConfigElement(string name, string url, int port)
        {
        }

        [ConfigurationProperty("name", DefaultValue = "testurl", IsRequired = false)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", DefaultValue = "http://www.testurl.com", IsRequired = false)]
        [RegexStringValidator(@"\w+:\/\/[\w]+\S*")]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 8080, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 8080, ExcludeRange = false)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }
    }
}
