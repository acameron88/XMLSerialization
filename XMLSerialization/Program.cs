using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLSerialization {
    internal class Program {
        private static void Main(string[] args) {


            var path = "result.xml";

            //worked --- problem with path 



            //1. Create initial list
            var myOrders = Order.StartUpOrders();

            foreach (var order in myOrders) {
                Console.WriteLine("List: "+order.ToString());
            }


            var myOrdertoArray = myOrders.ToArray();


            foreach (var order in myOrdertoArray)
            {
                Console.WriteLine("Array: " + order.ToString());
            }
            
            


            // get the collection
            //var orders = (from o in Queryable<Order>
            //             where o.Something
            //             select o).ToArray();

            // serializing in xml

            Order.SerializeOrders(myOrders.ToArray(), path);

            // deserializing the xml
            var serializedOrders = Order.Deserialize(path);
            Console.WriteLine("Finished");
            Console.Read();
        }
    }


    //Serialiazable Attribute
    [Serializable]
    public class Order {
        public string OrderNo { get; set; }
        public string ItemId { get; set; }
        public string ItemDesc { get; set; }
        public int Qty { get; set; }


        public override string ToString() {
            string odr = OrderNo + " " + ItemId + " " + ItemDesc + " " + Qty;
            return odr.ToString();
        }


        public static void SerializeOrders(Order[] orders, string path) {

            var parse = new XmlSerializer(typeof(Order[]));
            
            using (var writer = new StreamWriter(path)) {
                parse.Serialize(writer, orders);
                writer.Close();
            }
        }

        public static Order[] Deserialize(string path) {
            XmlSerializer ser = new XmlSerializer(typeof(Order[]));
            Order[] result;
            using (XmlReader reader = XmlReader.Create(path)) {
                result = (Order[])ser.Deserialize(reader);
            }
            return result;
        }


        public static List<Order> StartUpOrders() {

            var myOrders = new List<Order>();
            for (int i = 0; i < 5; i++) {
                Order o = new Order() { ItemDesc = "T-Shirt", OrderNo = (i + 1).ToString(), ItemId = "0", Qty = 1 };
                myOrders.Add(o);
            }

            return myOrders;
        }



    }





}

