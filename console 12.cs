using System;
using System.Xml;

class Program
{
    static void Main()
    {
        string filePath = "GPS.xml";

        // Create XML file
        CreateXmlFile(filePath);

        // Read and display XML file content
        Console.WriteLine("XML File Content:");
        ReadAndDisplayXml(filePath);
    }

    static void CreateXmlFile(string filePath)
    {
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t",
            Encoding = System.Text.Encoding.UTF8
        };

        using (XmlWriter writer = XmlWriter.Create(filePath, settings))
        {
            writer.WriteStartDocument();

            writer.WriteStartElement("GPS_Log");

            // Position element
            writer.WriteStartElement("Position");
            writer.WriteAttributeString("DateTime", DateTime.Now.ToString());
            writer.WriteElementString("x", "65.8973342");
            writer.WriteElementString("y", "72.3452346");

            // SatteliteInfo element
            writer.WriteStartElement("SatteliteInfo");
            writer.WriteElementString("Speed", "40");
            writer.WriteElementString("NoSatt", "7");
            writer.WriteEndElement(); // End SatteliteInfo

            writer.WriteEndElement(); // End Position

            // Image element
            writer.WriteStartElement("Image");
            writer.WriteAttributeString("Resolution", "1024x800");
            writer.WriteElementString("Path", @"\images\1.jpg");
            writer.WriteEndElement(); // End Image

            writer.WriteEndElement(); // End GPS_Log

            writer.WriteEndDocument();
        }

        Console.WriteLine("XML file created successfully!");
    }

    static void ReadAndDisplayXml(string filePath)
    {
        using (XmlReader reader = XmlReader.Create(filePath))
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        Console.Write($"<{reader.Name}");
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                Console.Write($" {reader.Name}=\"{reader.Value}\"");
                            }
                            reader.MoveToElement();
                        }
                        Console.WriteLine(">");
                        break;

                    case XmlNodeType.Text:
                        Console.WriteLine(reader.Value);
                        break;

                    case XmlNodeType.EndElement:
                        Console.WriteLine($"</{reader.Name}>");
                        break;
                }
            }
        }
    }
}

