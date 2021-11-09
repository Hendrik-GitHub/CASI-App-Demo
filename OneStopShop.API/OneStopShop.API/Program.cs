using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStopShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .CaptureStartupErrors(true)
            .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
            .ConfigureAppConfiguration((builderContext, config) => 
            { 
                config.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true); 
            })
            .UseStartup<Startup>()
            .Build();         
    }
}
