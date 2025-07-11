using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KP_Mitsura
{
    public partial class Window_Edit_Catalog : Window
    {
        private String get_Name;
        public String Get_Name
        {
            get { return get_Name; }
            set { get_Name = value; }
        }
        Int32 targetId;
        struct FileSystemItem
        {
            public Int32 id;
            public String name;
            public Int32 parentId;
            public Boolean isFile;
            public String path;
        }
        struct DictionaryItem
        {
            public Int32 id;
            public Int32 idCatalogInfo;
            public String namePart1;
            public String namePart2;
            public Int32? docType;
            public Int32? docVersion;
            public String docNotice;
            public DateTime dateNotice;
        }
        List<FileSystemItem> fileSystemItems = new List<FileSystemItem>();
        List<Int32> indexFileSystemItemInListBox = new List<Int32>();
        List<DictionaryItem> dictionaryItems = new List<DictionaryItem>();
        public Window_Edit_Catalog()
        {
            InitializeComponent();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Update_Info()
        {
            var expandedNodes = GetExpandedNodes();
            Load_Tree();
            Load_Dictionary();
            foreach (TreeViewItem item in treeView.Items)
            {
                RestoreExpandedNodes(item, expandedNodes);
            }
            if (Get_Name != null) Get_Name = null;
        }
        private void treeView_Initialized(object sender, EventArgs e)
        {
            Update_Info();
        }    
        private async void Load_Tree()
        {
            treeView.Items.Clear();
            List<FileSystemItem> fileSystemItems = await GetFileSystemItems();
            List<FileSystemItem> sortItems = fileSystemItems.OrderBy(x => x.isFile).ThenBy(x => x.id).ThenBy(x => x.parentId).ToList();
            foreach (FileSystemItem item in sortItems)
            {
                if (item.parentId == 0)
                {
                    TreeViewItem rootNode = new TreeViewItem();
                    rootNode.Header = item.name;
                    rootNode.Tag = item.id;
                    treeView.Items.Add(rootNode);
                    AddChildItems(rootNode, sortItems);
                }
            } 
        }
        private async Task<List<FileSystemItem>> GetFileSystemItems()
        {
            fileSystemItems.Clear();
            DataTable table = await DB.QuerySIDAsync("SELECT * FROM `catalog_info`");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                FileSystemItem item = new FileSystemItem();
                item.id = (Int32)table.Rows[i]["id"];
                item.name = (String)table.Rows[i]["name"];
                item.parentId = (Int32)table.Rows[i]["parent_id"];
                item.isFile = (Boolean)table.Rows[i]["is_file"];
                item.path = (String)table.Rows[i]["path"];
                fileSystemItems.Add(item);
            }
            return fileSystemItems;
        }
        private void AddChildItems(TreeViewItem parentItem, List<FileSystemItem> fileSystemItems)
        {
            Int32 parentId = (Int32)parentItem.Tag;
            foreach (FileSystemItem item in fileSystemItems)
            {
                if (item.parentId == parentId)
                {
                    try
                    {
                        TreeViewItem childNode = new TreeViewItem();
                        childNode.Header = item.name;
                        childNode.Tag = item.id;
                        if (item.isFile)
                        {
                            childNode.Header = new StackPanel
                            {
                                Orientation = System.Windows.Controls.Orientation.Horizontal,
                                Children = {
                        new System.Windows.Controls.Image
                        {
                            Source = new BitmapImage(new Uri("D:\\02 Study\\ZIS-22\\ДП\\KP_Mitsura\\icon1.png")),
                            Width = 16,
                            Height = 16
                        },
                        new TextBlock
                        {
                            Text = item.name
                        }
                    }
                            };
                        }
                        parentItem.Items.Add(childNode);
                        AddChildItems(childNode, fileSystemItems);
                    }
                    catch (Exception ex)
                    {
                        Win_Meaasge_Box.MsgB(ex.Message);
                    }
                }
            }
        }
        private async void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            if (selectedItem != null)
            {
                FileSystemItem item = await GetFileSystemItemById((Int32)selectedItem.Tag);
                if (item.isFile)
                {
                    if (item.path.ToLower().EndsWith(".pdf") || item.path.ToLower().EndsWith(".jpg") || item.path.ToLower().EndsWith(".jpeg") || item.path.ToLower().EndsWith(".png"))
                    {
                        FileViewerWindow viewer = new FileViewerWindow();
                        viewer.LoadFile(item.path);
                        viewer.Show();
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(item.path);
                    }
                    Int32 index = fileSystemItems.FindIndex(it => it.id == item.id);
                    targetId = fileSystemItems[index].id;
                    Dictionary_Info_Show(targetId);
                }
            }
        }
        private async Task<FileSystemItem> GetFileSystemItemById(Int32 itemId)
        {
            List<FileSystemItem> fileSystemItems = await GetFileSystemItems();
            FileSystemItem item = fileSystemItems.FirstOrDefault(x => x.id == itemId);
            return item;
        }
        private async void Menu_Add_Catalog_Click(object sender, RoutedEventArgs e)
        {
            Window_New_Catalog_Name window = new Window_New_Catalog_Name();
            window.Owner = this;
            window.ShowDialog();
            String filePath = "-";
            String fileName = Get_Name;
            Int32 mode = 0;
            Int32 parent_id = 0;
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            FileSystemItem item = new FileSystemItem();
            if (selectedItem != null )
            {
                item = await GetFileSystemItemById((Int32)selectedItem.Tag);
                parent_id = item.id;
            }
            if (await Check_Name(fileName, parent_id) && !Crypt.Check_Correct(fileName))
            {
                String querry = "INSERT INTO `catalog_info` (`name`, `parent_id`, `is_file`, `path`) VALUES (@name, @parent_id, @is_file, @path)";
                var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@name", (MySqlDbType.VarChar, fileName)},
                    { "@parent_id", (MySqlDbType.Int32, parent_id)},
                    { "@is_file", (MySqlDbType.Int32, mode)},
                    { "@path", (MySqlDbType.VarChar, filePath)}
                };
                await DB.QuerySIDAsync(querry, parameters);
                await DB.AddLogAsync(new String[] { "Каталог: новый раздел", fileName, "от", authUser.Login, "id =", authUser.Id.ToString() });
                Update_Info();
            }
            else if (fileName != null) Win_Meaasge_Box.MsgB("В данном разделе это имя уже занято или оно не было указано корректно");
        }
        private Dictionary<int, bool> GetExpandedNodes()
        {
            var expandedNodes = new Dictionary<int, bool>();
            foreach (TreeViewItem item in treeView.Items)
            {
                SaveExpandedNodes(item, expandedNodes);
            }
            return expandedNodes;
        }
        private void SaveExpandedNodes(TreeViewItem item, Dictionary<int, bool> expandedNodes)
        {
            if (item != null && item.Tag != null && item.IsExpanded)
            {
                expandedNodes[(int)item.Tag] = item.IsExpanded;
            }
            foreach (TreeViewItem childItem in item.Items)
            {
                SaveExpandedNodes(childItem, expandedNodes);
            }
        }
        private void RestoreExpandedNodes(TreeViewItem item, Dictionary<int, bool> expandedNodes)
        {
            if (item != null && item.Tag != null && expandedNodes.ContainsKey((int)item.Tag))
            {
                item.IsExpanded = expandedNodes[(int)item.Tag];
            }
            foreach (TreeViewItem childItem in item.Items)
            {
                RestoreExpandedNodes(childItem, expandedNodes);
            }
        }
        private async void Menu_Add_File_Click(object sender, RoutedEventArgs e)
        {
            String filePath = "";
            String fileName = "";
            Int32 mode = 1;
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            if (selectedItem == null)
            {
                Win_Meaasge_Box.MsgB("Необходимо выбрать каталог, в который вы хотите добавить файл");
            }
            else
            {
                FileSystemItem item = await GetFileSystemItemById((Int32)selectedItem.Tag);
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    filePath = openFileDialog.FileName;
                    fileName = openFileDialog.SafeFileName;
                }
                if ( !String.IsNullOrEmpty(filePath) && await Check_Name(fileName, item.isFile ? item.parentId : item.id) && Crypt.Check_Correct(fileName))
                {
                    String querry = "INSERT INTO `catalog_info` (`name`, `parent_id`, `is_file`, `path`) VALUES (@name, @parent_id, @is_file, @path)";
                    var parameters = new Dictionary<String, (MySqlDbType, Object)>
                    {
                        { "@name", (MySqlDbType.VarChar, fileName)},
                        { "@parent_id", (MySqlDbType.Int32, item.id)},
                        { "@is_file", (MySqlDbType.Int32, mode)},
                        { "@path", (MySqlDbType.VarChar, filePath)}
                    };
                    await DB.QuerySIDAsync(querry, parameters);                    
                    await DB.AddLogAsync(new String[] { "Каталог: новый файл", fileName, "в разделе", item.name ,"от", authUser.Login, "id =", authUser.Id.ToString() });
                    Update_Info();
                }
                else if (!fileName.Equals("")) Win_Meaasge_Box.MsgB("В данном разделе это имя уже занято");
            }
        }
        private async void Menu_Delete_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            FileSystemItem item = await GetFileSystemItemById((Int32)selectedItem.Tag);
            String querry;
            if (item.isFile)
            {
                querry = "DELETE FROM `catalog_info` WHERE `id` = @id";
                targetId = item.id;
                Button_Delete_Dictionary_Item_Click(sender, e);
            }
            else
            {
                querry = "DELETE FROM `catalog_info` WHERE `id` = @id OR `parent_id` = @id";               
            }
            var parameters = new Dictionary<String, (MySqlDbType, Object)>
            {
                { "@id", (MySqlDbType.Int32, item.id)}
            };
            await DB.QuerySIDAsync(querry, parameters);            
            await DB.AddLogAsync(new String[] { "Каталог: удален", item.name, "от", authUser.Login, "id =", authUser.Id.ToString() });
            Update_Info();
        }
        private async void treeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem clickedItem = VisualUpwardSearch(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (clickedItem != null)
            {
                clickedItem.IsSelected = true;
            }
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            if (selectedItem != null)
            {
                FileSystemItem item = await GetFileSystemItemById((Int32)selectedItem.Tag);
                if (item.isFile)
                {
                    Int32 index = fileSystemItems.FindIndex(it => it.id == item.id);
                    targetId = fileSystemItems[index].id;
                    Dictionary_Info_Show(targetId);
                }
            }
        }
        static DependencyObject VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(TreeViewItem))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }
        private async Task <Boolean> Check_Name(String name, Int32 parent_id)
        {
            List<FileSystemItem> fileSystemItems = await GetFileSystemItems();
            List<FileSystemItem> parentItems = new List<FileSystemItem>();
            foreach (FileSystemItem item in fileSystemItems)
            {
                if (item.parentId == parent_id)
                {
                    parentItems.Add(item);
                }
            }
            foreach (FileSystemItem item in parentItems)
            {
                if (item.name.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            listBox.Items.Clear();
            indexFileSystemItemInListBox.Clear();
            String str = searchBox.Text;
            foreach (FileSystemItem item in fileSystemItems)
            {
                if (item.isFile && !str.Equals(""))
                {
                    if (item.name.Contains(str))
                    {
                        listBox.Items.Add(item.name);
                        indexFileSystemItemInListBox.Add(fileSystemItems.IndexOf(item));
                    }
                }
            }
        }
        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Int32 indexListBox = listBox.SelectedIndex;
            Int32 indexId = indexFileSystemItemInListBox[indexListBox];
            FileSystemItem item = fileSystemItems[indexId];
            if (item.path.ToLower().EndsWith(".pdf") || item.path.ToLower().EndsWith(".jpg") || item.path.ToLower().EndsWith(".jpeg") || item.path.ToLower().EndsWith(".png"))
            {
                FileViewerWindow viewer = new FileViewerWindow();
                viewer.LoadFile(item.path);
                viewer.Show();
            }
            else
            {
                System.Diagnostics.Process.Start(item.path);
            }
            targetId = fileSystemItems[indexId].id;
            Dictionary_Info_Show(targetId);
        }
        private void listBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            {
                ListBoxItem clickedItem = VisualUpwardSearchListBox(e.OriginalSource as DependencyObject) as ListBoxItem;
                if (clickedItem != null)
                {
                    clickedItem.IsSelected = true;
                }
                Int32 indexListBox = listBox.SelectedIndex;
                Int32 indexId = indexFileSystemItemInListBox[indexListBox];
                targetId = fileSystemItems[indexId].id;
                Dictionary_Info_Show(targetId);
            }
        }
        static DependencyObject VisualUpwardSearchListBox(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(ListBoxItem))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }
        private void StackPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = FindVisualParent<ScrollViewer>(sender as UIElement);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
                e.Handled = true;
            }
        }
        private T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
        private async void Button_Save_Dictionary_Click(object sender, RoutedEventArgs e)
        {
            if (targetId > 0 && namePart1Box.Text.Length > 1 && namePart2Box.Text.Length > 1 && docTypeBox.SelectedIndex != -1 && docVersionBox.Text.Length > 0 && Convert.ToInt32(docVersionBox.Text) > -1 && docNoticeBox.Text.Length > 1 && dateNoticePicker.SelectedDate != null)
            {
                Boolean isUpdate = false;
                Int32 index = -1;
                Int32 fileIndex = -1;
                for (int i = 0; i < dictionaryItems.Count; i++)
                {
                    if (dictionaryItems[i].idCatalogInfo == targetId)
                    {
                        isUpdate = true;
                        index = i;
                        break;
                    }
                }
                for (int i = 0; i < fileSystemItems.Count; i++)
                {
                    if (fileSystemItems[i].id == targetId)
                    {
                        fileIndex = i;
                        break;
                    }
                }
                String querry;
                var parameters = new Dictionary<String, (MySqlDbType, Object)> { };
                Int32 docTypeIndex = docTypeBox.SelectedIndex + 1;
                if (isUpdate)
                {                   
                    querry = "UPDATE `dictionary_files` SET `name_part_1` = @name_part_1, `name_part_2` = @name_part_2, `doc_type` = @doc_type, `doc_version` = @doc_version, `doc_notice` = @doc_notice, `date_notice` = @date_notice WHERE `id` = @id";
                    parameters = new Dictionary<String, (MySqlDbType, Object)>
                    {
                        { "@id", (MySqlDbType.Int32, dictionaryItems[index].id)},
                        { "@name_part_1", (MySqlDbType.VarChar, namePart1Box.Text)},
                        { "@name_part_2", (MySqlDbType.VarChar, namePart2Box.Text)},
                        { "@doc_type", (MySqlDbType.Int32, docTypeIndex)},
                        { "@doc_version", (MySqlDbType.Int32, docVersionBox.Text)},
                        { "@doc_notice", (MySqlDbType.VarChar, docNoticeBox.Text)},
                        { "@date_notice", (MySqlDbType.DateTime, dateNoticePicker.SelectedDate)}
                    };
                }
                else
                {
                    querry = "INSERT INTO `dictionary_files` (`id_catalog_info`, `name_part_1`, `name_part_2`, `doc_type`, `doc_version`, `doc_notice`, `date_notice`) VALUES (@id_catalog_info, @name_part_1, @name_part_2, @doc_type, @doc_version, @doc_notice, @date_notice)";
                    parameters = new Dictionary<String, (MySqlDbType, Object)>
                    {
                        { "@id_catalog_info", (MySqlDbType.Int32, targetId)},
                        { "@name_part_1", (MySqlDbType.VarChar, namePart1Box.Text)},
                        { "@name_part_2", (MySqlDbType.VarChar, namePart2Box.Text)},
                        { "@doc_type", (MySqlDbType.Int32, docTypeIndex)},
                        { "@doc_version", (MySqlDbType.Int32, docVersionBox.Text)},
                        { "@doc_notice", (MySqlDbType.VarChar, docNoticeBox.Text)},
                        { "@date_notice", (MySqlDbType.DateTime, dateNoticePicker.SelectedDate)}
                    };
                }
                await DB.QuerySIDAsync(querry, parameters);                
                await DB.AddLogAsync(new String[] { "Каталог: обновление подробностей для ", fileSystemItems[fileIndex].name, "на", namePart1Box.Text, namePart2Box.Text, docVersionBox.Text, docNoticeBox.Text, dateNoticePicker.Text, "от", authUser.Login, "id =", authUser.Id.ToString() });
                Win_Meaasge_Box.MsgB("Запись сохранена");
                Update_Info();
            }
            else Win_Meaasge_Box.MsgB("Сначала нужно выбрать файл и корректно заполнить все элементы");
        }
        private async void Button_Delete_Dictionary_Item_Click(object sender, RoutedEventArgs e)
        {
            Int32 index = -1;
            Int32 fileIndex = -1;
            for (int i = 0; i < dictionaryItems.Count; i++)
            {
                if (dictionaryItems[i].idCatalogInfo == targetId)
                {
                    index = i;
                    break;
                }
            }
            for (int i = 0; i < fileSystemItems.Count; i++)
            {
                if (fileSystemItems[i].id == targetId)
                {
                    fileIndex = i;
                    break;
                }
            }
            if (index > -1)
            {
                String querry = "DELETE FROM `dictionary_files` WHERE `id` = @id";
                var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@id", (MySqlDbType.Int32, dictionaryItems[index].id)}
                };
                await DB.QuerySIDAsync(querry, parameters);                
                await DB.AddLogAsync(new String[] { "Каталог: удалены подробности для ", fileSystemItems[fileIndex].name, "от", authUser.Login, "id =", authUser.Id.ToString() });
                Win_Meaasge_Box.MsgB("Запись удалена");
                Update_Info();
                Dictionary_Clear();
            }
        }
        private async void Load_Dictionary()
        {
            dictionaryItems.Clear();
            DataTable table = await DB.QuerySIDAsync("SELECT * FROM `dictionary_files`");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DictionaryItem item = new DictionaryItem();
                item.id = (Int32)table.Rows[i]["id"];
                item.idCatalogInfo = (Int32)table.Rows[i]["id_catalog_info"];
                item.namePart1 = (String)table.Rows[i]["name_part_1"];
                item.namePart2 = (String)table.Rows[i]["name_part_2"];
                item.docType = (Int32)table.Rows[i]["doc_type"];
                item.docVersion = (Int32)table.Rows[i]["doc_version"];
                item.docNotice = (String)table.Rows[i]["doc_notice"];
                item.dateNotice = (DateTime)table.Rows[i]["date_notice"];
                dictionaryItems.Add(item);
            }
        }
        private void Dictionary_Info_Show(Int32 indexId)
        {
            Dictionary_Clear();
            Int32 index = -1;
            for(int i = 0; i < dictionaryItems.Count; i++)
            {
                if (dictionaryItems[i].idCatalogInfo == indexId)
                {
                    index = i; break;
                }
            }
            if (index != -1)
            {
                namePart1Box.Text = dictionaryItems[index].namePart1;
                namePart2Box.Text = dictionaryItems[index].namePart2;
                docTypeBox.SelectedIndex = (Int32)dictionaryItems[index].docType -1;
                docVersionBox.Text = dictionaryItems[index].docVersion.ToString();
                docNoticeBox.Text = dictionaryItems[index].docNotice;
                if (dictionaryItems[index].dateNotice != null) dateNoticePicker.SelectedDate = dictionaryItems[index].dateNotice;
            }
        }
        private void Dictionary_Clear()
        {
            namePart1Box.Text = String.Empty;
            namePart2Box.Text = String.Empty ;
            docTypeBox.SelectedIndex = -1;
            docVersionBox.Text = String.Empty;
            docNoticeBox.Text = String.Empty;
            dateNoticePicker.SelectedDate = null;
        }
        private async void docTypeBox_Initialized(object sender, EventArgs e)
        {
            DataTable table = await DB.QuerySIDAsync("SELECT `type` FROM `doc_type`");                        
            for (int i = 0; i < table.Rows.Count; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = table.Rows[i][0].ToString();
                docTypeBox.Items.Add(textBlock);
            }
        }
        private void docVersionBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }
        private void docVersionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
            if (String.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "0";
                textBox.CaretIndex = textBox.Text.Length;
            }
            else
            {
                Int32 value;
                Boolean isValid = Int32.TryParse(textBox.Text, out value);
                if (!isValid || value <= 0)
                {
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
        }
        private Boolean IsNumeric(String str)
        {
            Double value;
            return Double.TryParse(str, out value);
        }
        private void Button_Open_Dictionary_Click(object sender, RoutedEventArgs e)
        {
            Window_Edit_Dictionary window = new Window_Edit_Dictionary();
            window.ShowDialog();
            Update_Info();
        }

        private async void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            if (selectedItem != null)
            {
                FileSystemItem item = await GetFileSystemItemById((Int32)selectedItem.Tag);
                if (item.isFile)
                {
                    if (item.path.ToLower().EndsWith(".pdf") || item.path.ToLower().EndsWith(".jpg") ||
                        item.path.ToLower().EndsWith(".jpeg") || item.path.ToLower().EndsWith(".png"))
                    {
                        FileViewerWindow viewer = new FileViewerWindow();
                        viewer.LoadFile(item.path);
                        viewer.Show();
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(item.path);
                    }
                    Int32 index = fileSystemItems.FindIndex(it => it.id == item.id);
                    targetId = fileSystemItems[index].id;
                    Dictionary_Info_Show(targetId);
                }
                else
                {
                    if (selectedItem.HasItems)
                    {
                        selectedItem.IsExpanded = true;
                    }
                }
            }
        }

    }
}
