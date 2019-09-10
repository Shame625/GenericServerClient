﻿using ClientInfrastructure;
using Infrastructure;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using Infrastructure.Packets.Message;
using Infrastructure.Packets.Register;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        private static Socket _clientSocket;
        private static readonly byte[] _buffer = new byte[1024];
        public static Connection c = new Connection(null);

        static void Main(string[] args)
        {
            Connect();

            System.Threading.Thread.Sleep(2000);

            
            while (ClientStates.loginStatus == -1)
            {
                Console.WriteLine("1. Register\n2. Login");
                try
                {
                    var result = Convert.ToInt32(Console.ReadLine());
                    switch (result)
                    {
                        case 1:
                            string name = Console.ReadLine();
                            var regPacket = new CMSG_Register() { UserName = name };
                            Send(regPacket.Serialize());
                            break;
                        case 2:
                            string loginName = Console.ReadLine();
                            var loginPacket = new CMSG_Login() { UserName = loginName };
                            Send(loginPacket.Serialize());
                            break;
                    }
                }
                catch
                {
                }
            }

            while(true)
            {
                var b = Console.ReadLine();

                Send(new CMSG_Message() { Message = b }.Serialize());
            }

            Console.ReadKey();
        }

        static void Connect()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("93.142.166.126"), 50000);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 50000);

            if (_clientSocket.Connected)
                _clientSocket.Close();

            _clientSocket.BeginConnect(endPoint, ConnectCallback, null);
        }

        static void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket.EndConnect(AR);
                Console.WriteLine("Connected!");
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //_buffer = new byte[1024];
            }
        }

        static void ReceiveCallback(IAsyncResult AR)
        {
            //number of recieved bytes
            int received = _clientSocket.EndReceive(AR);

            if (received == 0)
            {
                return;
            }

            //temporary buffer, that copies last bytes it got to process them
            byte[] dataBuff = new byte[received];
            Array.Copy(_buffer, dataBuff, received);

            //Initialize packet
            BasePacket packet = null;

            try
            {
                //dataBuff.PrintData(false);

                packet = dataBuff.Deserialize();

                var test = packet.Execute();
                if (test != null && test.byteResult != null && test.byteResult.Length != 0)
                {
                    //send bytes
                    Send(test.byteResult);
                }
                else
                {
                    _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private static void Send(byte[] data)
        {
            //data.PrintData(true);
            if(data != null && data.Length != 0)
                _clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), _clientSocket);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
