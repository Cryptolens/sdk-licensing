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

                var sb = new StringBuilder();

            //foreach (var mod in assembly.Modules)
            //{
            //    sb.Append(mod.Name);
            //    sb.Append("\n");
            //    foreach (var attr in mod.CustomAttributes)
            //    {
            //        sb.Append(attr.AttributeType.ToString());
            //    }

            //    foreach (var type in mod.GetTypes())
            //    {
            //        sb.Append(type.Name);
            //        foreach (var method in type.GetMethods())
            //        {
            //            sb.Append(method.Name);
            //            //sb.Append(Convert.ToBase64String(method.GetMethodBody().GetILAsByteArray()));
            //        }
            //        foreach (var field in type.GetFields())
            //        {
            //            sb.Append(field.Name);
            //            //sb.Append(Convert.ToBase64String(method.GetMethodBody().GetILAsByteArray()));
            //        }
            //    }
            //}
            //SHA512 sha = SHA512.Create();
            //return Convert.ToBase64String(sha.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(assembly, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //}))));
            //System.Reflection.Emit

            //SHA512 sha = SHA512.Create();
            //foreach (var module in assembly.Modules)
            //{
            //    foreach (var method in module.GetMethods())
            //    {
            //        //var res = method.IsAs;

            //        if (res != null)
            //        {
            //            //sha.TransformBlock(res, 0, res.Length, res, 0);

            //            sb.Append(sha.ComputeHash(res.GetILAsByteArray()));
            //            Console.WriteLine("test");
            //        }
            //        Console.WriteLine("test2");

            //    }
            //    //sha.TransformFinalBlock(new byte[] { }, 0, 0);
            //}

            //var runtimeType = typeof(Assembly).GetType().GetType();

            //using (var memoryStream = new System.IO.MemoryStream())
            //{
            //    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    binaryFormatter.Serialize(memoryStream, runtimeType);
            //    string serializedRuntimeTypeBase64 = System.Convert.ToBase64String(memoryStream.ToArray());

            //    return serializedRuntimeTypeBase64;
            //}

            //BinaryFormatter formatter = new BinaryFormatter();


            //MemoryStream stream = new MemoryStream();

            //var formattert = new FormatterConverter();
            //var streamer = new StreamingContext();
            //assembly.GetObjectData(new SerializationInfo(typeof(Assembly), new FormatterConverter()), streamer);


            //formatter.Serialize(stream, assembly);

            //SHA512 sha = SHA512.Create();

            //var hash = Convert.ToBase64String(sha.ComputeHash(stream));

            //return Convert.ToBase64String(sha.Hash);
            //return sb.ToString();
            //return assembly.GetModules()[0]..ToString();

            return sb.ToString();
        }

    }
}
