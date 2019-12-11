using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GenericServer
{
    public static class Program
    {
        private static readonly IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

        static void Main()
        {
            LoadSettings();
            Server.StartServer();
            Console.ReadLine();
        }

        static void LoadSettings()
        {
            var settings = config.GetSection("Settings");
            settings.Bind(ServerSettings.Settings);
        }
    }
}
