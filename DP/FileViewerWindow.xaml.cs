using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KP_Mitsura
{
    public partial class FileViewerWindow : Window
    {
        public FileViewerWindow()
        {
            InitializeComponent();
        }
        public void LoadFile(string filePath)
        {
            if (filePath.ToLower().EndsWith(".pdf"))
            {
                PdfViewerControl.Visibility = Visibility.Visible;
                ImageViewer.Visibility = Visibility.Collapsed;

                PdfViewerControl.Navigate(new Uri(filePath));
            }
            else if (filePath.ToLower().EndsWith(".jpg") || filePath.ToLower().EndsWith(".jpeg") || filePath.ToLower().EndsWith(".png"))
            {
                ImageViewer.Visibility = Visibility.Visible;
                PdfViewerControl.Visibility = Visibility.Collapsed;
                ImageViewer.Source = new BitmapImage(new Uri(filePath));
            }
        }
    }
}
