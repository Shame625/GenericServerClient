using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Infrastructure.Packets
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
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
