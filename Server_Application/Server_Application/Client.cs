using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server_Application.Packets;

namespace Server_Application
{
    internal class Client
    {
        string Nickname = "";
        public delegate void Connection_Handler(Client client);
        public event Connection_Handler On_Disconnected;
        Socket client_socket;
        Server server;
        public Client(Server server, Socket client)
        {
            this.server = server;
            this.client_socket = client;
            IPEndPoint ipendpoint = client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine("收到了連線請求，IP:" + ipendpoint.Address + ":" + ipendpoint.Port);
            Listen();
        }
        public void Set_Nickname(string Nickname) => this.Nickname = Nickname;
        public string Get_Nickname() => Nickname;
        public Server Get_Server()
        {
            return server;
        }
        public void Send_Raw(byte[] content)
        {
            client_socket.Send(content);
        }
        public void Send(Packet_Type type, byte[] content)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte)type);
            bytes.AddRange(content);
            byte[] send_data = Packet_Helper.Encode_Data(bytes.ToArray());
            Send_Raw(send_data);
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
                        length = client_socket.Receive(buffer);
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
                                        Packet_Base packet = Packet_Helper.Get_Packet_Instance(this, raw_data);
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
                catch (Exception ex) { }
                On_Disconnected?.Invoke(this);
            }).Start();
        }
    }
}
