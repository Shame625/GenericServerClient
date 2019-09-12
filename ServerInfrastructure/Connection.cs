using DatabaseCore.Models;
using System;
using System.Net.Sockets;

namespace ServerInfrastructure
{
    public class Connection
    {
        public Socket Socket { get; set; }
        public User User { get; set; }
        public Connection(Socket socket)
        {
            this.Socket = socket;
        }

        public string ConnectionHandle
        {
            get
            {
                if(User != null && User.UserId != Guid.Empty)
                {
                    return GetHashCode().ToString() + " | " + User.UserId + " | " + User.UserName;
                }
                else
                {
                    return GetHashCode().ToString();
                }
            }
        }
    }
}