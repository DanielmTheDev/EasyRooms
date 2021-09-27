namespace EasyRooms.ViewModel;

public class MainWindowViewModel : BindableBase
{
    private readonly XpsUploadViewModel _xpsUploadViewModel;
    private readonly TestViewModel _testViewModel;

    private BindableBase _currentViewModel;

    public BindableBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public MainWindowViewModel()
    {
        _xpsUploadViewModel = (XpsUploadViewModel)((App)System.Windows.Application.Current).Services.GetService(typeof(XpsUploadViewModel));
        _testViewModel = (TestViewModel)((App)System.Windows.Application.Current).Services.GetService(typeof(TestViewModel));
        CurrentViewModel = _xpsUploadViewModel;
    }
}
