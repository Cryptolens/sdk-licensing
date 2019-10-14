using System;

using System.Reflection;

using System.Security.Cryptography;
using System.Security.Policy;

namespace SDK_netcore
{
    public class SDK
    {
        public static string Name()
        {
            //var assembly = Assembly.GetEntryAssembly();
            var assembly = System.Reflection.Assembly.GetCallingAssembly();

            

            var hash = new Hash(assembly);

            var hashVal = hash.GenerateHash(new System.Security.Cryptography.SHA512Managed());

            return Convert.ToBase64String(hashVal);
        }
    }
}
