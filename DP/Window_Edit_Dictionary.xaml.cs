using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KP_Mitsura
{
    public partial class Window_Edit_Dictionary : Window
    {
        DataTable tableD = new DataTable(); 
        struct dictionaryId
        {
            public int id;
            public int idCatalogInfo;
        }
        public Window_Edit_Dictionary()
        {
            InitializeComponent();
            Load_Table();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void Load_Table()
        {
            tableD.Rows.Clear();
            tableD.Columns.Clear();
            gridTable.ItemsSource = null;
            gridTable.Items.Clear();
            String querry = "SELECT df.name_part_1, df.name_part_2, dt.type, dt.short_type, df.doc_version, df.doc_notice, df.date_notice FROM dictionary_files df JOIN doc_type dt ON df.doc_type = dt.id;";
            tableD = await DB.QuerySIDAsync(querry);
            tableD.Columns[0].ColumnName = "Обозначение";
            tableD.Columns[1].ColumnName = "Наименование";
            tableD.Columns[2].ColumnName = "Тип документа";
            tableD.Columns[3].ColumnName = "Краткое обозначение";
            tableD.Columns[4].ColumnName = "Версия документа";
            tableD.Columns[5].ColumnName = "Обоснование";
            tableD.Columns[6].ColumnName = "Дата внедрения";
            gridTable.ItemsSource = tableD.DefaultView;
        }
        private void gridTable_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private async void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            Int32 index = gridTable.SelectedIndex;
            if (index > -1)
            {
                String querry = "DELETE FROM `dictionary_files` WHERE `id` = @id";
                var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@id", (MySqlDbType.Int32, tableD.Rows[index][0])}
                };
                await DB.QuerySIDAsync(querry, parameters);               
                await DB.AddLogAsync(new String[] { "Каталог: удалены подробности для ", tableD.Rows[index][2].ToString(), "от", authUser.Login, "id =", authUser.Id.ToString() });
                Load_Table();
            }
            else Win_Meaasge_Box.MsgB("Сначала нужно выбрать сообщение");
        }
        private async void Button_Cleaning_Click(object sender, RoutedEventArgs e)
        {            
            List<dictionaryId> dictionaryIdItems = new List<dictionaryId>();
            List<Int32> fileID = new List<Int32>();
            DataTable table = await DB.QuerySIDAsync("SELECT `id`, `id_catalog_info ` FROM `dictionary_files`");            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dictionaryId item = new dictionaryId();
                item.id = (Int32)table.Rows[i]["id"];
                item.idCatalogInfo = (Int32)table.Rows[i]["id_catalog_info"];
                dictionaryIdItems.Add(item);
            }
            table = await DB.QuerySIDAsync("SELECT `id` FROM `catalog_info` WHERE `is_file` = 1");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                fileID.Add((Int32)table.Rows[i]["id"]);
            }
            Int32 counter = 0;
            for (int i = 0; i < dictionaryIdItems.Count; i++)
            {
                Boolean flag = false;
                for (int j = 0; j < fileID.Count; j++)
                {
                    if (dictionaryIdItems[i].idCatalogInfo == fileID[j])
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    String querry = "DELETE FROM `dictionary_files` WHERE `id` = @id";
                    var parameters = new Dictionary<String, (MySqlDbType, Object)>
                    {
                        { "@id", (MySqlDbType.Int32, dictionaryIdItems[i].id)}
                    };
                    await DB.QuerySIDAsync(querry, parameters);
                    counter++;                    
                }
                if (counter > 0)
                {
                    await DB.AddLogAsync(new String[] { "Подробности: удалено ", counter.ToString(), " неактуальных записи от ", authUser.Login, "id = ", authUser.Id.ToString() });
                }
            }
        }
        private void gridTable_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
            }
        }
        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            string filterText = SearchBox.Text;
            if (!string.IsNullOrEmpty(filterText))
            {
                tableD.DefaultView.RowFilter = string.Format(
                    "([Обозначение] LIKE '%{0}%' OR " +
                    "[Наименование] LIKE '%{0}%' OR " +
                    "[Тип документа] LIKE '%{0}%' OR " +
                    "[Краткое обозначение] LIKE '%{0}%' OR " +
                    "Convert([Версия документа], 'System.String') LIKE '%{0}%' OR " +
                    "[Обоснование] LIKE '%{0}%' OR " +
                    "Convert([Дата внедрения], 'System.String') LIKE '%{0}%')", filterText);
            }
            else
            {
                tableD.DefaultView.RowFilter = string.Empty;
            }
        }
    }
}
