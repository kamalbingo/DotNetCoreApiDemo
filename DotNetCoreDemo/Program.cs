using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DotNetCoreDemo
{
    public class Program
    {
        //Entry point for application
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

       //Configures web server using the app's hosting configuration providers
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
