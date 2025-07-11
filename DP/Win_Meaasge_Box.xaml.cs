using System;
using System.Windows;
using System.Windows.Interop;

namespace KP_Mitsura
{   
    public partial class Win_Meaasge_Box : Window
    {
        public Win_Meaasge_Box(String str)
        {
            InitializeComponent();
            Text1.Text = "Внимание!";
            Text2.Text = str;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public static void MsgB(String str)
        {
            Win_Meaasge_Box window = new Win_Meaasge_Box(str);
            window.ShowDialog();
        }
    }
}
