using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TextFileToXML.Entities
{
    [XmlType("person")]
    public class Person
    {
        [XmlElement(ElementName = "firstname")]
        public string Firstname { get; set; }
        [XmlElement(ElementName = "lastname")]
        public string Lastname { get; set; }
        [XmlElement(ElementName = "address")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "phone")]
        public Phone Phone { get; set; }
        [XmlElement(ElementName = "family")]
        public List<Family> Family { get; private set; }

        public Person()
        {
            Family = new List<Family>();
        }

        public Person(string firstName, string lastName = null)
        {
            Family = new List<Family>();
            Firstname = firstName;
            Lastname = lastName;
        }
    }
}
