using Server_Application.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    internal class Server
    {
        int Port = 25565;
        Socket server;
        private List<Client> clients = new List<Client>();
        private void Add_Client(Client client)
        {
            lock (clients)
            {
                if (clients.Contains(client))
                    return;
                clients.Add(client);
            }
        }
        private void Remove_Client(Client client)
        {
            lock (clients)
            {
                if (!clients.Contains(client))
                    return;
                clients.Remove(client);
            }
        }
        public Server() { }
        public void Build_Up()
        {
            new Thread(() =>
            {
                Console.WriteLine("正在啟動伺服器，埠號: " + Port);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(new IPEndPoint(IPAddress.Any, Port));
                server.Listen(10);
                Console.WriteLine("伺服器啟動成功");
                while (true)
                {
                    try
                    {
                        Client client = new Client(this, server.Accept());
                        client.On_Disconnected += Remove_Client;
                        Add_Client(client);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }).Start();
        }
        public void ShutDown()
        {
            try
            {
                server.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void Send_To(Client client, byte[] message)
        {
            client.Send_Raw(message);
        }
        public void Send_To(Client client, Packet_Type packet, byte[] message)
        {
            client.Send( packet, message);
        }
        public void Broadcast(byte[] message)
        {
            lock (clients)
                foreach (Client client in clients)
                    Send_To(client, message);
        }
        public void Broadcast(Packet_Type type,  byte[] message)
        {
            lock (clients)
                foreach (Client client in clients)
                    Send_To(client, type, message);
        }
    }
}
