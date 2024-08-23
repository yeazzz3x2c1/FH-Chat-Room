using Client_Application.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_Application
{
    /// <summary>
    /// Interaction logic for Waiting_Page.xaml
    /// </summary>
    public partial class Connecting_Page : Window
    {
        public Connecting_Page()
        {
            InitializeComponent();
            Global.Initialize_First_Window(this);
            Loaded += delegate { Initialize(); };
        }
        private void Initialize()
        {
            Server server = new Server();
            Global.Set_Server(server);
            server.Server_Connected += delegate
            {
                Global.Open_Window(new Username_Page());
            };
            server.Connection_Failed += delegate
            {
                new Thread(() =>
                {
                    Thread.Sleep(1000);
                    server.Connect();
                }).Start();
            };
            server.Connect();
        }
    }
}
