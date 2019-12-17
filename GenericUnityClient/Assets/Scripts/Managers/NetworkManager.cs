using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using Infrastructure;
using UnityClientInfrastructure;
using Infrastructure.Packets;

public class NetworkManager : MonoBehaviour
{
    private static Socket clientSocket;
    private static byte[] buffer;
    private static Connection connection = new Connection(null);

    private void Awake()
    {
        Connect();
    }

    int i = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var msg = new Infrastructure.Packets.Message.CMSG_Message { Message = "Test" + i.ToString()};
            Send(msg.Serialize());
            i++;
        }
    }

    public void Connect()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("93.142.166.126"), 50000);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 50000);

        if (clientSocket.Connected)
            clientSocket.Close();

        clientSocket.BeginConnect(endPoint, ConnectCallback, null);
    }

    private void ConnectCallback(IAsyncResult AR)
    {
        try
        {
            buffer = new byte[Constants.BUFFER_SIZE];
            clientSocket.EndConnect(AR);
            connection.Socket = clientSocket;
            Debug.Log("Connected!");
            //_clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
            var loginPacket = new Infrastructure.Packets.Login.CMSG_Login { UserName = "branimir" };

            Send(loginPacket.Serialize());
        }
        catch (SocketException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (ObjectDisposedException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    static void ReceiveCallback(IAsyncResult AR)
    {
        Debug.Log("Receive data");
        //number of recieved bytes
        int received = clientSocket.EndReceive(AR);

        if (received == 0)
        {
            return;
        }

        //temporary buffer, that copies last bytes it got to process them
        byte[] dataBuff = new byte[received];
        Array.Copy(buffer, dataBuff, received);

        //Initialize packet
        BasePacket packet = null;

            try
            {
                //dataBuff.PrintData(false);

                packet = dataBuff.Deserialize();

                Debug.Log(Helpers.Helper.GetObjectProps(packet));

            Infrastructure.Models.Result result = null;
            Dispatcher.RunOnMainThread(() =>
            {
                result = packet.Execute();
            });

            Debug.Log("Exectued");
                if (result != null && !result.IsVoidResult)
                {
                    //send bytes
                    Send(result.PacketBytes);
                }
                else
                {
                    clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return;
            }
        
    }

    public static void Send(byte[] data)
    {
        //data.PrintData(true);
        if (data != null && data.Length != 0)
             clientSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), clientSocket);
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
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}