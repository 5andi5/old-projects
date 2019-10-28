using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CorelEstimator.Extensions
{
    public static class XElementExtensions
    {
        public static XNamespace Namespace = "http://www.w3.org/2000/svg";

        public static IEnumerable<XElement> NsElements(this XElement element, string name)
        {
            return element.Elements(Namespace+ name);
        }

        public static XElement NsElement(this XDocument element, string name)
        {
            return element.Element(Namespace + name);
        }
    }
}
