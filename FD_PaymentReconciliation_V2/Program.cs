﻿
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace FD_PaymentReconciliation_V2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration(options =>
               {
                   options.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
               })
                .UseStartup<Startup>();
    }
}
