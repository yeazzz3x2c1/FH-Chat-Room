using System.Text;

namespace Server_Application.Packets.Types
{
    internal class Packet_Text_Message : Packet_Base
    {
        private string Message = "";
        public Packet_Text_Message(Client client, byte[] data) : base(client, data) { packet_type = Packet_Type.Text_Message; }
        public Packet_Text_Message(string Message)
        {
            this.Message = Message;
            packet_type = Packet_Type.Text_Message;
        }

        public override void Handle()
        {
            client.Get_Server().Broadcast(Packet_Type.Text_Message, data);
        }
        public string Get_Message() => Message;
        public override byte[] Encode_Packet()
        {
            byte[] encoded = Encoding.UTF8.GetBytes(Message);
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
            Message = Encoding.UTF8.GetString(bytes);
        }
    }
}
