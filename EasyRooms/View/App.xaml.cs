using System;
using System.Windows;
using EasyRooms.Model.DayPlan;
using EasyRooms.Model.DayPlan.Implementations;
using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.FileDialog;
using EasyRooms.Model.FileDialog.Implementations;
using EasyRooms.Model.FileDialog.Interfaces;
using EasyRooms.Model.Occupations;
using EasyRooms.Model.Occupations.Implementations;
using EasyRooms.Model.Occupations.Interfaces;
using EasyRooms.Model.Pdf;
using EasyRooms.Model.Pdf.Implementations;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Implementations;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.Rows;
using EasyRooms.Model.Rows.Implementations;
using EasyRooms.Model.Rows.Interfaces;
using EasyRooms.Model.Therapies;
using EasyRooms.Model.Therapies.Implementations;
using EasyRooms.Model.Therapies.Interfaces;
using EasyRooms.Model.Validation;
using EasyRooms.Model.Validation.Implementations;
using EasyRooms.Model.Validation.Interfaces;
using EasyRooms.Model.XpsExtracting;
using EasyRooms.Model.XpsExtracting.Implementations;
using EasyRooms.Model.XpsExtracting.Interfaces;
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