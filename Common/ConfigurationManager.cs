using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ConfigurationManager
    {
        public static IConfiguration Configuration;
        public static void Config(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
