using Infrastructure.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Infrastructure.Packets.Message
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CMSG_Message : BasePacket
    {
        public CMSG_Message() : base(OpCodes.CMSG_Message) { }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Message;
    }
}
