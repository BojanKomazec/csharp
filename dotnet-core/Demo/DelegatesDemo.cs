using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    class DelegatesDemo
    {
        public static void Demo()
        {
            var delegatesDemo = new DelegatesDemo();
            delegatesDemo.DelegatesDemoHelperWhereTest();
        }

        public void DelegatesDemoHelperWhereTest()
        {
            Console.WriteLine("DelegatesDemo.DelegatesDemoHelperWhereTest()");

            Func<int, bool> predicate = n => n % 2 == 0;
            int[] seq = Enumerable.Range(1, 10).ToArray();
            var res = seq.Where(predicate);
            res.Dump();
        }
    }

    public static class DelegatesDemoHelper
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            foreach(var element in sequence)
            {
                if(predicate(element))
                    yield return element;
            }
        }
    }
}