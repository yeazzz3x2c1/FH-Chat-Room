using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application.Packets.Types
{
    internal class Packet_Broadcast_Username_Online : Packet_Join_Chat_Request
    {
        public Packet_Broadcast_Username_Online(string Name) : base(Name)
        {
            packet_type = Packet_Type.Broadcast_Username_Online;
        }

        public Packet_Broadcast_Username_Online(Client client, byte[] data) : base(client, data)
        {
            packet_type = Packet_Type.Broadcast_Username_Online;
        }
        public override void Handle()
        {
        }
    }
}
