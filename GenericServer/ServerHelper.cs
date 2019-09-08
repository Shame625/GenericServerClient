using System;
using System.Collections.Generic;
using System.Text;

namespace GenericServer
{
    public static class ServerHelper
    {
        public static void UpdateConsoleTitle(int numberOfConnections)
        {
            Console.Title = "Server running. Connections: " + numberOfConnections;
        }
    }
}
