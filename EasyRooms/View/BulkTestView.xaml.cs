using System.Windows;
using EasyRooms.ViewModel;

namespace EasyRooms.View;

public partial class BulkTestView
{
    public BulkTestView()
    {
        InitializeComponent();
        DataContext = ((App) Application.Current).Services.GetService(typeof(BulkTestViewModel));
    }
}