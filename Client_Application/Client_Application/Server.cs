using Client_Application.Packets;
using Client_Application.Packets.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Client_Application
{
    internal class Server
    {
        public delegate void ServerConnectionHandler();
        public event ServerConnectionHandler Server_Connected;
        public event ServerConnectionHandler Connection_Failed;
        public event ServerConnectionHandler Server_Disconnected;

        Socket server;
        TaskScheduler scheduler;
        public Server()
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }
        public void Disconnect()
        {
            if (server == null) return;
            server.Close();
        }
        public void Connect()
        {
            new Thread(() =>
            {
                //連線
                try
                {
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    server.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25565));
                    Listen();
                    //byte[] send_data = Packet_Helper.Encode_Data(new byte[] { (byte)Packet_Type.Test });
                    //server.Send(send_data);
                    new Task(() => { Server_Connected?.Invoke(); }).Start(scheduler);
                }
                catch (Exception ex)
                {
                    new Task(() => { Connection_Failed?.Invoke(); }).Start(scheduler);
                }
            }).Start();
        }
        private void Listen()
        {
            new Thread(() =>
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int length = 0;
                    List<byte> bytes = new List<byte>();
                    while (true)
                    {
                        length = server.Receive(buffer);
                        if (length < 1) break;
                        for (int i = 0; i < length; i++)
                            switch (buffer[i])
                            {
                                case 2:
                                    bytes.Clear();
                                    break;
                                case 3:
                                    byte[] raw_data = Packet_Helper.Decode_Data(bytes.ToArray());
                                    if (Packet_Helper.Check_CRC_Pass(raw_data))
                                    {
                                        Packet_Base packet = Packet_Helper.Get_Packet_Instance(raw_data);
                                        packet.Decode_Packet();
                                        packet.Handle();
                                    }
                                    break;
                                default:
                                    bytes.Add(buffer[i]);
                                    break;
                            }
                    }
                }
                catch (SocketException ex) { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
                new Task(() => { Server_Disconnected?.Invoke(); }).Start(scheduler);
            }).Start();
        }

        public void Send(byte[] content)
        {
            server.Send(content);
        }
        public void Send(Packet_Type type, byte[] content)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte)type);
            bytes.AddRange(content);
            byte[] send_data = Packet_Helper.Encode_Data(bytes.ToArray());
            Send(send_data);
        }
        public void Send(Packet_Base packet)
        {
            Send(packet.Get_Type(), packet.Encode_Packet());
        }
        public void Register_User(string Name)
        {
            Send(new Packet_Join_Chat_Request(Name));
        }
        public void Send_Message(string Message)
        {
            Send(new Packet_Text_Message(Message));
        }
        public void Send_File(string Path)
        {
            Send(new Packet_Upload_File(Path));
        }
        public void Download_File(string Path, long File_ID, string File_Name)
        {
            Send(new Packet_Download_File(File_ID, File_Name, Path));
        }
    }
}
