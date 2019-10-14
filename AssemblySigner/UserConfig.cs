using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblySigner
{
    public class UserConfig
    {
        public List<string> Assemblies { get; set; }
        public string Key { get; set; }
    }
}
