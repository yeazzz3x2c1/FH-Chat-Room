using System.Text;

namespace Server_Application.Packets.Types
{
    internal class Packet_Download_File : Packet_Base
    {
        private string File_Name = "";
        private long ID = 0;
        private byte[] File_Raw_Data;
        public Packet_Download_File(Client client, byte[] data) : base(client, data) { packet_type = Packet_Type.Download_File; }
        public Packet_Download_File(long ID, string File_Name)
        {
            this.ID = ID;
            this.File_Name = File_Name;
            packet_type = Packet_Type.Download_File;
        }

        public override void Handle()
        {
            new Thread(() =>
            {
                //Read File
                string Dir_Path = "Data/" + ID + "/";
                Directory.CreateDirectory(Dir_Path);
                FileStream f = new FileStream(Dir_Path + File_Name, FileMode.Open);
                File_Raw_Data = new byte[f.Length];
                f.Read(File_Raw_Data, 0, File_Raw_Data.Length);
                f.Close();
                //Broadcast
                client.Get_Server().Send_To(client, Packet_Type.Download_File, Encode_Packet());
            }).Start();
        }
        public override byte[] Encode_Packet()
        {
            List<byte> result = new List<byte>();
            int file_length = File_Raw_Data.Length;
            for (int i = 0; i < 4; i++)
            {
                result.Add((byte)(file_length & 0xFF));
                file_length >>= 8;
            }
            result.AddRange(File_Raw_Data);
            long ID_Temp = ID;
            for(int i = 0; i < 8; i++)
            {
                result.Add((byte)(ID_Temp & 0xFF));
                ID_Temp >>= 8;
            }
            return result.ToArray();
        }
        public override void Decode_Packet()
        {
            int file_name_length = data[0];
            byte[] file_name = new byte[file_name_length];
            for (int i = 0; i < file_name_length; i++)
                file_name[i] = data[i + 1];
            long data_id = 0;
            for (int i = 8; i > 0; i--)
            {
                data_id <<= 8;
                data_id |= data[file_name_length + i];
            }
            File_Name = Encoding.UTF8.GetString(file_name);
            ID = data_id;
        }
    }
}
