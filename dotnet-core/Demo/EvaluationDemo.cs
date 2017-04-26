using System;

namespace Demo
{
    class EvaluationDemo
    {
        public static void Demo()
        {
            var linqDemo = new EvaluationDemo();
            linqDemo.LazyEvaluationDemo();
        }

        // https://en.wikipedia.org/wiki/Lazy_evaluation
        public void LazyEvaluationDemo()
        {
            Console.WriteLine("LazyEvaluationDemo()");

            var a = 0;
            var b = 0;
            var c = new Lazy<int>(() => a + b);
            a = 1;
            b = 2;
            Console.WriteLine($"c.IsValueCreated = {c.IsValueCreated}");
            Console.WriteLine($"c.Value = {c.Value}");
            Console.WriteLine($"c.IsValueCreated = {c.IsValueCreated}");
        }
    }
}