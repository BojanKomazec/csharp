using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    class LinqDemo
    {
        public void OrderByDemo()
        {
            var arr = Enumerable.Range(1, 100);
            arr.ForEach(n => Console.WriteLine(n));
            var arr2 = arr.OrderBy(n => Guid.NewGuid());
            arr2.ForEach(n => Console.WriteLine(n));
        }

        public void PrintSequence1<T>(IEnumerable<T> sequence)
        {
            foreach(var element in sequence)
                Console.WriteLine(element);
        }

        // http://stackoverflow.com/questions/101265/why-there-is-no-foreach-extension-method-on-ienumerable
        // http://stackoverflow.com/questions/529188/executing-a-certain-action-for-all-elements-in-an-enumerablet
        public void PrintSequence2<T>(IEnumerable<T> sequence)
        {
            sequence.ToList().ForEach(element => Console.WriteLine(element));
        }
    }

    static class MyExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach(var element in sequence)
                action(element);
        }
    }

}