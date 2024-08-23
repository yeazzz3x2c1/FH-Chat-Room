using Server_Application.Packets.Types;
using Server_Application.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application.Packets
{
    internal enum Packet_Type
    {
        Default = -1,
        Join_Chat_Request = 0,
        Broadcast_Username_Online = 1,
        Text_Message = 2,
        Upload_File = 3,
        Download_File = 4,
    }
    internal class Packet_Helper
    {
        private static Type[] packet_types = new Type[]
        {
            typeof(Packet_Join_Chat_Request),
            typeof(Packet_Broadcast_Username_Online),
            typeof(Packet_Text_Message),
            typeof(Packet_Upload_File),
            typeof(Packet_Download_File),
        };
        public static Packet_Base Get_Packet_Instance(Client client, byte[] Raw_Data)
        {
            return (Packet_Base)Activator.CreateInstance(packet_types[Raw_Data[0]], client, Raw_Data);
        }
        public static byte[] Encode_Data(byte[] Raw_Data)
        {
            List<byte> raw_data = new List<byte>(Raw_Data);
            raw_data.Add(Calculate_CRC(Raw_Data));
            List<byte> bytes = new List<byte>();
            bytes.Add(2);
            bytes.AddRange(Encoding.ASCII.GetBytes(Convert.ToBase64String(raw_data.ToArray())));
            bytes.Add(3);
            return bytes.ToArray();
        }
        public static byte[] Decode_Data(byte[] Encoded_Data)
        {
            List<byte> bytes = new List<byte>(Encoded_Data);
            return Convert.FromBase64String(Encoding.ASCII.GetString(bytes.ToArray()));
        }
        public static byte Calculate_CRC(byte[] Data)
        {
            byte crc = 0;
            for (int i = 0; i < Data.Length; i++)
                crc += Data[i];
            return crc;
        }
        public static bool Check_CRC_Pass(byte[] Data)
        {
            byte crc = 0;
            for (int i = 0; i < Data.Length - 1; i++)
                crc += Data[i];
            return crc == Data[Data.Length - 1];
        }
    }
}
