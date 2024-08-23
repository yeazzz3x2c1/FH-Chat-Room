using System.Text;

namespace Server_Application.Packets.Types
{
    internal class Packet_Upload_File : Packet_Base
    {
        private byte[] Raw_Data = null;
        private long ID = 0;
        private string File_Name = "";
        public Packet_Upload_File(Client client, byte[] data) : base(client, data) { packet_type = Packet_Type.Upload_File; }
        public Packet_Upload_File(long ID, string File_Name)
        {
            this.ID = ID;
            this.File_Name = File_Name;
            packet_type = Packet_Type.Upload_File;
        }

        public override void Handle()
        {
            new Thread(() =>
            {
                //Save File
                string Dir_Path = "Data/" + ID + "/";
                Directory.CreateDirectory(Dir_Path);
                FileStream f = new FileStream(Dir_Path + File_Name, FileMode.CreateNew);
                f.Write(Raw_Data);
                f.Close();
                //Broadcast
                client.Get_Server().Broadcast(Packet_Type.Upload_File, Encode_Packet());
            }).Start();
        }
        public override byte[] Encode_Packet()
        {
            List<byte> result = new List<byte>();
            long ID_Copy = ID;
            for (int i = 0; i < 8; i++)
            {
                result.Add((byte)(ID_Copy & 0xFF));
                ID_Copy >>= 8;
            }
            byte[] file_name = Encoding.UTF8.GetBytes(File_Name);
            int file_name_length = file_name.Length;
            result.Add((byte)file_name_length);
            result.AddRange(file_name);
            return result.ToArray();
        }
        public override void Decode_Packet()
        {
            int file_name_length = data[0];
            byte[] file_name = new byte[file_name_length];
            for (int i = 0; i < file_name_length; i++)
                file_name[i] = data[i + 1];
            int data_length = 0;
            for (int i = 4; i > 0; i--)
            {
                data_length <<= 8;
                data_length |= data[file_name_length + i];
            }
            byte[] data_bytes = new byte[data_length];
            for (int i = 0; i < data_length; i++)
                data_bytes[i] = data[i + 1 + 4 + file_name_length];
            File_Name = Encoding.UTF8.GetString(file_name);
            Raw_Data = data_bytes;
            ID = DateTime.Now.Ticks;
        }
    }
}
