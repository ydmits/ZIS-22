using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KP_Mitsura
{
    public partial class Window_Edit_Users : Window
    {
        public Window_Edit_Users()
        {
            InitializeComponent();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_List_Users_Click(object sender, RoutedEventArgs e)
        {
            Window_List_Users window = new Window_List_Users();
            window.ShowDialog();
        }
        private async void Button_Add_User_Click(object sender, RoutedEventArgs e)
        {
            String login = addLogBox.Text.Trim();
            String password = Crypt.Base64Encrypt(addPassBox.Text.Trim());
            Int32 mode = addModeBox.SelectedIndex + 1;
            String surname = addSurBox.Text.Trim();
            String name = addNameBox.Text.Trim();
            String middName = addMiddBox.Text.Trim();
            Boolean correct_flag = await is_Corr_Str(login, password, mode);       
            if (correct_flag)
            {
                String querry = "INSERT INTO `users` (`login`, `password`, `mode_id`, `name`, `surname`, `middle_name`) VALUES (@login, @pass, @mode, @name, @surname, @middName)";
                var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@login", (MySqlDbType.VarChar, login)},
                    { "@pass", (MySqlDbType.VarChar, password)},
                    { "@mode", (MySqlDbType.Int32, mode)},
                    { "@name", (MySqlDbType.VarChar, name)},
                    { "@surname", (MySqlDbType.VarChar, surname)},
                    { "@middName", (MySqlDbType.VarChar, middName)}
                };
                await DB.QuerySIDAsync(querry, parameters);
                addLogBox.Clear();
                addMiddBox.Clear();
                addModeBox.SelectedIndex = -1;
                addNameBox.Clear();
                addPassBox.Clear();
                addSurBox.Clear();
                await DB.AddLogAsync(new String[] { "Пользователи: добавлен", login, name, surname, "от", authUser.Login, "id =", authUser.Id.ToString() });
            }
        }
        private async void Button_Delete_User_Click(object sender, RoutedEventArgs e)
        {
            String login = deleteBox.Text.Trim();
            Boolean correct_flag;
            if (Crypt.Check_Correct(login))
            {
                deleteBox.ToolTip = "Некорректные данные";
                deleteBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                deleteBox.ToolTip = "";
                deleteBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            if(login.Equals(authUser.Login))
            {
                correct_flag=false;
                Win_Meaasge_Box.MsgB("Нужно завершить текущую сессию, чтобы удалить эту учетную запись");
            }
            DataTable table = await DB.QuerySIDAsync("SELECT `login` FROM `users`");
            for(int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().Equals(login)) 
                { 
                    correct_flag = true;
                    break;
                }
                else correct_flag = false;

            }
            if (!correct_flag)
               Win_Meaasge_Box.MsgB("Учетной записи " + login + " не найдено");
            else
            {
                String querry = "DELETE FROM `users` WHERE `login` = @login";
                var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@login", (MySqlDbType.VarChar, login)}
                };
                await DB.QuerySIDAsync(querry, parameters);                
                await DB.AddLogAsync(new String[] { "Пользователи: удален", login, "от", authUser.Login, "id =", authUser.Id.ToString() }); ;
                deleteBox.Clear();
            }
        }
        private async void Button_Edit_User_Click(object sender, RoutedEventArgs e)
        {
            String login = editLogUserBox.Text.Trim();
            Boolean correct_flag;
            if (Crypt.Check_Correct(login))
            {
                editLogUserBox.ToolTip = "Некорректные данные";
                editLogUserBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                editLogUserBox.ToolTip = "";
                editLogUserBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            if (radioLogin.IsChecked == false && radioMidName.IsChecked == false && radioMode.IsChecked == false && radioName.IsChecked == false && radioPass.IsChecked == false && radioSurname.IsChecked == false)
            {
                Win_Meaasge_Box.MsgB("Не выбран вариант для редактирования");
            }
            DataTable table = await DB.QuerySIDAsync("SELECT `login` FROM `users`");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().Equals(login))
                {
                    correct_flag = true;
                    break;
                }
                else correct_flag = false;
            }
            if (!correct_flag)
                Win_Meaasge_Box.MsgB("Учетной записи " + login + " не найдено");
            else
            {
                if (radioMidName.IsChecked == true)
                {
                    String editMidName = editMidNameBox.Text.Trim();
                    if (Crypt.Check_Correct(editMidName))
                    {
                        editMidNameBox.ToolTip = "Некорректные данные";
                        editMidNameBox.Background = Brushes.DarkRed;
                        correct_flag = false;
                    }
                    else
                    {
                        editMidNameBox.ToolTip = "";
                        editMidNameBox.Background = Brushes.Transparent;
                        correct_flag = true;
                    }
                    if (correct_flag)
                    {
                        String querry = "UPDATE `users` SET `middle_name` = @middle_name WHERE `login` = @login";
                        var parameters = new Dictionary<String, (MySqlDbType, Object)>
                        {
                            { "@login", (MySqlDbType.VarChar, login)},
                            { "@middle_name", (MySqlDbType.VarChar, editMidName)}
                        };
                        await DB.QuerySIDAsync(querry, parameters);                        
                        await DB.AddLogAsync(new String[] { "Пользователи: изменено отчество для", login, "на", editMidName, "от", authUser.Login, "id =", authUser.Id.ToString() });
                        if (login.Equals(authUser.Login)) authUser.MidName = editMidName;
                        editMidNameBox.Clear();
                    }
                }
                if (radioName.IsChecked == true)
                {
                    String editName = editNameBox.Text.Trim();
                    if (Crypt.Check_Correct(editName))
                    {
                        editNameBox.ToolTip = "Некорректные данные";
                        editNameBox.Background = Brushes.DarkRed;
                        correct_flag = false;
                    }
                    else
                    {
                        editNameBox.ToolTip = "";
                        editNameBox.Background = Brushes.Transparent;
                        correct_flag = true;
                    }
                    if (correct_flag)
                    {
                        String querry = "UPDATE `users` SET `name` = @name WHERE `login` = @login";
                        var parameters = new Dictionary<String, (MySqlDbType, Object)>
                        {
                            { "@login", (MySqlDbType.VarChar, login)},
                            { "@name", (MySqlDbType.VarChar, editName)}
                        };
                        await DB.QuerySIDAsync(querry, parameters);                        
                        await DB.AddLogAsync(new String[] { "Пользователи: изменено имя для", login, "на", editName, "от", authUser.Login, "id =", authUser.Id.ToString() });
                        if (login.Equals(authUser.Login)) authUser.Name = editName;
                        editNameBox.Clear();
                    }
                }
                if (radioSurname.IsChecked == true)
                {
                    String editSurname = editFamilyBox.Text.Trim();
                    if (Crypt.Check_Correct(editSurname))
                    {
                        editFamilyBox.ToolTip = "Некорректные данные";
                        editFamilyBox.Background = Brushes.DarkRed;
                        correct_flag = false;
                    }
                    else
                    {
                        editFamilyBox.ToolTip = "";
                        editFamilyBox.Background = Brushes.Transparent;
                        correct_flag = true;
                    }
                    if (correct_flag)
                    {
                        String querry = "UPDATE `users` SET `surname` = @surname WHERE `login` = @login";
                        var parameters = new Dictionary<String, (MySqlDbType, Object)>
                        {
                            { "@login", (MySqlDbType.VarChar, login)},
                            { "@surname", (MySqlDbType.VarChar, editSurname)}
                        };
                        await DB.QuerySIDAsync(querry, parameters);                        
                        await DB.AddLogAsync(new String[] { "Пользователи: изменена фамилия для", login, "на", editSurname, "от", authUser.Login, "id =", authUser.Id.ToString() });
                        if (login.Equals(authUser.Login)) authUser.SurName = editSurname;
                        editFamilyBox.Clear();
                    }
                }
                if (radioMode.IsChecked == true)
                {
                    Int32 mode = editModeBox.SelectedIndex + 1;
                    if (mode < 1 || mode > 2)
                    {
                        editModeBox.ToolTip = "Некорректные данные";
                        editModeBox.Background = Brushes.DarkRed;
                        correct_flag = false;
                    }
                    else
                    {
                        editModeBox.ToolTip = "";
                        editModeBox.Background = Brushes.Transparent;
                        correct_flag = true;
                    }
                    if (correct_flag)
                    {
                        String querry = "UPDATE `users` SET `mode_id` = @mode WHERE `login` = @login";
                        var parameters = new Dictionary<String, (MySqlDbType, Object)>
                        {
                            { "@login", (MySqlDbType.VarChar, login)},
                            { "@mode", (MySqlDbType.Int32, mode)}
                        };
                        await DB.QuerySIDAsync(querry, parameters);                        
                        await DB.AddLogAsync(new String[] { "Пользователи: изменен режим доступа для", login, "на", mode.ToString(), "от", authUser.Login, "id =", authUser.Id.ToString() });
                        if (login.Equals(authUser.Login)) authUser.Mode = mode;
                        editModeBox.SelectedIndex = -1;
                    }
                }
                if (radioPass.IsChecked == true)
                {
                    String editPass =Crypt.Base64Encrypt(editPassBox.Text.Trim());
                    if (Crypt.Check_Correct(Crypt.Base64Decrypt(editPass)))
                    {
                        editPassBox.ToolTip = "Некорректные данные";
                        editPassBox.Background = Brushes.DarkRed;
                        correct_flag = false;
                    }
                    else
                    {
                        editPassBox.ToolTip = "";
                        editPassBox.Background = Brushes.Transparent;
                        correct_flag = true;
                    }
                    if (correct_flag)
                    {
                        String querry = "UPDATE `users` SET `password` = @password WHERE `login` = @login";
                        var parameters = new Dictionary<String, (MySqlDbType, Object)>
                        {
                            { "@login", (MySqlDbType.VarChar, login)},
                            { "@password", (MySqlDbType.VarChar, editPass)}
                        };
                        await DB.QuerySIDAsync(querry, parameters);                        
                        await DB.AddLogAsync(new String[] { "Пользователи: изменен пароль для", login, "от", authUser.Login, "id =", authUser.Id.ToString() });
                        editPassBox.Clear();
                    }
                }
                if (radioLogin.IsChecked == true)
                {
                    String editLogin = editLogBox.Text.Trim();
                    if (Crypt.Check_Correct(editLogin))
                    {
                        editLogBox.ToolTip = "Некорректные данные";
                        editLogBox.Background = Brushes.DarkRed;
                        correct_flag = false;
                    }
                    else
                    {
                        editLogBox.ToolTip = "";
                        editLogBox.Background = Brushes.Transparent;
                        correct_flag = true;
                    }
                    if (correct_flag)
                    {
                        String querry = "UPDATE `users` SET `login` = @editLogin WHERE `login` = @login";
                        var parameters = new Dictionary<String, (MySqlDbType, Object)>
                        {
                            { "@login", (MySqlDbType.VarChar, login)},
                            { "@editlogin", (MySqlDbType.VarChar, editLogin)}
                        };
                        await DB.QuerySIDAsync(querry, parameters);                        
                        await DB.AddLogAsync(new String[] { "Пользователи: изменен login для", login, "на", editLogin, "от", authUser.Login, "id =", authUser.Id.ToString() });
                        if (login.Equals(authUser.Login)) authUser.Login = editLogin;
                        editLogBox.Clear();
                    }
                }                
            }
        }
        private async Task<Boolean> is_Corr_Str(String login, String password, Int32 mode)
        {
            Boolean correct_flag;
            if (Crypt.Check_Correct(login))
            {
                addLogBox.ToolTip = "Некорректные данные";
                addLogBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                addLogBox.ToolTip = "";
                addLogBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            if (Crypt.Check_Correct(Crypt.Base64Decrypt(password)))
            {
                addPassBox.ToolTip = "Некорректные данные";
                addPassBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                addPassBox.ToolTip = "";
                addPassBox.Background = Brushes.Transparent;
                correct_flag = true;
            }
            if (mode < 1 || mode > 2)
            {
                addModeBox.ToolTip = "Некорректные данные";
                addModeBox.Background = Brushes.DarkRed;
                correct_flag = false;
            }
            else
            {
                addModeBox.ToolTip = "";
                addModeBox.Background = Brushes.Transparent;
                correct_flag = true;
            }            
            DataTable table = await DB.QuerySIDAsync("SELECT `login` FROM `users`");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().Equals(login))
                {
                    correct_flag = false;
                    Win_Meaasge_Box.MsgB("Учетная запись " + login + " уже существует");
                }
            }
            return correct_flag;
        }
    }
}

