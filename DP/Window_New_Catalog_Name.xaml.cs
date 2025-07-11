using System;
using System.Windows;
using System.Windows.Media;

namespace KP_Mitsura
{
    public partial class Window_New_Catalog_Name : Window
    {
        public Window_New_Catalog_Name()
        {
            InitializeComponent();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Add_Catalog_Click(object sender, RoutedEventArgs e)
        {
            String name = nameBox.Text;
            Boolean correct_flag = false;
            if (name == null)
            {
                Win_Meaasge_Box.MsgB("Название не было указано");
                this.Close();
            }
            if (name != null && Crypt.Check_Correct(name))
            {
                nameBox.ToolTip = "Некорректные данные";
                nameBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }

            else if (name != null)
            {
                nameBox.ToolTip = "";
                nameBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            if (correct_flag)
            {
                Window_Edit_Catalog window = (Window_Edit_Catalog)Owner;
                window.Get_Name = name;
                this.Close();
            }
        }
    }
}
