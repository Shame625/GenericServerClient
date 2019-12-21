using System;
using System.Collections.Generic;
using System.Text;

namespace ServerInfrastructure.Enums
{
    public enum PacketFilter : UInt16
    {
        Anonymous,
        LoggedIn
    }

    public enum PacketProtectionLevel : UInt16
    {
        Guest,
        User,
        Admin
    }
}
