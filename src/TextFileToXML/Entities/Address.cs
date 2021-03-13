using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TextFileToXML.Entities
{
    public class Address
    {
        [XmlElement(ElementName = "street")]
        public string Street { get; set; }
        [XmlElement(ElementName = "city")]
        public string City { get; set; }
        [XmlElement(ElementName = "zipcode")]
        public string ZIPCode { get; set; }

        public Address()
        {

        }

        public Address(string street, string city = null, string zipCode = null)
        {
            Street = street;
            City = city;
            ZIPCode = zipCode;
        }
    }
}
