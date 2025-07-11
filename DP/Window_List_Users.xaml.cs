using System;
using System.Data;
using System.Windows;

namespace KP_Mitsura
{
    public partial class Window_List_Users : Window
    {
        DataTable table = new DataTable();
        public Window_List_Users()
        {
            InitializeComponent();
            Load_Info();
        }
        private async void Load_Info()
        {
            String querry = "SELECT u.login, u.password, m.mode, u.surname, u.name, u.middle_name FROM users u INNER JOIN mods m ON u.mode_id = m.id";
            table = await DB.QuerySIDAsync(querry);
            table.Columns[0].ColumnName = "Логин";
            table.Columns[1].ColumnName = "Пароль";
            table.Columns[2].ColumnName = "Доступ";
            table.Columns[3].ColumnName = "Фамилия";
            table.Columns[4].ColumnName = "Имя";
            table.Columns[5].ColumnName = "Отчество";
            gridUsersTable.ItemsSource = table.DefaultView;
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string filterText = SearchBox.Text;
            if (!string.IsNullOrEmpty(filterText))
            {
                table.DefaultView.RowFilter = string.Format(
                    "([Логин] LIKE '%{0}%' OR " +
                    "[Пароль] LIKE '%{0}%' OR " +
                    "[Доступ] LIKE '%{0}%' OR " +
                    "[Фамилия] LIKE '%{0}%' OR " +
                    "[Имя] LIKE '%{0}%' OR " +
                    "[Отчество] LIKE '%{0}%')", filterText);
            }
            else
            {
                table.DefaultView.RowFilter = string.Empty;
            }
        }
    }
}
