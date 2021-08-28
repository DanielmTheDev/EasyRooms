using EasyRooms.ViewModel;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EasyRooms.View
{
    /// <summary>
    /// Interaction logic for XpsUploadView.xaml
    /// </summary>
    public partial class XpsUploadView : UserControl
    {
        public XpsUploadView()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".xps",
                Filter = "XPS Files (*.xps)|*.xps"
            };

            var result = dialog.ShowDialog();

            if (result == true)
            {
                var filename = dialog.FileName;
            }
        }
    }
}
