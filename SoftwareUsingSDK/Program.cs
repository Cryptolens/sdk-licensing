using System;

namespace SoftwareUsingSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SDK.SDK.Name());

            Console.WriteLine(LibraryUsing_SDK.MySDK.Action());


            Console.ReadLine();
        }
    }
}
