using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class YieldTester
    {
        public void Test()
        {
            foreach (int i in GetArray(10))
                Console.WriteLine(i);
        }

        IEnumerable GetArray(int n)
        {
            for (int i = 0; i < n; i++)
                yield return i;
        }
    }
}
