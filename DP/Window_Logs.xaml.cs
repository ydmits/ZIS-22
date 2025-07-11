using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KP_Mitsura
{
    public partial class Window_Logs : Window
    {
        public Window_Logs()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {
            DataTable table = await DB.QuerySIDAsync("SELECT date, request FROM `logs`");
            table.Columns[0].ColumnName = "Дата";
            table.Columns[1].ColumnName = "Запрос";
            var groups = table.AsEnumerable()
                              .GroupBy(row => ((string)row["Запрос"]).Split(' ')[0])
                              .ToList();

            foreach (var group in groups)
            {
                DataGrid dataGrid = new DataGrid
                {
                    IsReadOnly = true,
                    AutoGenerateColumns = true
                };
                dataGrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

                TabItem tabItem = new TabItem
                {
                    Header = group.Key,
                    Content = dataGrid
                };
                dataGrid.ItemsSource = group.CopyToDataTable().DefaultView;
                tabControlLogs.Items.Add(tabItem);
            }
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Дата")
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
            }
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            string filterText = SearchBox.Text;
            foreach (TabItem tab in tabControlLogs.Items)
            {
                if (tab.Content is DataGrid dataGrid)
                {
                    DataTable dt = ((DataView)dataGrid.ItemsSource).Table;
                    DataView dv = new DataView(dt);
                    dv.RowFilter = GetRowFilter(filterText);
                    dataGrid.ItemsSource = dv;
                }
            }
        }
        private string GetRowFilter(string filterText)
        {
            if (string.IsNullOrEmpty(filterText))
                return "";
            return string.Format(
                "Convert([Дата], 'System.String') LIKE '%{0}%' OR Запрос LIKE '%{0}%'",
                filterText.Replace("'", "''")
            );
        }
    }
}
