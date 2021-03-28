using System.Configuration;

namespace Dunk.Tools.Configuration.Test.ConfigStubs
{
    public class TestUrlsCollection : ConfigurationElementCollection
    {
        public TestUrlsCollection()
        {
        }

        public new TestUrlConfigElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0)
                {
                    return null;
                }
                return (TestUrlConfigElement)BaseGet(name);
            }
        }

        public TestUrlConfigElement this[int index]
        {
            get { return (TestUrlConfigElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name.ToLower() == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TestUrlConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TestUrlConfigElement)element).Name;
        }
    }
}
