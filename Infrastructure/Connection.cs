using DatabaseCore.Models;
using System.Net.Sockets;

namespace Infrastructure
{
    public class Connection
    {
        public Socket Socket { get; set; }

        public User User { get; set; }
        public Connection(Socket socket)
        {
            this.Socket = socket;
        }
    }
}