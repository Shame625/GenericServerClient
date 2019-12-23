using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Infrastructure.Packets.Error
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class SMSG_Error : BasePacket
    {
        public SMSG_Error() : base(OpCodes.SMSG_Error) { }

        public ErrorType errorType;
    }
}
