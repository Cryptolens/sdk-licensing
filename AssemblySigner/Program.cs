using System;

using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;

using Newtonsoft.Json;
using System.Reflection;
using System.IO;

namespace AssemblySigner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cryptolens Assembly Signer (command-line) v1.0");
            Console.WriteLine("Copyright (c) 2019 Cryptolens AB. All rights reserved.\n");

            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"This tool helps you to automatically sign multiple assemblies.\n" +
                              $"All configurations about where these assemblies are located should \n" +
                              $"be put into build.json in the same folder as this file. If you want\n" +
                              $"to read build.json from a different folder, please supply ir as an\n" +
                              $"argument to this application. This file should also have a valid \n" +
                              $"license key supplied.\n\n" +
                              $"For more information, please check \n" +
                              $" > https://github.com/cryptolens/sdk-licensing");
            Console.WriteLine("--------------------------------------------------------------------\n");

            string asmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // TODO: error handling

            VendorConfig vendorConfig;

            try
            {
                vendorConfig = JsonConvert.DeserializeObject<VendorConfig>(System.IO.File.ReadAllText(Path.Combine(asmDir, "config.json")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: could not find config.json. Please contact the vendor.");
                return;
            }
            //JsonConvert.DeserializeObject()

            string pathToUserConfig = Path.Combine(asmDir, "build.json");

            if (args.Length != 0)
            {
                pathToUserConfig = args[0];
            }

            var userConfig = JsonConvert.DeserializeObject<UserConfig>(pathToUserConfig);

            if(userConfig.Assemblies == null || userConfig.Assemblies.Count == 0)
            {
                Console.WriteLine("Error: no assembly paths specified.");
                return;
            }

            foreach (var path in userConfig.Assemblies)
            {

            }

            Console.ReadLine();
        }
    }
}
