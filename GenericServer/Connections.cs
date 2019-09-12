using Infrastructure;
using ServerInfrastructure;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace GenericServer
{
    public static class Connections
    {
        public static Dictionary<Socket, Connection> connectedClients = new Dictionary<Socket, Connection>();

        public static bool IsConnected(this Socket socket)
        {
            return connectedClients.ContainsKey(socket);
        }
        public static bool Connect(this Socket socket)
        {
            if (!socket.IsConnected())
            {
                connectedClients.Add(socket, new Connection(socket));
                Console.WriteLine("New socket connection established.");
                return true;
            }
            Console.WriteLine("Client already connected.");

            return false;
        }
    }
}
