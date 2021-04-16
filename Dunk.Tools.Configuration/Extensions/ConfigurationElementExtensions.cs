using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Dunk.Tools.Configuration.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for converting a <see cref="ConfigurationElement"/> instance into
    /// a LINQ-to-XML equivalent.
    /// </summary>
    public static class ConfigurationElementExtensions
    {
        /// <summary>
        /// Converts a <see cref="ConfigurationSection"/> instance into a LINQ-to-XML <see cref="XElement"/>.
        /// </summary>
        /// <param name="configSection">The configuration section.</param>
        /// <returns>
        /// A <see cref="XElement"/> containing the details of the <paramref name="configSection"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configSection"/> was null.</exception>
        public static XElement ToXElement(this ConfigurationSection configSection)
        {
            if (configSection == null)
            {
                throw new ArgumentNullException(nameof(configSection));
            }
            string xElName = !string.IsNullOrEmpty(configSection.SectionInformation.Name) ?
                configSection.SectionInformation.Name : configSection.GetType().Name;

            XElement element = new XElement(xElName);

            ParseElementDetails(element, configSection);

            return element;
        }

        /// <summary>
        /// Converts a <see cref="ConfigurationElement"/> instance into a LINQ-to-XML <see cref="XElement"/>.
        /// </summary>
        /// <param name="configElement">The configuration element.</param>
        /// <returns>
        /// A <see cref="XElement"/> containing the details of the <paramref name="configElement"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="configElement"/> was null.</exception>
        public static XElement ToXElement(this ConfigurationElement configElement)
        {
            if(configElement == null)
            {
                throw new ArgumentNullException(nameof(configElement));
            }
            XElement element = new XElement(configElement.GetType().Name);

            ParseElementDetails(element, configElement);

            return element;
        }

        private static void ParseElementDetails(XElement element, ConfigurationElement configElement)
        {
            var propertyMap = GetProperties(configElement);

            var attributes = ParseAttributes(configElement, propertyMap.AttributeProperties);
            var subElements = ParseSubElements(configElement, propertyMap.SubElementsProperties);

            element.Add(attributes.ToArray());
            element.Add(subElements.ToArray());

        }

        private static List<XAttribute> ParseAttributes(ConfigurationElement configElement, IEnumerable<PropertyInfo> attributeProps)
        {
            List<XAttribute> attributes = new List<XAttribute>();
            foreach (var attributeProp in attributeProps)
            {
                string name = attributeProp.GetCustomAttribute<ConfigurationPropertyAttribute>().Name;
                object value = attributeProp.GetValue(configElement);

                XAttribute attribute = new XAttribute(name, value);

                attributes.Add(attribute);
            }
            return attributes;
        }

        private static List<XElement> ParseSubElements(ConfigurationElement configElement, IEnumerable<PropertyInfo> subElementProps)
        {
            List<XElement> elements = new List<XElement>();
            foreach (var subElProp in subElementProps)
            {
                string subElementName = subElProp.GetCustomAttribute<ConfigurationPropertyAttribute>().Name;
                ConfigurationElement subConfigEl = (ConfigurationElement)subElProp.GetValue(configElement);

                XElement subElement = subConfigEl.ToXElement();
                if (subConfigEl is ConfigurationElementCollection)
                {
                    var subCollectionElements = ParseConfigCollectionSubElements(subConfigEl as ConfigurationElementCollection, subElProp);
                    subCollectionElements.ForEach(sub => subElement.Add(sub));
                }
                subElement.Name = subElementName;
                elements.Add(subElement);
            }
            return elements;
        }

        private static List<XElement> ParseConfigCollectionSubElements(ConfigurationElementCollection configCollectionEl, PropertyInfo configCollectionProperty)
        {
            List<XElement> subElements = new List<XElement>();
            string elementName = GetCollectionElementName(configCollectionProperty);

            foreach (ConfigurationElement configCollectionSubEl in configCollectionEl)
            {
                string subElementName = !string.IsNullOrEmpty(elementName) ? elementName : configCollectionSubEl.GetType().Name;
                XElement subElement = configCollectionSubEl.ToXElement();
                subElement.Name = subElementName;

                subElements.Add(subElement);
            }
            return subElements;
        }

        private static string GetCollectionElementName(PropertyInfo configCollectionProperty)
        {
            //try to get the AddItem name
            var configCollectionAttr = configCollectionProperty.GetCustomAttribute<ConfigurationCollectionAttribute>();
            if (configCollectionAttr != null &&
                !string.IsNullOrEmpty(configCollectionAttr.AddItemName))
            {
                return configCollectionAttr.AddItemName;
            }
            return null;
        }

        private static ConfigElementPropertyMap GetProperties(ConfigurationElement configElement)
        {
            IEnumerable<PropertyInfo> properties = configElement.GetType().GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(ConfigurationPropertyAttribute)));

            IEnumerable<PropertyInfo> attributes = properties
                .Where(p => !typeof(ConfigurationElement).IsAssignableFrom(p.PropertyType));

            IEnumerable<PropertyInfo> subElementsProp = properties
                .Where(p => typeof(ConfigurationElement).IsAssignableFrom(p.PropertyType));

            return new ConfigElementPropertyMap
            {
                AttributeProperties = attributes,
                SubElementsProperties = subElementsProp
            };
        }

        private class ConfigElementPropertyMap
        {
            public IEnumerable<PropertyInfo> AttributeProperties { get; set; }

            public IEnumerable<PropertyInfo> SubElementsProperties { get; set; }
        }
    }
}
