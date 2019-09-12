using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace GenericServer
{
    public static class Program
    {
        public static IConfiguration config = null;
        static void Main()
        {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        
        config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            Server.StartServer();
            Console.ReadLine();
        }
    }
}
