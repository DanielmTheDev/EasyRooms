using System.Windows.Controls;
using EasyRooms.ViewModel;

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
            DataContext = ((App)System.Windows.Application.Current).Services.GetService(typeof(XpsUploadViewModel));
        }
    }
}
