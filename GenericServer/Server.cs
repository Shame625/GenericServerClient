using Infrastructure;
using Microsoft.Extensions.Configuration;
using ServerInfrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GenericServer
{
    public static class Server
    {
        public static IConfiguration config;
        static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static byte[] buffer;
        public static void StartServer()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Console.WriteLine("Setting up server...");
            Console.WriteLine("Receive buffer size set to " + config["bufferSize"] + " bytes.");
            Console.WriteLine("Listening to port: " + config["serverPort"]);
            buffer = new byte[Convert.ToInt32(config["bufferSize"])];

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, Convert.ToInt32(config["serverPort"])));
            serverSocket.Listen(1000);

            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            Console.WriteLine("Server started successfully!");
        }
        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = serverSocket.EndAccept(AR);

            if (socket.Connect())
            {
                ServerHelper.UpdateConsoleTitle(Connections.connectedClients.Count);
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }

            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Connection client = null;
            try
            {
                client = ((Socket)AR.AsyncState).HandleSocket();

                int received = client.socket.EndReceive(AR);

                if (received == 0)
                {
                    return;
                }

                //Deserializes recived bytes
                {
                    byte[] dataBuff = new byte[received];
                    Array.Copy(buffer, dataBuff, received);

                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Client: " + client.GetHashCode());
                    dataBuff.PrintData(false);
                    var receivedPacket = dataBuff.Deserialize();
                    Console.WriteLine("-----------------------------");

                    var data = receivedPacket.Execute(ref client);

                    //Send data
                    if (data != null)
                    {
                        //send to all users
                        if (PacketHandler.packets[receivedPacket.Id].global)
                        {
                            foreach(var v in Connections.connectedClients)
                            {
                                var temp = v.Value;
                                data.SendPacket(ref temp);
                            }
                        }
                        //send to caller
                        else
                        {
                            data.SendPacket(ref client);
                        }
                    }
                }

                //Continue recieving data for client.
                client.socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, client.socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (client != null)
                {
                    Connections.connectedClients.Remove(client.socket);
                    ServerHelper.UpdateConsoleTitle(Connections.connectedClients.Count);
                }
            }
        }
        public static void SendPacket(this byte[] data, ref Connection client)
        {
            try
            {
                client.socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), client.socket);
                data.PrintData(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //close connection in case of error
            }
        }
        private static void SendCallback(IAsyncResult AR)
        {
            try
            {
                Socket clientSocket = (Socket)AR.AsyncState;
                clientSocket.EndSend(AR);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static Connection HandleSocket(this Socket socket)
        {
            //Check if client is already in dictionary, if not add him
            try
            {
                return Connections.connectedClients[socket];
            }
            //If it is not in dictionary, add, return
            catch
            {
                Connections.connectedClients[socket] = new Connection(socket);
                return Connections.connectedClients[socket];
            }
        }
    }
}
