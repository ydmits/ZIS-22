using System.Data;
using System.Windows;

namespace KP_Mitsura
{
    public partial class Window_Admin : Window
    {
        public Window_Admin()
        {
            InitializeComponent();
            Block_FIO.Text = authUser.getFIO();
        }       
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Relogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
        private void Button_Edit_Catalog_Click(object sender, RoutedEventArgs e)
        {
            Window_Edit_Catalog window = new Window_Edit_Catalog();
            window.ShowDialog();
        }
        private void Button_Edit_Users_Click(object sender, RoutedEventArgs e)
        {
            Window_Edit_Users window = new Window_Edit_Users();
            window.ShowDialog();
        }
        private void Button_Logs_Click(object sender, RoutedEventArgs e)
        {
            Window_Logs window = new Window_Logs();
            window.ShowDialog();
        }
        private void Button_Edit_Message_Click(object sender, RoutedEventArgs e)
        {
            Window_Edit_Message window = new Window_Edit_Message();
            window.ShowDialog();
        }
    }
}
