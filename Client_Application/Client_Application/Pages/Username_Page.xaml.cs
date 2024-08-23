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
using System.Windows.Shapes;

namespace Client_Application.Pages
{
    /// <summary>
    /// Username_Page.xaml 的互動邏輯
    /// </summary>
    public partial class Username_Page : Window
    {
        public Username_Page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Global.Get_Server().Register_User(username.Text);
        }
    }
}
