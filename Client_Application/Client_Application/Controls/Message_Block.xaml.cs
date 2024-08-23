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
using static Client_Application.Pages.Chat_Room_Page;

namespace Client_Application.Controls
{
    /// <summary>
    /// Message_Block.xaml 的互動邏輯
    /// </summary>
    public partial class Message_Block : UserControl
    {
        static SolidColorBrush left_brush = new SolidColorBrush(Colors.DarkGray);
        static SolidColorBrush center_brush = new SolidColorBrush(Colors.Gray);
        static SolidColorBrush right_brush = new SolidColorBrush(Colors.LimeGreen);
        public enum Text_Direction
        {
            LEFT, CENTER, RIGHT
        }
        public Message_Block()
        {
            InitializeComponent();
        }
        public void Set_Text(string Text)
        {
            TextBlock text = new TextBlock();
            text.Text = Text;
            Message.Children.Add(text);
        }

        public void Set_Alignment(Text_Direction Direction)
        {
            HorizontalAlignment alignment;
            switch (Direction)
            {
                case Text_Direction.LEFT:
                    alignment = HorizontalAlignment.Left;
                    Background.Background = left_brush;
                    break;
                case Text_Direction.RIGHT:
                    alignment = HorizontalAlignment.Right;
                    Background.Background = right_brush;
                    break;
                default:
                    alignment = HorizontalAlignment.Center;
                    Background.Background = center_brush;
                    break;
            }
            HorizontalAlignment = alignment;
        }
    }
}
