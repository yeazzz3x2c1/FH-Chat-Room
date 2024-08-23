using Client_Application.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client_Application.Packets.Types
{
    internal class Packet_Join_Chat_Request : Packet_Base
    {
        private string Name = "";
        public Packet_Join_Chat_Request(byte[] data) : base(data) { packet_type = Packet_Type.Join_Chat_Request; }
        public Packet_Join_Chat_Request(string Name)
        {
            this.Name = Name;
            packet_type = Packet_Type.Join_Chat_Request;
        }

        public override void Handle()
        {
            if (Name == "!!invlid")
                MessageBox.Show("這是無法使用的使用者名稱");
            else
                Global.Run_Task_On_UI(new Task(() => { Global.Open_Window(new Chat_Room_Page()); }));
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
