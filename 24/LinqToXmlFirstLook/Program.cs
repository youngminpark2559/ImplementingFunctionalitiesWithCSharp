using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

//c Add BuildXmlDocWithDOM() generating XML file composed of elements, values which I put one by one.

namespace LinqToXmlFirstLook
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        //Generate XML by using System.Xml.dll
        //The way to generate XML will be replaced with LINQ to XML in this chap.
        private static void BuildXmlDocWithDOM()
        {
            // Make a new XML document in memory.
            XmlDocument doc = new XmlDocument();

            // Fill this document with a root element named <Inventory>.
            XmlElement inventory = doc.CreateElement("Inventory");

            // Now, make a subelement named <Car> with an ID attribute.
            XmlElement car = doc.CreateElement("Car");
            car.SetAttribute("ID", "1000");

            // Build the data within the <Car> element.
            XmlElement name = doc.CreateElement("PetName");
            name.InnerText = "Jimbo";
            XmlElement color = doc.CreateElement("Color");
            color.InnerText = "Red";
            XmlElement make = doc.CreateElement("Make");
            make.InnerText = "Ford";

            // Add <PetName>, <Color>, and <Make> to the <Car> element.
            car.AppendChild(name);
            car.AppendChild(color);
            car.AppendChild(make);

            // Add the <Car> element to the <Inventory> element.
            inventory.AppendChild(car);

            // Insert the complete XML into the XmlDocument object, and save to file.
            // Inventory.xml file is generated in bin\Debug folder.
            doc.AppendChild(inventory);
            doc.Save("Inventory.xml");
        }
    }
}
