using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TextFileToXML.Entities
{
    [XmlRoot(ElementName="people")]
    public class People
    {
        [XmlElement(ElementName = "person")]
        public List<Person> Persons { get; private set; }

        public People()
        {
            Persons = new List<Person>();
        }
    }
}
