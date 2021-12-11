using System.Windows;
using EasyRooms.View;

#nullable disable
namespace EasyRooms.ViewModel;

public class MainWindowViewModel : BindableBase
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly XpsUploadViewModel _xpsUploadViewModel;
    // ReSharper disable once NotAccessedField.Local
    private readonly TestViewModel _testViewModel;

    private BindableBase _currentViewModel;

    public BindableBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public MainWindowViewModel()
    {
        _xpsUploadViewModel =
            (XpsUploadViewModel) ((App) Application.Current).Services.GetService(typeof(XpsUploadViewModel));
        _testViewModel = (TestViewModel) ((App) Application.Current).Services.GetService(typeof(TestViewModel));
        CurrentViewModel = _xpsUploadViewModel;
    }
}