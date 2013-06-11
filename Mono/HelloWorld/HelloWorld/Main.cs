using System;

namespace HelloWorld
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

            YieldTester yieldTester = new YieldTester();
            yieldTester.Test();
		}
	}
}
