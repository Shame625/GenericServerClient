using System.Net.Sockets;

namespace Infrastructure
{
    public class Connection
    {
        public Socket socket { get; set; }
        public Connection(Socket socket)
        {
            this.socket = socket;
        }
    }
}