using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Section_1_1();
        }

        public static void Section_1_1()
        {
            string[] names = { "Tom", "Dick", "Harry" };

            // Where is a query operator. A query operator is a method that transforms a sequence. 
            // A typical query operator accepts an input sequence and emits a transformed output sequence. 
            // Most query operators accept a lambda expression as an argument. 
            // The Where operator requires that the lambda expression return a bool value, which if true, indicates that the element should be included in the output sequence.
            // The input argument corresponds to an input element. In this case, the input argument n represents each name in the array and is of type string.
            //
            // A query is an expression that transforms sequences with query operators.
            //                        
            /*
            IEnumerable<string> filteredNames =
                  System.Linq.Enumerable.Where(
                  names, n => n.Length >= 4);                          
            */

            // Where (like other standard query operators) is implemented as extension method 
            /*
            IEnumerable<string> filteredNames =
                names.Where(
                    n => n.Length >= 4);
            */

            // implicitly typing filteredNames
            var filteredNames =
                names.Where(
                    n => n.Length >= 4);

            foreach (string n in filteredNames)
                Console.Write(n + "|");            // Dick|Harry|

            Console.WriteLine();

            // C# also defines a special syntax for writing queries, called query comprehension syntax:
            IEnumerable<string> filteredNames2 =
                  from n in names
                  where n.Contains("a")
                  select n;

            foreach (string n in filteredNames2)
                Console.Write(n + "|");            // Harry|
        }
    }
}
