using System.Windows;
using EasyRooms.View;
using EasyRooms.ViewModel.Commands;

#nullable disable
namespace EasyRooms.ViewModel;

public class MainWindowViewModel : BindableBase
{
    public RelayCommand SwitchToOptionsCommand { get; set; }

    private readonly XpsUploadViewModel _xpsUploadViewModel;
    private readonly OptionsViewModel _optionsViewModel;
    private BindableBase _currentViewModel;
    private string _navigationButtonContent = "Optionen";

    public BindableBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public string NavigationButtonContent
    {
        get => _navigationButtonContent;
        set => SetProperty(ref _navigationButtonContent, value);
    }

    public MainWindowViewModel()
    {
        _xpsUploadViewModel = (XpsUploadViewModel) ((App) Application.Current).Services.GetService(typeof(XpsUploadViewModel));
        _optionsViewModel = (OptionsViewModel) ((App) Application.Current).Services.GetService(typeof(OptionsViewModel));
        SwitchToOptionsCommand = new RelayCommand(SwitchToOptions);
        CurrentViewModel = _xpsUploadViewModel;
    }

    private void SwitchToOptions()
    {
        CurrentViewModel = CurrentViewModel == _xpsUploadViewModel
            ? _optionsViewModel
            : _xpsUploadViewModel;
        NavigationButtonContent = CurrentViewModel == _xpsUploadViewModel
            ? "Optionen"
            : "Zurück";
    }
}