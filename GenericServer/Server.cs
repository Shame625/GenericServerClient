using Infrastructure;
using Infrastructure.Models;
using ServerInfrastructure;
using System;
using System.Net;
using System.Net.Sockets;
using GenericServer.Extensions;
using Infrastructure.Enums;

namespace GenericServer
{
    public static class Server
    {
        public static long NumberOfConnections = 0;
        public static long TotalPacketsSent = 0;
        public static long TotalPacketsRec = 0;

        static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static byte[] buffer;
        public static void StartServer()
        {
            Console.WriteLine("Setting up server...");
            Console.WriteLine("Receive buffer size set to " + ServerSettings.Settings.bufferSize + " bytes.");
            Console.WriteLine("Listening to port: " + ServerSettings.Settings.serverPort);
            buffer = new byte[ServerSettings.Settings.bufferSize];

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, ServerSettings.Settings.serverPort));
            serverSocket.Listen(1000);

            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            Console.WriteLine("Server started successfully!");
        }
        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = serverSocket.EndAccept(AR);

            if (socket.Connect())
            {
                NumberOfConnections = Connections.connectedClients.Count;
                ServerHelper.UpdateConsoleTitle();

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

                int received = client.Socket.EndReceive(AR);

                if (received == 0)
                {
                    return;
                }

                //Deserializes Received bytes
                {
                    byte[] dataBuff = new byte[received];
                    Array.Copy(buffer, dataBuff, received);

                    //dataBuff.PrintData(false);
                    var receivedPacket = dataBuff.Deserialize();
                    TotalPacketsRec++;
                    var data = receivedPacket.Execute(client).Result;


                    var packetType = receivedPacket.GetPacketType();
                    ServerHelper.PrintPacketData(ref client, receivedPacket.GetData(), false, packetType);

                    //Send data
                    if (!data.IsVoidResult)
                    {
                        switch (packetType)
                        {
                            case PacketType.Local:
                                foreach (var v in Connections.connectedClients)
                                {
                                    var temp = v.Value;
                                    data.SendPacket(ref temp);
                                    TotalPacketsSent++;
                                }
                                break;
                            case PacketType.Others:
                                foreach (var v in Connections.connectedClients)
                                {
                                    var temp = v.Value;
                                    if (temp != client)
                                    {
                                        data.SendPacket(ref temp);
                                        TotalPacketsSent++;
                                    }
                                }
                                break;
                            case PacketType.ReturnToSender:
                                data.SendPacket(ref client);
                                TotalPacketsSent++;
                                break;
                        }
                    }
                }

                //Continue recieving data for client.
                client.Socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, client.Socket);

                ServerHelper.UpdateConsoleTitle();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (client != null)
                {
                    Connections.connectedClients.Remove(client.Socket);
                    NumberOfConnections = Connections.connectedClients.Count;
                    ServerHelper.UpdateConsoleTitle();
                }
            }
        }
        public static void SendPacket(this byte[] data, ref Connection client)
        {
            try
            {
                client.Socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), client.Socket);
                //data.PrintData(true);
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

        static void SendPacket(this Result result, ref Connection c)
        {
            result.PacketBytes.SendPacket(ref c);
            ServerHelper.PrintPacketData(ref c, result.Packet.GetData(), true, result.Packet.GetPacketType());
        }
    }
}
