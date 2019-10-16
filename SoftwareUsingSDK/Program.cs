using System;

namespace SoftwareUsingSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            var sdk = new SDKExample.MathMethods("");
            Console.WriteLine(sdk.Status());

            return;
        }
    }
}
