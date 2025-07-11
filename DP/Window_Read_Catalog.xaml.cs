using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KP_Mitsura
{
    public partial class Window_Read_Catalog : Window
    {
        private String get_Message;
        public String Get_Message 
        {
            get { return get_Message; } 
            set { get_Message = value; } 
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
            public String docType;
            public String shortType;
            public Int32 docVersion;
            public String docNotice;
            public DateTime dateNotice;
        }
        List<FileSystemItem> fileSystemItems = new List<FileSystemItem>();
        List<Int32> indexFileSystemItemInListBox = new List<Int32>();
        List<DictionaryItem> dictionaryItems = new List<DictionaryItem>();
        public Window_Read_Catalog()
        {
            InitializeComponent();
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Update_Info()
        {
            Load_Tree();
            Load_Dictionary();
            Get_Message = null;
        }
        private void treeView_Initialized(object sender, EventArgs e)
        {
            Update_Info();
        }
        private async void Load_Tree()
        {
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
                    catch(Exception ex)
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

        private async void MenuItem_Message_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = (TreeViewItem)treeView.SelectedItem;
            FileSystemItem item = await GetFileSystemItemById((Int32)selectedItem.Tag);
            if (item.isFile)
            {
                Window_User_Message window = new Window_User_Message();
                window.Owner = this;
                window.ShowDialog();
                String message = Get_Message;
                String querry = "INSERT INTO `user_message` (`user_id`, `user_login`, `message`, `status_id`, `file_name`, `file_path`) VALUES (@user_id, @user_login, @message, @status_id, @file_name, @file_path)";
                var parameters = new Dictionary<String, (MySqlDbType, Object)>
                {
                    { "@user_id", (MySqlDbType.Int32, authUser.Id)},
                    { "@user_login", (MySqlDbType.VarChar, authUser.Login)},
                    { "@message", (MySqlDbType.VarChar, message)},
                    { "@status_id", (MySqlDbType.Int32, 1)},
                    { "@file_name", (MySqlDbType.VarChar, item.name)},
                    { "@file_path", (MySqlDbType.VarChar, item.path)}
                };
                await DB.QuerySIDAsync(querry, parameters);              
                await DB.AddLogAsync(new String[] { "Обратная связь: добавлено сообщение", message, "для", item.name,  "от", authUser.Login, "id =", authUser.Id.ToString() });
                Win_Meaasge_Box.MsgB("Сообщение отправлено");
            }
            else Win_Meaasge_Box.MsgB("Необходимо выбрать файл");
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
        private async void Load_Dictionary()
        {
            dictionaryItems.Clear();
            DataTable table = await DB.QuerySIDAsync("SELECT t1.id, t1.id_catalog_info, t1.name_part_1, t1.name_part_2, t2.type, t2.short_type, t1.doc_version, t1.doc_notice, t1.date_notice FROM dictionary_files t1 JOIN doc_type t2 ON t1.doc_type = t2.id"); 
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DictionaryItem item = new DictionaryItem();
                item.id = (Int32)table.Rows[i]["id"];
                item.idCatalogInfo = (Int32)table.Rows[i]["id_catalog_info"];
                item.namePart1 = (String)table.Rows[i]["name_part_1"];
                item.namePart2 = (String)table.Rows[i]["name_part_2"];
                item.docType = (String)table.Rows[i]["type"];
                item.shortType = (String)table.Rows[i]["short_type"];
                item.docVersion = (Int32)table.Rows[i]["doc_version"];
                item.docNotice = (String)table.Rows[i]["doc_notice"];
                item.dateNotice = (DateTime)table.Rows[i]["date_notice"];
                dictionaryItems.Add(item);
            }
        }
        private void Dictionary_Info_Show(Int32 indexId)
        {
            showDictionaryBox.Items.Clear();
            Int32 index = -1;
            for (int i = 0; i < dictionaryItems.Count; i++)
            {
                if (dictionaryItems[i].idCatalogInfo == indexId)
                {
                    index = i; break;
                }
            }
            if (index != -1)
            {
                showDictionaryBox.Items.Add(dictionaryItems[index].namePart1 + " " + dictionaryItems[index].shortType);
                showDictionaryBox.Items.Add(dictionaryItems[index].namePart2);
                showDictionaryBox.Items.Add(dictionaryItems[index].docType);
                showDictionaryBox.Items.Add("Версия документа: " + dictionaryItems[index].docVersion);
                showDictionaryBox.Items.Add("Обоснование: " + dictionaryItems[index].docNotice);
                showDictionaryBox.Items.Add("Дата внедрения: " + dictionaryItems[index].dateNotice.ToShortDateString());
            }
        }
    }
}
