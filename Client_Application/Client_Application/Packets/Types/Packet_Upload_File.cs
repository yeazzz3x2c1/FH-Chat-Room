using Client_Application.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client_Application.Packets.Types
{
    internal class Packet_Upload_File : Packet_Base
    {
        private long ID = 0;
        private string Path = "";
        private string File_Name = "";
        private byte[] Raw_Data = null;
        public Packet_Upload_File(byte[] data) : base(data) { packet_type = Packet_Type.Upload_File; }
        public Packet_Upload_File(string Path)
        {
            this.Path = Path;
            packet_type = Packet_Type.Upload_File;
        }

        public override void Handle()
        {
            Window current_window = Global.Get_Current_Window();
            if (current_window == null) return;
            if (!(current_window is Chat_Room_Page)) return;
            Global.Run_Task_On_UI(new Task(() => ((Chat_Room_Page)current_window).Add_File_Message("檔案" + File_Name + "已完成上傳", ID, File_Name)));
        }
        public override byte[] Encode_Packet()
        {
            FileStream f = new FileStream(Path, FileMode.Open);
            File_Name = System.IO.Path.GetFileName(Path);

            byte[] file_name = Encoding.UTF8.GetBytes(File_Name);
            int file_name_length = file_name.Length;
            byte[] data_bytes = new byte[f.Length];
            f.Read(data_bytes, 0, data_bytes.Length);
            f.Close();
            int data_length = data_bytes.Length;
            List<byte> result = new List<byte>();
            result.Add((byte)file_name_length);
            result.AddRange(file_name);
            result.Add((byte)(data_length & 0xFF));
            result.Add((byte)((data_length >> 8) & 0xFF));
            result.Add((byte)((data_length >> 16) & 0xFF));
            result.Add((byte)((data_length >> 24) & 0xFF));
            result.AddRange(data_bytes);
            return result.ToArray();
        }
        public override void Decode_Packet()
        {
            long ID = 0;
            for (int i = 7; i > -1; i--)
            {
                ID <<= 8;
                ID |= data[i];
            }
            int file_name_length = data[8];
            byte[] file_name = new byte[file_name_length];
            for (int i = 0; i < file_name_length; i++)
                file_name[i] = data[i + 1 + 8];
            this.ID = ID;
            File_Name = Encoding.UTF8.GetString(file_name);
        }
    }
}
