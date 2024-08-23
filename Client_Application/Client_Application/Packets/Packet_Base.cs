using Client_Application.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Application
{
    internal class Packet_Base
    {
        protected Packet_Type packet_type = Packet_Type.Default;
        public Packet_Type Get_Type() => packet_type;
        protected byte[] data = null;
        public Packet_Base() { }
        public Packet_Base(byte[] data)
        {
            List<byte> list = new List<byte>(data);
            list.RemoveAt(0);
            this.data = list.ToArray();
        }
        public virtual void Handle() { }
        public virtual byte[] Encode_Packet() { return null; }
        public virtual void Decode_Packet() { }
    }
}
