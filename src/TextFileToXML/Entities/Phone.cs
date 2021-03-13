using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TextFileToXML.Entities
{
    public class Phone
    {
        [XmlElement(ElementName = "mobile")]
        public string Mobile { get; set; }
        [XmlElement(ElementName = "landline")]
        public string Landline { get; set; }

        public Phone()
        {

        }

        public Phone(string mobile, string landline = null)
        {
            Mobile = mobile;
            Landline = landline;
        }
    }
}
