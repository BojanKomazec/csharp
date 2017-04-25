using System;
using System.Collections.Generic;

namespace Demo
{
    static class Util
    {
        public static void Dump<T>(this IEnumerable<T> sequence)
        {
            foreach(var element in sequence)
                Console.WriteLine(element);
        }
    }
}