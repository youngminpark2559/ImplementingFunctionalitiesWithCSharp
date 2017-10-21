using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace CustomSerialization
{
    [Serializable]
    class StringData : ISerializable
    {
        private string dataItemOne = "First data block";
        private string dataItemTwo = "More data";
        public StringData() { }

        // Special custom constructor
        // with this signature constructor, the runtime engine can set the state of object
        protected StringData(SerializationInfo si, StreamingContext ctx)
        {
            // Rehydrate member variables from stream.
            dataItemOne = si.GetString("First_Item").ToLower();
            dataItemTwo = si.GetString("dataItemTwo").ToLower();
        }

        // Implement from ISerializable
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            // Fill up the SerializationInfo object with the formatted data.
            info.AddValue("First_Item", dataItemOne.ToUpper());
            info.AddValue("dataItemTwo", dataItemTwo.ToUpper());
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Custom Serialization *****");

            // StringData : ISerializable
            StringData myData = new StringData();

            // When saving serialized data in file
            // ISerializable.GetObjectData() is invoked
            SoapFormatter soapFormat = new SoapFormatter();
            using (Stream fStream = new FileStream("MyData.soap",
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(fStream, myData);
            }
            Console.ReadLine();
        }
    }
}
