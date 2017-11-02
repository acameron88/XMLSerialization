using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BinarySerialization {
    class Program {
        static void Main(string[] args)
        {

            //Binary serialization saves ALL the fields and properties of an object keeping TYPE FIDELITY

            string path = "myFile3.bin";

            MyObject obj = new MyObject();
            obj.n1 = 1;
            obj.n2 = 2;
            obj.str = "Some String";

            Serialize(obj,path);

            var desObj = (MyObject)Deserialize(path);

            Console.WriteLine(desObj.n1+" "+desObj.n1+" "+desObj.str);

            Console.WriteLine();
            Console.Read();
        }

        private static void Serialize(MyObject obj,string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        private static object Deserialize(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read);
            MyObject obj2 = (MyObject) formatter.Deserialize(stream);
            stream.Close();

            return obj2;

        }

    }
}
