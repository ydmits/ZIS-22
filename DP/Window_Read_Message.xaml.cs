using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace KP_Mitsura
{
    public partial class Window_Read_Message : Window
    {
        public Window_Read_Message()
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
            String querry = "SELECT user_message.date, user_message.message, message_status.status AS status, user_message.file_name FROM user_message JOIN message_status ON user_message.status_id = message_status.id WHERE user_message.user_id = @user_id";
            var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@user_id", (MySqlDbType.Int32, authUser.Id)}
                };
            DataTable table = await DB.QuerySIDAsync(querry, parameters);            
            gridUsersTable.ItemsSource = table.DefaultView;
        }
    }
}
