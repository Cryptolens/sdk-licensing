using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Security.Policy;
using System.Security.Cryptography;

namespace SDK_net40
{
    public class SDK
    {
        public static string Name2()
        {
            //var assembly = Assembly.GetEntryAssembly();
            var assembly = Assembly.GetCallingAssembly();

            var hash = new Hash(assembly);

            return Convert.ToBase64String(hash.GenerateHash(new SHA512Managed()));

        }

    }
}
