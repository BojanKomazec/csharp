using System;
using System.Collections.Generic;

namespace Demo
{
    class DataStructuresDemo
    {
        public static void Demo()
        {
            var dataStructuresDemo = new DataStructuresDemo();
            dataStructuresDemo.Dictionary_PrintKeyValuePairs();
        }

        // https://en.wikipedia.org/wiki/Quicksort
        public void Dictionary_PrintKeyValuePairs()
        {
            var dict = new Dictionary<string, int>{ 
                {"Orange juice", 12}, 
                {"Water", 3}, 
                {"Apple juice", 7} 
            };

            // each item is of type KeyValuePair<string, int>
            foreach(var item in dict)
                Console.WriteLine($"Key = {item.Key}, Value = {item.Value}");

        }
    }
}