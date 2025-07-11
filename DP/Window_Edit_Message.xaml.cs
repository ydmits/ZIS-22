using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;

namespace KP_Mitsura
{
    public partial class Window_Edit_Message : Window
    {
        DataTable table = new DataTable();
        public Window_Edit_Message()
        {
            InitializeComponent();
            Load_Table_Message();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void Load_Table_Message()
        {
            table.Rows.Clear();
            gridUsersTable.ItemsSource = null;
            gridUsersTable.Items.Clear();
            table = await DB.QuerySIDAsync("SELECT user_message.id, user_message.date, user_message.user_login, user_message.message, message_status.status AS status, user_message.file_name, user_message.file_path FROM user_message JOIN message_status ON user_message.status_id = message_status.id");
            table.Columns[1].ColumnName = "Дата";
            table.Columns[2].ColumnName = "Логин";
            table.Columns[3].ColumnName = "Сообщение";
            table.Columns[4].ColumnName = "Статус";
            table.Columns[5].ColumnName = "Название файла";
            gridUsersTable.ItemsSource = table.DefaultView;
        }
        private void MenuItem_Open_File_Click(object sender, RoutedEventArgs e)
        {
            if (gridUsersTable.SelectedItem is DataRowView selectedRow)
            {
                string filePath = selectedRow["file_path"].ToString();
                if (File.Exists(filePath))
                {
                    if (filePath.ToLower().EndsWith(".pdf") || filePath.ToLower().EndsWith(".jpg") || filePath.ToLower().EndsWith(".jpeg") || filePath.ToLower().EndsWith(".png"))
                    {
                        FileViewerWindow viewer = new FileViewerWindow();
                        viewer.LoadFile(filePath);
                        viewer.Show();
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
                else
                {
                    Win_Meaasge_Box.MsgB("Файл не найден");
                }
            }
            else
            {
                Win_Meaasge_Box.MsgB("Сначала нужно выбрать сообщение");
            }
        }

        private void gridUsersTable_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow clickedRow = VisualUpwardSearch(e.OriginalSource as DependencyObject) as DataGridRow;
            if (clickedRow != null)
            {
                clickedRow.IsSelected = true;
            }
        }
        static DependencyObject VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(DataGridRow))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }
        private async void MenuItem_Delete_Message_Click(object sender, RoutedEventArgs e)
        {
            if (gridUsersTable.SelectedItem is DataRowView selectedRow)
            {
                int messageId = Convert.ToInt32(selectedRow["id"]);
                string query = "DELETE FROM `user_message` WHERE `id` = @id";
                var parameters = new Dictionary<string, (MySqlDbType, object)>
        {
            { "@id", (MySqlDbType.Int32, messageId) }
        };
                await DB.QuerySIDAsync(query, parameters);
                await DB.AddLogAsync(new String[] { "Сообщения: удалена запись пользователя", selectedRow["Логин"].ToString(), "\"", selectedRow["Сообщение"].ToString(), "\"", "от", authUser.Login, "id =", authUser.Id.ToString() });
                Load_Table_Message();
            }
            else
            {
                Win_Meaasge_Box.MsgB("Сначала нужно выбрать сообщение");
            }
        }
        private void MenuItem_Status_New_Message_Click(object sender, RoutedEventArgs e)
        {
            Update_Status(1);
        }
        private void MenuItem_Status_Work_Message_Click(object sender, RoutedEventArgs e)
        {
            Update_Status(2);
        }
        private void MenuItem_Status_Close_Message_Click(object sender, RoutedEventArgs e)
        {
            Update_Status(3);
        }
        private async void Update_Status(Int32 status_id)
        {
            if (gridUsersTable.SelectedItem is DataRowView selectedRow)
            {
                int messageId = Convert.ToInt32(selectedRow["id"]);
                string query = "UPDATE `user_message` SET `status_id` = @status_id WHERE `id` = @id";
                var parameters = new Dictionary<string, (MySqlDbType, object)>
                {
                    { "@status_id", (MySqlDbType.Int32, status_id) },
                    { "@id", (MySqlDbType.Int32, messageId) }
                };
                await DB.QuerySIDAsync(query, parameters);
                await DB.AddLogAsync(new String[] { "Сообщения: изменение статуса записи пользователя", selectedRow["Логин"].ToString(), "\"", selectedRow["Сообщение"].ToString(), "\"", "от", authUser.Login, "id =", authUser.Id.ToString() });
                Load_Table_Message();
            }
            else
            {
                Win_Meaasge_Box.MsgB("Сначала нужно выбрать сообщение");
            }
        }
        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            string filterText = SearchBox.Text;
            if (!string.IsNullOrEmpty(filterText))
            {
                table.DefaultView.RowFilter = string.Format(
                    "Convert([Дата], 'System.String') LIKE '%{0}%' OR " +
                    "[Логин] LIKE '%{0}%' OR " +
                    "[Сообщение] LIKE '%{0}%' OR " +
                    "[Статус] LIKE '%{0}%' OR " +
                    "[Название файла] LIKE '%{0}%'", filterText);
            }
            else
            {
                table.DefaultView.RowFilter = string.Empty;
            }
        }

        private void gridUsersTable_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "id")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
            }
            if (e.PropertyName == "file_path")
            {
                
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
