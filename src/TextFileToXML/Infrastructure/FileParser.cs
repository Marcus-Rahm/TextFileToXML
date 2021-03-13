using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TextFileToXML.Entities;

namespace TextFileToXML.Infrastructure
{
    public class FileParser
    {
        /// <summary>
        /// Reads stream line by line and creates up a XML representation of the data.
        /// </summary>
        /// <param name="stream">Stream containing text data which will become <see cref="Person"/> objects</param>
        /// <returns>XML representation of stream data</returns>
        public string ParseToXML(StreamReader stream)
        {
            People people = new People();

            string line;
            while ((line = stream.ReadLine()) != null)
            {
                ProcessLine(line, ref people);
            }

            using (var stringwriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(people.GetType());
                serializer.Serialize(stringwriter, people);
                return stringwriter.ToString();
            }
        }

        /// <summary>
        /// Reads stream line by line and creates up a XML representation of the data.
        /// </summary>
        /// <param name="stream">Stream containing text data which will become <see cref="Person"/> objects</param>
        /// <returns>XML representation of stream data</returns>
        public People ParseToPersonList(StreamReader stream)
        {
            People people = new People();

            string line;
            while ((line = stream.ReadLine()) != null)
            {
                ProcessLine(line, ref people);
            }

            return people;
        }

        /// <summary>
        /// Splits <paramref name="line"/> and reads the first value in the resulting array.
        /// Handles the different data based on the value.
        /// </summary>
        /// <param name="line">Line string containing data</param>
        /// <param name="people">List of all people objects</param>
        private void ProcessLine(string line, ref People people)
        {
            string[] lineData = line.Split('|');
            switch (lineData[0].ToLower())
            {
                case "p":
                    HandlePerson(lineData, ref people);
                    break;
                case "a":
                    HandleAddress(lineData, ref people);
                    break;
                case "t":
                    HandlePhone(lineData, ref people);
                    break;
                case "f":
                    HandleFamily(lineData, ref people);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles Person line data.
        /// Adds a new person into <paramref name="people"/> list.
        /// </summary>
        /// <param name="lineData">string array that contains data which is filled into person object</param>
        /// <param name="people">List of all people which has been currently processed</param>
        private void HandlePerson(string[] lineData, ref People people)
        {
            if(lineData.Length >= 2 && lineData.Length <= 3)
            {
                var person = new Person(lineData[1]);

                if(lineData.Length == 3)
                {
                    person.Lastname = lineData[2];
                }

                people.Persons.Add(person);
            }
            else
            {
                throw new ArgumentException($"Invalid data for person, expecting between 2 and 3 arguments, received {lineData.Length}");
            }
        }

        /// <summary>
        /// Handles Address line data.
        /// Adds Address object to current person if current person does not contain a family member.
        /// If it does contain family members then it will add the Address object to the last family member
        /// </summary>
        /// <param name="lineData">string array that contains data which is filled into Address object</param>
        /// <param name="people">List of all people which has been currently processed</param>
        private void HandleAddress(string[] lineData, ref People people)
        {
            if (lineData.Length >= 2 && lineData.Length <= 4)
            {

                var address = new Address(lineData[1]);

                if(lineData.Length >= 3)
                {
                    address.City = lineData[2];
                }

                if(lineData.Length == 4)
                {
                    address.ZIPCode = lineData[3];
                }

                var currentPerson = people.Persons.LastOrDefault();
                if(currentPerson != null)
                {
                    if (currentPerson.Family.Any())
                    {
                        currentPerson.Family.LastOrDefault().Address = address;
                    }
                    else
                    {
                        currentPerson.Address = address;
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid data for Address, expecting between 2 and 4 arguments, received {lineData.Length}");
            }
        }

        /// <summary>
        /// Handles Phone line data.
        /// Adds phone object to current person if current person does not contain a family member.
        /// If it does contain family members then it will add the Phone object to the last family member
        /// </summary>
        /// <param name="lineData">string array that contains data which is filled into Phone object</param>
        /// <param name="people">List of all people which has been currently processed</param>
        private void HandlePhone(string[] lineData, ref People people)
        {
            if (lineData.Length >= 2 && lineData.Length <= 3)
            {
                var phone = new Phone(lineData[1]);
                if (lineData.Length == 3)
                {
                    phone.Landline = lineData[2];
                }

                var currentPerson = people.Persons.LastOrDefault();
                if (currentPerson != null)
                {
                    if (currentPerson.Family.Any())
                    {
                        currentPerson.Family.LastOrDefault().Phone = phone;
                    }
                    else
                    {
                        currentPerson.Phone = phone;
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid data for Phone, expecting between 2 and 3 arguments, received {lineData.Length}");
            }
        }

        /// <summary>
        /// Handles Family line data.
        /// Adds Family member to the last person in people list.
        /// Throws exception if it does not match the family argument count.
        /// </summary>
        /// <param name="lineData">string array that contains data which is filled into Family object</param>
        /// <param name="people">List of all people which has been currently processed</param>
        private void HandleFamily(string[] lineData, ref People people)
        {
            if (lineData.Length >= 2 && lineData.Length <= 3)
            {
                var family = new Family(lineData[1]);
                if (lineData.Length == 3)
                {
                    family.Born = lineData[2];
                }

                var currentPerson = people.Persons.LastOrDefault();
                if (currentPerson != null)
                {
                    currentPerson.Family.Add(family);
                }
            }
            else
            {
                throw new ArgumentException($"Invalid data for Family, expecting between 2 and 3 arguments, received {lineData.Length}");
            }
        }
    }
}
