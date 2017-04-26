using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo
{
    class JsonDemo
    {
        class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime DeliveryDate { get; set; }
            public float[] Prices { get; set;}
            public float[] PackageSizes { get; set; }
        }

        public static void Demo()
        {
            var jsonDemo = new JsonDemo();
            jsonDemo.DeserializingUnknownType();
            jsonDemo.RoundtripKnownType();
        }

        // How to deserialize JSON with unknown structure?
        // yahoo_weather_forecast.json was fetched from https://developer.yahoo.com/weather/
        public void DeserializingUnknownType()
        {
            Console.WriteLine("JsonDemo.DeserializingUnknownType()");

            var json = System.IO.File.ReadAllText("yahoo_weather_forecast.json");

            var obj1 = JObject.Parse(json);
            Console.WriteLine(obj1.ToString());

            dynamic obj2 = JsonConvert.DeserializeObject(json);
            //Console.WriteLine($"obj2.GetType() = {obj2.GetType()}, obj2.ToString() = {obj2.ToString()}");
            Console.WriteLine($"obj2.ToString() = {obj2.ToString()}");
            var query1 = obj2.query;
            var query2 = obj2["query"];

            // accessing deeper levels
            var created1 = obj2.query.created;
            Console.WriteLine($"created1 = {created1}");

            // accessing the same deep level (another semantics used)
            var created2 = obj2["query"]["created"];
            Console.WriteLine($"created2 = {created2}");
        }

        // use http://json2csharp.com/ to generate C# type that matches JSON
        public void RoundtripKnownType()
        {
            Console.WriteLine("JsonDemo.RoundtripKnownType()");

            var product = new Product{
                Id = 123,
                Name = "Orange juice",
                DeliveryDate = new DateTime(2017, 04, 26),
                Prices = new float[] {1.79F, 2.39F, 2.79F},
                PackageSizes = new float[] {1, 1.5F, 2}
            };

            string json = JsonConvert.SerializeObject(product);
            Console.WriteLine(json);
            
            var deserializedProduct = JsonConvert.DeserializeObject<Product>(json);
        }

    }
}