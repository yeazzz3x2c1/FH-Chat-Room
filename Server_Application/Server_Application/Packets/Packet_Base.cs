using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application.Packets
{
    internal class Packet_Base
    {
        protected Packet_Type packet_type = Packet_Type.Default;
        public Packet_Type Get_Type() => packet_type;
        protected byte[] data = null;
        protected Client client;
        public Packet_Base() { }
        public Packet_Base(Client client, byte[] data)
        {
            this.client = client;
            List<byte> list = new List<byte>(data);
            list.RemoveAt(0);
            this.data = list.ToArray();
        }
        public virtual void Handle() { }
        public virtual byte[] Encode_Packet() { return null; }
        public virtual void Decode_Packet() { }
    }
}
