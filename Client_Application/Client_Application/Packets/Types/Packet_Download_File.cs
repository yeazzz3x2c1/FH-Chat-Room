using Client_Application.Pages;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client_Application.Packets.Types
{
    internal class Packet_Download_File : Packet_Base
    {
        private static Dictionary<long, Packet_Download_File> packets = new Dictionary<long, Packet_Download_File>();
        private string Saving_Path = "";
        private string File_Name = "";
        private long ID = 0;
        private byte[] File_Raw_Data;
        public Packet_Download_File(byte[] data) : base(data) { packet_type = Packet_Type.Download_File; }
        public Packet_Download_File(long ID, string File_Name, string Saving_Path)
        {
            this.ID = ID;
            this.File_Name = File_Name;
            this.Saving_Path = Saving_Path;
            packet_type = Packet_Type.Download_File;
            packets.Add(ID, this);
        }

        public override void Handle()
        {
            new Thread(() =>
            {
                Packet_Download_File packet = packets[ID];
                if (packet == this)
                {
                    packets.Remove(ID);
                    FileStream f = new FileStream(Saving_Path, FileMode.CreateNew);
                    f.Write(File_Raw_Data, 0, File_Raw_Data.Length);
                    f.Close();
                    Window current_window = Global.Get_Current_Window();
                    if (current_window == null) return;
                    if (!(current_window is Chat_Room_Page)) return;
                    Global.Run_Task_On_UI(new Task(() => ((Chat_Room_Page)current_window).Add_Broadcast_Message("檔案" + File_Name + "接收完畢")));
                }
                else
                {
                    packet.Set_File_Raw_Data(File_Raw_Data);
                    packet.Handle();
                }
            }).Start();
        }

        public override byte[] Encode_Packet()
        {
            byte[] file_name = Encoding.UTF8.GetBytes(File_Name);
            int file_name_length = file_name.Length;
            byte[] id_bytes = new byte[8];
            long ID_Temp = ID;
            for (int i = 0; i < 8; i++)
            {
                id_bytes[i] = (byte)(ID_Temp & 0xFF);
                ID_Temp >>= 8;
            }
            List<byte> result = new List<byte>();
            result.Add((byte)file_name_length);
            result.AddRange(file_name);
            result.AddRange(id_bytes);
            return result.ToArray();
        }
        public override void Decode_Packet()
        {
            int file_length = 0;
            for (int i = 3; i > -1; i--)
            {
                file_length <<= 8;
                file_length |= data[i];
            }
            File_Raw_Data = new byte[file_length];
            for (int i = 0; i < file_length; i++)
                File_Raw_Data[i] = data[i + 4];
            ID = 0;
            for (int i = 7; i > -1; i--)
            {
                ID <<= 8;
                ID |= data[i + 4 + file_length];
            }
        }
        private void Set_File_Raw_Data(byte[] File_Raw_Data)
        {
            this.File_Raw_Data = File_Raw_Data;
        }
    }
}
