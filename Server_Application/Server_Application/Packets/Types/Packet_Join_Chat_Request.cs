using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application.Packets.Types
{
    internal class Packet_Join_Chat_Request : Packet_Base
    {
        private string Name = "";
        public Packet_Join_Chat_Request(Client client, byte[] data) : base(client, data) { packet_type = Packet_Type.Join_Chat_Request; }
        public Packet_Join_Chat_Request(string Name)
        {
            this.Name = Name;
            packet_type = Packet_Type.Join_Chat_Request;
        }

        public override void Handle()
        {
            client.Set_Nickname(Name);
            client.Send(packet_type, data);
            client.Get_Server().Broadcast(Packet_Type.Broadcast_Username_Online, data);
        }
        public string Get_Name() => Name;
        public override byte[] Encode_Packet()
        {
            byte[] encoded = Encoding.UTF8.GetBytes(Name);
            int length = encoded.Length;
            List<byte> result = new List<byte>();
            result.Add((byte)length);
            result.AddRange(encoded);
            return result.ToArray();
        }
        public override void Decode_Packet()
        {
            int length = data[0];
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
                bytes[i] = data[i + 1];
            Name = Encoding.UTF8.GetString(bytes);
        }
    }
}
