#nullable disable
using System.Windows;
using EasyRooms.Model.Constants;
using EasyRooms.View;
using EasyRooms.ViewModel.Commands;

namespace EasyRooms.ViewModel;

public class MainWindowViewModel : BindableBase
{
    public RelayCommand SwitchToOptionsCommand { get; set; }
    public RelayCommand SwitchToBulkTestCommand { get; set; }

    private readonly XpsUploadViewModel _xpsUploadViewModel;
    private readonly OptionsViewModel _optionsViewModel;
    private readonly BulkTestViewModel _bulkTestViewModel;
    private BindableBase _currentViewModel;
    private string _navigationButtonContent = NavigationConstants.Options;

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

    public string BulkTestVisibility
        => bool.Parse(((App) Application.Current).Configuration.GetSection("Admin")["ShowTestScreen"])
            ? "Visible"
            : "Collapsed";


    public MainWindowViewModel()
    {
        _xpsUploadViewModel = (XpsUploadViewModel) ((App) Application.Current).Services.GetService(typeof(XpsUploadViewModel));
        _optionsViewModel = (OptionsViewModel) ((App) Application.Current).Services.GetService(typeof(OptionsViewModel));
        _bulkTestViewModel = (BulkTestViewModel) ((App) Application.Current).Services.GetService(typeof(BulkTestViewModel));
        SwitchToOptionsCommand = new(SwitchToOptions);
        SwitchToBulkTestCommand = new(SwitchToBulkTest);
        CurrentViewModel = _xpsUploadViewModel;
    }

    private void SwitchToOptions()
    {
        CurrentViewModel = CurrentViewModel == _xpsUploadViewModel
            ? _optionsViewModel
            : _xpsUploadViewModel;
        NavigationButtonContent = CurrentViewModel == _xpsUploadViewModel
            ? NavigationConstants.Options
            : NavigationConstants.Return;
    }

    private void SwitchToBulkTest()
        => CurrentViewModel = CurrentViewModel != _bulkTestViewModel
            ? _bulkTestViewModel
            : _xpsUploadViewModel;
}