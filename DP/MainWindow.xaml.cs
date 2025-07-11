using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace KP_Mitsura
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            String login = loginBox.Text.Trim();
            String pass = Crypt.Base64Encrypt(passBox.Password.Trim());
            Boolean correct_flag = is_Corr_Str(login, pass);
            if (correct_flag)
            {
                String querry = "SELECT * FROM `users` WHERE `login` = @logUser AND `password` = @passUser";
                var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@logUser", (MySqlDbType.VarChar, login)},
                    { "@passUser", (MySqlDbType.VarChar, pass)}
                };
                DataTable table = await DB.QuerySIDAsync(querry, parameters);
                if (table.Rows.Count > 0)
                {
                    await DB.AddLogAsync(new String[] { "Авторизация:", table.Rows[0][1].ToString(), "id =", table.Rows[0][0].ToString() });
                    authUser.Login = table.Rows[0][1].ToString();
                    authUser.Mode = Convert.ToInt32(table.Rows[0][3].ToString());
                    authUser.Id = Convert.ToInt32(table.Rows[0][0].ToString());
                    authUser.Name = table.Rows[0][4].ToString();
                    authUser.SurName = table.Rows[0][5].ToString();
                    authUser.MidName = table.Rows[0][6].ToString();
                    if (Convert.ToInt32(table.Rows[0][3].ToString()) == 1)
                    {
                        Window_Admin window = new Window_Admin();
                        window.Show();
                        this.Close();
                    }
                    else if (Convert.ToInt32(table.Rows[0][3].ToString()) == 2)
                    {
                        Window_User window = new Window_User();
                        window.Show();
                        this.Close();
                    }
                }
                else
                {
                    Win_Meaasge_Box.MsgB("Одно или несколько полей заполнено некорректно, или такого пользователя не существует");
                }
            }
        }
        private Boolean is_Corr_Str(String login, String pass)
        {
            Boolean correct_flag = false;
            if (Crypt.Check_Correct(login))
            {
                loginBox.ToolTip = "Некорректные данные";
                loginBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                loginBox.ToolTip = "";
                loginBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            if (Crypt.Check_Correct(Crypt.Base64Decrypt(pass)))
            {
                passBox.ToolTip = "Некорректные данные";
                passBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            return correct_flag;
        }
    }
}