using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextFileToXML.Infrastructure;

namespace TextFileToXML.Test
{
    [TestClass]
    public class FileParserTest
    {
        [TestMethod]
        public void Parse_PersonValidationTest()
        {
            // Arrange
            var validTestData =
                "P|Carl Gustaf|Bernadotte\n" +
                "P|Carl Gustaf";

            var invalidTestData =
                "P|Carl Gustaf|Bernadotte|1212121212\n" +
                "P";

            byte[] byteArray = Encoding.ASCII.GetBytes(validTestData);
            MemoryStream stream = new MemoryStream(byteArray);
            var validData = new StreamReader(stream);

            byte[] byteArrayInvalid = Encoding.ASCII.GetBytes(invalidTestData);
            MemoryStream streamInvalid = new MemoryStream(byteArrayInvalid);
            var invalidData = new StreamReader(streamInvalid);

            var parser = new FileParser();

            // Act
            var result = parser.ParseToPersonList(validData);

            // Assert
            Assert.IsTrue(result.Persons.Count == 2);
            Assert.IsNotNull(result.Persons[0].Firstname);
            Assert.IsNotNull(result.Persons[0].Lastname);
            Assert.IsNotNull(result.Persons[1].Firstname);
            Assert.IsNull(result.Persons[1].Lastname);

            Assert.ThrowsException<ArgumentException>(() => parser.ParseToPersonList(invalidData));

        }

        [TestMethod]
        public void Parse_AddressValidationTest()
        {
            // Arrange
            var validTestData =
                "P|Carl Gustaf|Bernadotte\n" +
                "A|Drottningholms slott|Stockholm|1000\n" +
                "P|Carl Gustaf\n" +
                "A|Drottningholms slott|Stockholm\n" +
                "P|Carl Gustaf\n" +
                "A|Drottningholms slott";

            var invalidTestData =
                "P|Carl Gustaf|Bernadotte\n" +
                "A|Drottningholms slott|Stockholm|1000|sweden\n" +
                "P|Carl Gustaf\n" +
                "A";

            byte[] byteArray = Encoding.ASCII.GetBytes(validTestData);
            MemoryStream stream = new MemoryStream(byteArray);
            var validData = new StreamReader(stream);

            byte[] byteArrayInvalid = Encoding.ASCII.GetBytes(invalidTestData);
            MemoryStream streamInvalid = new MemoryStream(byteArrayInvalid);
            var invalidData = new StreamReader(streamInvalid);

            var parser = new FileParser();

            // Act
            var result = parser.ParseToPersonList(validData);

            // Assert
            Assert.IsTrue(result.Persons.Count == 3);
            Assert.IsNotNull(result.Persons[0].Address);
            Assert.IsNotNull(result.Persons[0].Address.Street);
            Assert.IsNotNull(result.Persons[0].Address.City);
            Assert.IsNotNull(result.Persons[0].Address.ZIPCode);

            Assert.IsNotNull(result.Persons[1].Address);
            Assert.IsNotNull(result.Persons[1].Address.Street);
            Assert.IsNotNull(result.Persons[1].Address.City);
            Assert.IsNull(result.Persons[1].Address.ZIPCode);

            Assert.IsNotNull(result.Persons[2].Address);
            Assert.IsNotNull(result.Persons[2].Address.Street);
            Assert.IsNull(result.Persons[2].Address.City);
            Assert.IsNull(result.Persons[2].Address.ZIPCode);

            Assert.ThrowsException<ArgumentException>(() => parser.ParseToPersonList(invalidData));

        }

        [TestMethod]
        public void Parse_PhoneValidationTest()
        {
            // Arrange
            var validTestData =
                "P|Carl Gustaf|Bernadotte\n" +
                "T|0768 - 101801|08 - 101801\n" +
                "P|Carl Gustaf\n" +
                "T|0768 - 101801";

            var invalidTestData =
                "P|Carl Gustaf|Bernadotte\n" +
                "T|0768 - 101801|08 - 101801|sweden\n" +
                "P|Carl Gustaf\n" +
                "T";

            byte[] byteArray = Encoding.ASCII.GetBytes(validTestData);
            MemoryStream stream = new MemoryStream(byteArray);
            var validData = new StreamReader(stream);

            byte[] byteArrayInvalid = Encoding.ASCII.GetBytes(invalidTestData);
            MemoryStream streamInvalid = new MemoryStream(byteArrayInvalid);
            var invalidData = new StreamReader(streamInvalid);

            var parser = new FileParser();

            // Act
            var result = parser.ParseToPersonList(validData);

            // Assert
            Assert.IsTrue(result.Persons.Count == 2);
            Assert.IsNotNull(result.Persons[0].Phone);
            Assert.IsNotNull(result.Persons[0].Phone.Mobile);
            Assert.IsNotNull(result.Persons[0].Phone.Landline);

            Assert.IsNotNull(result.Persons[1].Phone);
            Assert.IsNotNull(result.Persons[1].Phone.Mobile);
            Assert.IsNull(result.Persons[1].Phone.Landline);

            Assert.ThrowsException<ArgumentException>(() => parser.ParseToPersonList(invalidData));

        }

        [TestMethod]
        public void Parse_FamilyValidationTest()
        {
            // Arrange
            var validTestData =
                "P|Carl Gustaf|Bernadotte\n" +
                "F|Victoria|1977\n" +
                "P|Carl Gustaf\n" +
                "F|Victoria\n";

            var invalidTestData =
                "P|Carl Gustaf|Bernadotte\n" +
                "F|Victoria|1977|gatan 1\n" +
                "P|Carl Gustaf\n" +
                "F";

            byte[] byteArray = Encoding.ASCII.GetBytes(validTestData);
            MemoryStream stream = new MemoryStream(byteArray);
            var validData = new StreamReader(stream);

            byte[] byteArrayInvalid = Encoding.ASCII.GetBytes(invalidTestData);
            MemoryStream streamInvalid = new MemoryStream(byteArrayInvalid);
            var invalidData = new StreamReader(streamInvalid);

            var parser = new FileParser();

            // Act
            var result = parser.ParseToPersonList(validData);

            // Assert
            Assert.IsTrue(result.Persons.Count == 2);
            Assert.IsNotNull(result.Persons[0].Family);
            Assert.IsTrue(result.Persons[0].Family.Count == 1);
            Assert.IsNotNull(result.Persons[0].Family[0].Name);
            Assert.IsNotNull(result.Persons[0].Family[0].Born);

            Assert.IsNotNull(result.Persons[1].Family);
            Assert.IsTrue(result.Persons[1].Family.Count == 1);
            Assert.IsNotNull(result.Persons[1].Family[0].Name);
            Assert.IsNull(result.Persons[1].Family[0].Born);

            Assert.ThrowsException<ArgumentException>(() => parser.ParseToPersonList(invalidData));

        }

        [TestMethod]
        public void Parse_WithValidData()
        {
            // Arrange
            var testData =
                "P|Carl Gustaf|Bernadotte\n" +
                "T|0768 - 101801|08 - 101801\n" +
                "A|Drottningholms slott|Stockholm|1000\n" +
                "F|Victoria|1977\n" +
                "A|Haga Slott|Stockholm|10002\n" +
                "F|Carl Philip|1979\n" +
                "T|0768 - 101802|08 - 101802\n" +
                "P|Barack|Obama\n" +
                "A|1600 Pennsylvania Avenue|Washington, D.C";

            byte[] byteArray = Encoding.ASCII.GetBytes(testData);
            MemoryStream stream = new MemoryStream(byteArray);
            var data = new StreamReader(stream);

            var parser = new FileParser();

            // Act
            var result = parser.ParseToPersonList(data);

            //Assert
            Assert.IsTrue(result.Persons.Count == 2);
            Assert.IsNotNull(result.Persons[0].Firstname);
            Assert.IsNotNull(result.Persons[0].Lastname);
            Assert.IsNotNull(result.Persons[0].Address);
            Assert.IsNotNull(result.Persons[0].Address.Street);
            Assert.IsNotNull(result.Persons[0].Address.City);
            Assert.IsNotNull(result.Persons[0].Address.ZIPCode);
            Assert.IsNotNull(result.Persons[0].Phone);
            Assert.IsNotNull(result.Persons[0].Phone.Mobile);
            Assert.IsNotNull(result.Persons[0].Phone.Landline);

            Assert.IsNotNull(result.Persons[0].Family);
            Assert.IsTrue(result.Persons[0].Family.Count == 2);
            Assert.IsNotNull(result.Persons[0].Family[0].Name);
            Assert.IsNotNull(result.Persons[0].Family[0].Born);
            Assert.IsNotNull(result.Persons[0].Family[0].Address);
            Assert.IsNotNull(result.Persons[0].Family[0].Address.Street);
            Assert.IsNotNull(result.Persons[0].Family[0].Address.City);
            Assert.IsNotNull(result.Persons[0].Family[0].Address.ZIPCode);
            Assert.IsNotNull(result.Persons[0].Family[1].Name);
            Assert.IsNotNull(result.Persons[0].Family[1].Born);
            Assert.IsNotNull(result.Persons[0].Family[1].Phone);
            Assert.IsNotNull(result.Persons[0].Family[1].Phone.Mobile);
            Assert.IsNotNull(result.Persons[0].Family[1].Phone.Landline);

            Assert.IsNotNull(result.Persons[1].Firstname);
            Assert.IsNotNull(result.Persons[1].Lastname);
            Assert.IsNotNull(result.Persons[1].Address);
            Assert.IsNotNull(result.Persons[1].Address.Street);
            Assert.IsNotNull(result.Persons[1].Address.City);
            Assert.IsNull(result.Persons[1].Address.ZIPCode);
            Assert.IsNull(result.Persons[1].Phone);
            Assert.IsNotNull(result.Persons[1].Family);
            Assert.IsTrue(result.Persons[1].Family.Count == 0);
        }

        [TestMethod]
        public void Parse_WithInvalidData()
        {
            // Arrange
            var testData =
                "T|0768 - 101801|08 - 101801\n" +
                "A|Drottningholms slott|Stockholm|1000\n" +
                "F|Victoria|1977\n" +
                "A|Haga Slott|Stockholm|10002\n" +
                "T|0768 - 101802|08 - 101802\n" +
                "P|Barack|Obama\n" +
                "P|Barack|Obama\n" +
                "T|0768 - 101802|08 - 101802\n";

            byte[] byteArray = Encoding.ASCII.GetBytes(testData);
            MemoryStream stream = new MemoryStream(byteArray);
            var data = new StreamReader(stream);

            var parser = new FileParser();

            // Act
            var result = parser.ParseToPersonList(data);

            //Assert
            Assert.IsTrue(result.Persons.Count == 2);
            Assert.IsNotNull(result.Persons[0].Firstname);
            Assert.IsNotNull(result.Persons[0].Lastname);

            Assert.IsNotNull(result.Persons[1].Firstname);
            Assert.IsNotNull(result.Persons[1].Lastname);
            Assert.IsNotNull(result.Persons[1].Phone);
            Assert.IsNotNull(result.Persons[1].Phone.Mobile);
            Assert.IsNotNull(result.Persons[1].Phone.Landline);

        }
    }
}
