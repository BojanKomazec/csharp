using System;
using System.Collections.Generic;

namespace Demo
{
    class LambdaDemo
    {
        public static void Demo()
        {
            var linqDemo = new LambdaDemo();
            linqDemo.CaptureDemo();
            linqDemo.CaptureDemo_PrintIteratorValue_1();
            linqDemo.CaptureDemo_PrintIteratorValue_2();
        }

        public void CaptureDemo()
        {
            Console.WriteLine("LambdaDemo.CaptureDemo()");

            var value = 123;
            var lambda1 = new Func<int>(() => value);
            value = 456;
            Console.WriteLine(lambda1());

            var name = "London";
            var lambda2 = new Func<string>(() => name);
            name = "New York";
            Console.WriteLine(lambda2());
        }

        
        public void CaptureDemo_PrintIteratorValue_1()
        {
            Console.WriteLine("LambdaDemo.CaptureDemo_PrintIteratorValue_1()");

            var actions = new List<Action>();
            for (var i = 1; i < 10; i++)
                actions.Add(() => Console.WriteLine(i));

            foreach(var action in actions)
                action();
        }


        public void CaptureDemo_PrintIteratorValue_2()
        {
            Console.WriteLine("LambdaDemo.CaptureDemo_PrintIteratorValue_2()");

            var actions = new List<Action>();
            for (var i = 1; i < 10; i++)
            {
                var currentValue = i;
                actions.Add(() => Console.WriteLine(currentValue));
            }

            foreach(var action in actions)
                action();
        }

    }
}