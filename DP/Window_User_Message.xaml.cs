using System;
using System.Windows;
using System.Windows.Media;

namespace KP_Mitsura
{
    public partial class Window_User_Message : Window
    {
        public Window_User_Message()
        {
            InitializeComponent();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Message_Click(object sender, RoutedEventArgs e)
        {
            String message = messageBox.Text;
            Boolean correct_flag;
            if (message.Length < 5)
            {
                messageBox.ToolTip = "Некорректные данные";
                messageBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                messageBox.ToolTip = "";
                messageBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            if (correct_flag)
            {
                Window_Read_Catalog window = (Window_Read_Catalog)Owner;
                window.Get_Message = message;
                this.Close();
            }
        }
    }
}

