using System;

namespace SoftwareUsingSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            var maths = new SDKExample.MathMethods();

            Console.WriteLine(maths.Factorial(2));

            Console.WriteLine("Hello World!");
        }
    }
}
