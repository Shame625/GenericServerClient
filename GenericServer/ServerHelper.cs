using ServerInfrastructure;
using System;
using static Infrastructure.Enums.Enums;

namespace GenericServer
{
    public static class ServerHelper
    {
        public static void UpdateConsoleTitle()
        {
            Console.Title = "Server running. Connections: " + Server.NumberOfConnections + " Total Rec: " + Server.TotalPacketsRec + " Total Sent: " + Server.TotalPacketsSent;
        }

        public static void PrintPacketData(ref Connection c, string data, bool isSent, PacketType packetType = PacketType.Nothing)
        {
            Console.ForegroundColor = isSent ? ConsoleColor.DarkRed : ConsoleColor.Green;
            Console.WriteLine("-----------------------------");
            Console.WriteLine("[Client | " + c.ConnectionHandle + "] [PacketType" + " | " + packetType.ToString() + "]");
            Console.WriteLine((isSent ? "SENT: " : "REC: ") + data);
            Console.WriteLine("-----------------------------");
            Console.ResetColor();
        }
    }
}
