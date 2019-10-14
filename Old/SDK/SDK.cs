using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;


namespace SDK
{
    public class SDK
    {
        public static string Name()
        {
            //var assembly = Assembly.GetEntryAssembly();
            var assembly = Assembly.GetCallingAssembly();


            //var hash = new Hash(assembly);

            //var hashVal = hash.GenerateHash(new SHA512Managed());


            //return Convert.ToBase64String(hashVal);


            //SHA512 sha = SHA512.Create();


            //using (var stream = File.OpenRead(assembly.Location))
            //{
            //    return Convert.ToBase64String(sha.ComputeHash(stream));
            //}


            return assembly.Location;
        }

    }
}
