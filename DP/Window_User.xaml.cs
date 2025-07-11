using System.Data;
using System.Windows;

namespace KP_Mitsura
{
    public partial class Window_User : Window
    {
        public Window_User()
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
        private void Button_Read_Catalog_Click(object sender, RoutedEventArgs e)
        {
            Window_Read_Catalog window = new Window_Read_Catalog();
            window.ShowDialog();
        }
        private void Button_Read_Message_Click(object sender, RoutedEventArgs e)
        {
            Window_Read_Message window = new Window_Read_Message();
            window.ShowDialog();
        }
    }
}
