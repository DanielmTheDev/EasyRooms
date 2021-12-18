using System;
using System.Windows;
using EasyRooms.Model.DayPlan;
using EasyRooms.Model.FileDialog;
using EasyRooms.Model.Occupations;
using EasyRooms.Model.Pdf;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rows;
using EasyRooms.Model.Therapies;
using EasyRooms.Model.Therapies.NormalTherapies;
using EasyRooms.Model.Therapies.PartnerTherapies;
using EasyRooms.Model.Therapies.Preparations;
using EasyRooms.Model.Therapies.RoomSpecificTherapies;
using EasyRooms.Model.Validation;
using EasyRooms.Model.XpsExtracting;
using EasyRooms.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyRooms.View;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public IServiceProvider Services => _host.Services;
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            ConfigureServices(services)).Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IXpsWordsExtractor, XpsWordsExtractor>()
            .AddTransient<IRowsCreator, RowsCreator>()
            .AddTransient<IDayPlanParser, DayPlanParser>()
            .AddTransient<IRoomOccupationsFiller, RoomOccupationsFiller>()
            .AddTransient<IFileDialogOpener, FileDialogOpener>()
            .AddTransient<IFreeRoomFinder, FreeRoomFinder>()
            .AddTransient<ITherapyFiller, TherapyFiller>()
            .AddTransient<IRoomListCreator, RoomListCreator>()
            .AddTransient<IRoomsValidator, RoomsValidator>()
            .AddTransient<IPdfWriter, PdfWriter>()
            .AddTransient<ITherapyPlanCreator, TherapyPlanCreator>()
            .AddTransient<ITherapyPlanRowsPrinter, TherapyPlanRowsPrinter>()
            .AddTransient<ITherapyPlanHeadersPrinter, TherapyPlanHeadersPrinter>()
            .AddTransient<IOccupationsAdder, OccupationsAdder>()
            .AddTransient<IMassagesAdder, PreparationsAdder>()
            .AddTransient<IMassagesAdder, PartnerTherapiesAdder>()
            .AddTransient<IMassagesAdder, RoomSpecificMassagesAdder>()
            .AddTransient<IMassagesAdder, NormalTherapiesAdder>()
            .AddSingleton<XpsUploadView>()
            .AddSingleton<XpsUploadViewModel>()
            .AddSingleton<TestViewModel>()
            .AddSingleton<MainWindow>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
        }

        base.OnExit(e);
    }
}