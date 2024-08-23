using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Client_Application.Controls
{
    /// <summary>
    /// File_Block.xaml 的互動邏輯
    /// </summary>
    public partial class File_Block : UserControl
    {
        long Target_ID = 0;
        string Target_Server_File_Name = "";
        public File_Block()
        {
            InitializeComponent();
            Loaded += delegate
            {
                Message_Block.Set_Alignment(Message_Block.Text_Direction.CENTER);
            };
        }
        public void Set_Text(string Message)
        {
            Message_Block.Set_Text(Message);
        }
        public void Set_Target_ID(long Target_ID)
        {
            this.Target_ID = Target_ID;
        }
        public void Set_Target_Server_File_Name(string Target_Server_File_Name)
        {
            this.Target_Server_File_Name = Target_Server_File_Name;
        }

        private void Download_File(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.FileName = Target_Server_File_Name;
            if (file.ShowDialog() == true)
            {
                Global.Get_Server().Download_File(file.FileName, Target_ID, Target_Server_File_Name);
            }
        }
    }
}
