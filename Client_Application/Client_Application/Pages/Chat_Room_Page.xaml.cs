using Client_Application.Controls;
using Microsoft.Win32;
using System;
using System.Windows;
using static Client_Application.Controls.Message_Block;
using static System.Net.Mime.MediaTypeNames;

namespace Client_Application.Pages
{
    public partial class Chat_Room_Page : Window
    {
        public Chat_Room_Page()
        {
            InitializeComponent();
            Loaded += delegate
            {
                Add_Broadcast_Message("歡迎您的加入。");
            };
            Closing += delegate { Global.Disconnect_From_Server(); };
        }
        public void Add_Broadcast_Message(string Text) => Add_Text(Text, Text_Direction.CENTER);
        public void Add_File_Message(string Text,long ID, string File_Name)
        {
            File_Block block = new File_Block();
            block.Set_Text(Text);
            block.Set_Target_ID(ID);
            block.Set_Target_Server_File_Name(File_Name);
            Message_Container.Children.Add(block);
        }
        public void Add_Text(string Text, Text_Direction Direction)
        {
            Message_Block block = new Message_Block();
            block.Set_Text(Text);
            block.Set_Alignment(Direction);
            Message_Container.Children.Add(block);
        }

        private void On_Key_Down(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Global.Get_Server().Send_Message(Input_Message.Text);
                Input_Message.Text = "";
            }
        }
        private void On_Upload_File(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                Global.Get_Server().Send_File(path);
            }
        }
    }
}
