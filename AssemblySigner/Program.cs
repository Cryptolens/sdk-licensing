using System;

using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;

using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

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
                              $"to read build.json from a different folder, please supply it as an\n" +
                              $"argument to this application. This file should also have a valid \n" +
                              $"license key supplied.\n\n" +
                              $"For more information, please check \n" +
                              $" > https://github.com/Cryptolens/sdk-licensing/blob/master/Developer.md");
            Console.WriteLine("--------------------------------------------------------------------\n");

            string asmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

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

            string pathToUserConfig = Path.Combine(asmDir, "build.json");

            if (args.Length != 0)
            {
                pathToUserConfig = args[0];
            }

            var userConfig = JsonConvert.DeserializeObject<UserConfig>(System.IO.File.ReadAllText(pathToUserConfig));

            if(userConfig.Assemblies == null || userConfig.Assemblies.Count == 0)
            {
                Console.WriteLine("Error: No assembly paths specified.");
                return;
            }

            SHA512 sha = SHA512.Create();

            var response = Key.Activate(vendorConfig.ActivateToken, new ActivateModel { ProductId = vendorConfig.ProductId, Key = userConfig.Key, MachineCode =  Helpers.GetMachineCodePI() });

            if (!Helpers.IsSuccessful(response))
            {
                Console.WriteLine($"Error: Could not activate the following device, {(response != null ? response.Message : "")}. Please contact the vendor to increase the number of end users for this license key.");
                return;
            }

            long dObjId = -1;
            foreach (var dObj in response.LicenseKey.DataObjects)
            {
                if (dObj.Name == "cryptolens_assemblyhash")
                {
                    dObjId = dObj.Id;
                    break;
                }
            }

            if (dObjId == -1)
            {
                var dObjRes = Data.AddDataObject(vendorConfig.DataObjectToken, new AddDataObjectToKeyModel
                {
                    Name = "cryptolens_assemblyhash",
                    ProductId = vendorConfig.ProductId,
                    StringValue = "",
                    Key = userConfig.Key,
                    CheckForDuplicates = true
                });

                if (!Helpers.IsSuccessful(dObjRes))
                {
                    Console.WriteLine($"Warning: Could not add data object, '{(dObjRes != null ? dObjRes.Message : "")}'.");
                }
                else
                {
                    dObjId = dObjRes.Id;
                }
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(pathToUserConfig));

            foreach (var path in userConfig.Assemblies)
            {
                if(string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    Console.WriteLine($"Warning: incorrect path, '{path}'. Ignoring it.");
                    continue;
                }
                var dir = Path.GetDirectoryName(path);

                using (var stream = File.OpenRead(path))
                {
                    var sig = Convert.ToBase64String(sha.ComputeHash(stream));
                    Data.SetStringValue(vendorConfig.DataObjectToken, new ChangeStringValueToKeyModel { Id = dObjId, Key= userConfig.Key, ProductId = vendorConfig.ProductId, StringValue = sig  } );
                    var cert = Key.Activate(vendorConfig.ActivateToken, vendorConfig.ProductId, userConfig.Key, Helpers.GetMachineCodePI());
                    var certName = Path.Combine(dir, Path.GetFileName(path) + ".skm");
                    File.WriteAllText(certName, JsonConvert.SerializeObject(cert));

                    Console.WriteLine($"Assembly {path} was successfully signed.");
                }
            }

            Console.WriteLine("Process complete.");
        }
    }
}
