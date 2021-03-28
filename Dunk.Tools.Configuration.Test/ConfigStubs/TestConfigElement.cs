using System.Configuration;

namespace Dunk.Tools.Configuration.Test.ConfigStubs
{
    public class TestConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("size", DefaultValue = "12", IsRequired = false)]
        public int Size
        {
            get { return (int)this["size"]; }
            set { this["size"] = value; }
        }
    }
}
