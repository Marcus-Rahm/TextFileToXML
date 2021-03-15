# TextFileToXML
This is a console program that takes a txt file containing data which it converts into a XML.

## How to run it
To run the program, you have two options.
  1. Run it without arguments `TextFileToXML.exe`, it will then use stored test data for its input.
  2. Run it with arguments `TextFileToXML.exe -f .\test\data\testData.txt`, it will read the content of the file and convert it into XML.

Once the program has finished converting the data to XML it will print the XML to the console and give a promt asking if it should save the XML to file.
  
## Example data
A example of the data can be found in the file testData.txt located in test\data.
The example data stored in the program is below.
```
P|Carl Gustaf|Bernadotte
T|0768-101801|08-101801
A|Drottningholms slott|Stockholm|10001
F|Victoria|1977
A|Haga Slott|Stockholm|10002
F|Carl Philip|1979
T|0768-101802|08-101802
P|Barack|Obama
A|1600 Pennsylvania Avenue|Washington, D.C
```
