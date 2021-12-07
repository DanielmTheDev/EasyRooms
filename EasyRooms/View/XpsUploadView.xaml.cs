using System.Windows;
using EasyRooms.ViewModel;

namespace EasyRooms.View
{
    /// <summary>
    /// Interaction logic for XpsUploadView.xaml
    /// </summary>
    public partial class XpsUploadView
    {
        public XpsUploadView()
        {
            InitializeComponent();
            DataContext = ((App) Application.Current).Services.GetService(typeof(XpsUploadViewModel));
        }
    }
}