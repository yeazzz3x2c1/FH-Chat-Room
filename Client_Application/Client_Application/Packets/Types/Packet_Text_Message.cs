using Client_Application.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client_Application.Packets.Types
{
    internal class Packet_Text_Message : Packet_Base
    {
        private string Message = "";
        public Packet_Text_Message(byte[] data) : base(data) { packet_type = Packet_Type.Text_Message; }
        public Packet_Text_Message(string Message)
        {
            this.Message = Message;
            packet_type = Packet_Type.Text_Message;
        }

        public override void Handle()
        {
            Window current_window = Global.Get_Current_Window();
            if (current_window == null) return;
            if (!(current_window is Chat_Room_Page)) return;
            Global.Run_Task_On_UI(new Task(() => ((Chat_Room_Page)current_window).Add_Text(Message, Controls.Message_Block.Text_Direction.LEFT)));
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
