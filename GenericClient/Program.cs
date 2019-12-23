using ClientInfrastructure;
using Infrastructure;
using Infrastructure.Packets;
using Infrastructure.Packets.Login;
using Infrastructure.Packets.Message;
using Infrastructure.Packets.Register;
using Infrastructure.Packets.Test;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        private static Socket _clientSocket;
        private static readonly byte[] _buffer = new byte[10000];
        public static Connection c = new Connection(null);

        static void Main()
        {
            Connect();

            Thread.Sleep(2000);

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

                if (b == "MSGHISTORY")
                {
                    Send(new CMSG_LastMessages().Serialize());
                }
                else if (b.Contains("SPAWN CUBE"))
                {
                    var param = b.Split(" ");
                    var packet = new CMSG_SpawnObject { objType = Infrastructure.Enums.Objects.Cube, x = Convert.ToInt32(param[2]), y = Convert.ToInt32(param[3]), z = Convert.ToInt32(param[4]) };
                    Send(packet.Serialize());
                }
                else if(b.Contains("SPAWN SPHERE"))
                {
                    var param = b.Split(" ");
                    var packet = new CMSG_SpawnObject { objType = Infrastructure.Enums.Objects.Sphere, x = Convert.ToInt32(param[2]), y = Convert.ToInt32(param[3]), z = Convert.ToInt32(param[4]) };
                    Send(packet.Serialize());
                }
                else
                {
                    Send(new CMSG_Message() { Message = b }.Serialize());
                }
            }
        }

        static void Connect()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //var ipAddress = Dns.GetHostEntry(hostNameOrAddress: "brane1.westeurope.cloudapp.azure.com").AddressList.First();
            //IPEndPoint endPoint = new IPEndPoint(ipAddress, 50000);
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
                dataBuff.PrintData(true);

                packet = dataBuff.Deserialize();

                var test = packet.Execute();

                if (test != null && !test.IsVoidResult)
                {
                    //send bytes
                    Send(test.PacketBytes);
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
