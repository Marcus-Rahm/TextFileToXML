using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using TextFileToXML.Infrastructure;

namespace TextFileToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser parser = new FileParser();

            StreamReader dataStream = GetData(args);
            string result = null;
            if (dataStream != null)
            {
                try
                {
                    result = parser.ParseToXML(dataStream);
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\nStopping program");
                }
            }

            Console.WriteLine("Save to file? (y/n)");
            string response = Console.ReadLine();
            switch (response.ToLower())
            {
                case "y":
                    if(!string.IsNullOrEmpty(result))
                    {
                        File.WriteAllText("Output.xml", result);
                    }
                    else
                    {
                        Console.WriteLine("No result string, ignoring save to file");
                    }
                    break;
            }
        }

        static StreamReader GetData(string[] args)
        {
            if (args.Length == 2)
            {
                if (args[0].ToLower() == "-f")
                {
                    return new StreamReader(args[1]);
                }
                else
                {
                    Console.WriteLine("Expecting first argument to be -f (Example: -f testData.txt)");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Invalid arguments (Example: -f testData.txt)\nUsing default test data");
                return GetDefaultTestData();
            }
        }

        static StreamReader GetDefaultTestData()
        {
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
            return new StreamReader(stream);
        }
    }
}
