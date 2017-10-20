using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

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

    [Serializable]
    public class JamesBondCar : Car
    {
        public bool canFly;
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
            Console.ReadLine();
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
    }
}
