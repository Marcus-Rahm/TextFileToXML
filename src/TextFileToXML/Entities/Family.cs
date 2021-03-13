using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TextFileToXML.Entities
{
    public class Family
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "born")]
        public string Born { get; set; }
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "phone")]
        public Phone Phone { get; set; }

        public Family()
        {

        }

        public Family(string name, string born = null)
        {
            Name = name;
            Born = born;
        }
    }
}
