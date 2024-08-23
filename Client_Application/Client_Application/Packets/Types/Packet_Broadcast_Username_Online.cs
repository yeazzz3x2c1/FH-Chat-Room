using Client_Application.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client_Application.Packets.Types
{
    internal class Packet_Broadcast_Username_Online : Packet_Join_Chat_Request
    {
        public Packet_Broadcast_Username_Online(string Name) : base(Name)
        {
            packet_type = Packet_Type.Broadcast_Username_Online;
        }

        public Packet_Broadcast_Username_Online(byte[] data) : base(data)
        {
            packet_type = Packet_Type.Broadcast_Username_Online;
        }
        public override void Handle()
        {
            Window current_window = Global.Get_Current_Window();
            if (current_window == null) return;
            if (!(current_window is Chat_Room_Page)) return;
            Global.Run_Task_On_UI(new Task(() => ((Chat_Room_Page)current_window).Add_Broadcast_Message(Get_Name() + " 上線囉!")));
        }
    }
}
