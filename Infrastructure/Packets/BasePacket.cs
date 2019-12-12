using Infrastructure.Helpers;
using System;
using System.Runtime.InteropServices;

namespace Infrastructure.Packets
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public abstract class BasePacket
    {
        public OpCodes Id { get; set; }
        public long Ticks { get; set; }


        public BasePacket(OpCodes opCode)
        {
            Id = opCode;
            Ticks = DateTime.Now.Ticks;
        }

        public DateTime GetTimestamp()
        {
            return new DateTime(Ticks);
        }

        public string GetData()
        {
            return string.Format("{0}", Helper.GetObjectProps(this));
        }
    }
}
