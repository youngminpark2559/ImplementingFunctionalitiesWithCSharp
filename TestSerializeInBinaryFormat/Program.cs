using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestSerializeInBinaryFormat
{
    [Serializable]
    public class Radio
    {
        public bool hasTweeters;
        public bool hasSubWoofers;
        public double[] stationPresets;

        [NonSerialized]
        public string radioID = "XF-552RR6";
    }

    [Serializable]
    public class Car
    {
        public Radio theRadio = new Radio();
        public bool isHatchBack;
    }

    [Serializable, XmlRoot(Namespace = "http://www.MyCompany.com")]
    public class JamesBondCar : Car
    {
        public JamesBondCar(bool skyWorthy, bool seaWorthy)
        {
            canFly = skyWorthy;
            canSubmerge = seaWorthy;
        }
        // The XmlSerializer demands a default constructor!
        public JamesBondCar() { }

        [XmlAttribute]
        public bool canFly;
        [XmlAttribute]
        public bool canSubmerge;
    }

    [Serializable]
    public class Person
    {
        // A public field.
        public bool isAlive = true;

        // A private field.
        private int personAge = 21;

        // Public property/private data.
        private string fName = string.Empty;

        public string FirstName
        {
            get { return fName; }
            set { fName = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Object Serialization *****\n");

            // Make a JamesBondCar and set state.
            // jbc is an object graph which is consists of objects such as JamesBondCar, Car, Radio
            // The object of JamesBondCar references Car, and Car uses Radio.
            JamesBondCar jbc = new JamesBondCar();
            jbc.canFly = true;
            jbc.canSubmerge = false;
            jbc.theRadio.stationPresets = new double[] { 89.3, 105.1, 97.1 };
            jbc.theRadio.hasTweeters = true;

            // Now save the car to a specific file in a binary format.
            // Pass in the object-graph and CarData.dat will be created in the location of Bin\Debug\
            SaveAsBinaryFormat(jbc, "CarData.dat");
            LoadFromBinaryFile("CarData.dat");

            SaveAsSoapFormat(jbc, "CarData.soap");
            LoadFromSoapFile("CarData.soap");

            SaveAsXmlFormat(jbc, "CarData.xml");
            LoadFromXmlFile("CarData.xml");

            SaveListOfCars();

            SaveListOfCarsAsBinary();
        }

        static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.dat in binary.
            // Serializing technologies which .NET supports are 
            // 1. BinaryFommater 2. SOAPformatter 3. XML
            // 1. is broadly available but only on .NET flatform(not available? in other flatform)
            // 2. is XML format except for it using web service?
            // 3. is broadly available not only on .NET flatform but also on other flatform or file type
            // but can't be interacted with object-graph because it can cause loop in XML file due to object references to other.
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(fileName,
                  FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("=> Saved car in binary format!");
        }

        static void LoadFromBinaryFile(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            // Read the JamesBondCar from the binary file.
            // It is stream in byte array(binary type data)
            using (Stream fStream = File.OpenRead(fileName))
            {
                //I pass in that data(fStream) into Deserialize method to deserialze that binary data and to reconstruct object-graph which is return in object type so that I should cast object type to specific type that I want to use in explicitly
                JamesBondCar carFromDisk =
                  (JamesBondCar)binFormat.Deserialize(fStream);
                //After deserializing binary data and then type casting object type to specific type explicitly, I can use object, extracting canFly property data
                Console.WriteLine("1. Can this car fly? : {0}", carFromDisk.canFly);
                Console.WriteLine("2. stationPresets of Radio object");
                foreach (var stationPresets in carFromDisk.theRadio.stationPresets)
                {
                    Console.WriteLine(stationPresets);
                }
            }
        }


        // SOAP format uses XML format but it doesn't cause object-graph infinite loop in .soap file
        // cause it uses #ref tokens to mark the object references
        static void SaveAsSoapFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.soap in SOAP format.
            // First, Get SoapFormatter object
            SoapFormatter soapFormat = new SoapFormatter();

            // Second, create a file which will be contained by SOAP format data
            // in Bin\Debug
            using (Stream fStream = new FileStream(fileName,
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Serialize object-graph in SOAP format and save it to the physical file
                soapFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("\n=> Saved car in SOAP format!");
        }

        static void LoadFromSoapFile(string fileName)
        {
            SoapFormatter soapFormat = new SoapFormatter();

            // Read the JamesBondCar from the binary file.
            // It is stream in byte array(binary type data)
            using (Stream fStream = File.OpenRead(fileName))
            {
                JamesBondCar carFromDisk =(JamesBondCar)soapFormat.Deserialize(fStream);
                
                Console.WriteLine("1. Can this car fly? : {0}", carFromDisk.canFly);
                Console.WriteLine("2. stationPresets of Radio object");
                foreach (var stationPresets in carFromDisk.theRadio.stationPresets)
                {
                    Console.WriteLine(stationPresets);
                }
            }
        }



        //XmlSerializer requires you to specify type information that represents the class you want to serialize
        static void SaveAsXmlFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.xml in XML format.
            // Specify type information that represents the class you want to serialize.
            XmlSerializer xmlFormat = new XmlSerializer(typeof(JamesBondCar));

            using (Stream fStream = new FileStream(fileName,
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("\n=> Saved car in XML format!");
        }


        static void LoadFromXmlFile(string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(JamesBondCar));

            using (Stream fStream = File.OpenRead(fileName))
            {
                JamesBondCar carFromDisk = (JamesBondCar)xmlFormat.Deserialize(fStream);

                Console.WriteLine("1. Can this car fly? : {0}", carFromDisk.canFly);
                Console.WriteLine("2. stationPresets of Radio object");
                foreach (var stationPresets in carFromDisk.theRadio.stationPresets)
                {
                    Console.WriteLine(stationPresets);
                }
            }
        }


        static void SaveListOfCars()
        {
            // Now persist a List<T> of JamesBondCars.
            // System.Collections and System.Collections.Generic namespaces are already implemented as Serializable. 
            List<JamesBondCar> myCars = new List<JamesBondCar>();
            // Add objects to generic List
            myCars.Add(new JamesBondCar(true, true));
            myCars.Add(new JamesBondCar(true, false));
            myCars.Add(new JamesBondCar(false, true));
            myCars.Add(new JamesBondCar(false, false));

            // Create a physical file named CarCollection.xml
            using (Stream fStream = new FileStream("CarCollection.xml",
    FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // XmlSerializer requires you to specify the type that you want to serialize
                // In this case, List of JamesBondCar objects
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<JamesBondCar>));
                xmlFormat.Serialize(fStream, myCars);
            }
            Console.WriteLine("\n=> Saved list of cars!");
        }


        static void SaveListOfCarsAsBinary()
        {
            // Save ArrayList object (myCars) as binary.
            List<JamesBondCar> myCars = new List<JamesBondCar>();

            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream("AllMyCars.dat",
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, myCars);
            }
            Console.WriteLine("\n=> Saved list of cars in binary!");
        }
    }
}
