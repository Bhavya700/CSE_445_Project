using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SecurityUtils.Services
{
    public static class HashingOptions
    {
        public static int SaltSize =>
            int.Parse(ConfigurationManager.AppSettings["HashSaltSize"] ?? "16");
        public static int Iterations =>
            int.Parse(ConfigurationManager.AppSettings["HashIterations"] ?? "10000");
        public static int KeySize =>
            int.Parse(ConfigurationManager.AppSettings["HashKeySize"] ?? "32");
    }
}
