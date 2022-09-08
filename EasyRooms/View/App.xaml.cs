using System.IO;
using System.Windows;
using EasyRooms.Model.BulkCalculation;
using EasyRooms.Model.DayPlan.Implementations;
using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.Dialogs.Implementations;
using EasyRooms.Model.Dialogs.Interfaces;
using EasyRooms.Model.FileDialog.Implementations;
using EasyRooms.Model.FileDialog.Interfaces;
using EasyRooms.Model.Occupations.Implementations;
using EasyRooms.Model.Pdf.Implementations;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Persistence.Implementations;
using EasyRooms.Model.Persistence.Interfaces;
using EasyRooms.Model.Rooms.Implementations;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.Rows.Implementations;
using EasyRooms.Model.Rows.Interfaces;
using EasyRooms.Model.Therapies.Implementations;
using EasyRooms.Model.XpsExtracting.Implementations;
using EasyRooms.Model.XpsExtracting.Interfaces;
using EasyRooms.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#pragma warning disable CS8618

namespace EasyRooms.View;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public IServiceProvider Services => _host.Services;
    public IConfiguration Configuration { get; private set; }
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            ConfigureServices(services)).Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services
            .AddTransient<IXpsWordsExtractor, XpsWordsExtractor>()
            .AddTransient<IRowsCreator, RowsCreator>()
            .AddTransient<IDayPlanParser, DayPlanParser>()
            .AddTransient<IRoomOccupationsFiller, RoomOccupationsFiller>()
            .AddTransient<IFileDialogOpener, FileDialogOpener>()
            .AddTransient<IFreeRoomFinder, FreeRoomFinder>()
            .AddTransient<ITherapyFiller, TherapyFiller>()
            .AddTransient<IRoomListCreator, RoomListCreator>()
            .AddTransient<IPdfWriter, PdfWriter>()
            .AddTransient<IPlansCreator, PlansCreator>()
            .AddTransient<IPdfCreator, PdfCreator>()
            .AddTransient<IHeaderPrinter, HeaderPrinter>()
            .AddTransient<IOccupationsAdder, OccupationsAdder>()
            .AddTransient<ITherapiesAdder, PreparationsAdder>()
            .AddTransient<ITherapiesAdder, NoRoomTherapiesAdder>()
            .AddTransient<ITherapiesAdder, PartnerTherapiesAdder>()
            .AddTransient<ITherapiesAdder, CommentTherapiesAdder>()
            .AddTransient<ITherapiesAdder, RoomSpecificTherapiesAdder>()
            .AddTransient<ITherapiesAdder, LongTherapiesAdder>()
            .AddTransient<ITherapiesAdder, AdjacentTherapiesAdder>()
            .AddTransient<IFilledRoomsProvider, FilledRoomsProvider>()
            .AddTransient<IAdjacentTherapiesExtractor, AdjacentTherapiesExtractor>()
            .AddTransient<IPlainListWriter, PlainListWriter>()
            .AddSingleton<IPersistenceService, PersistenceService>()
            .AddSingleton<IMessageBoxShower, MessageBoxShower>()
            .AddSingleton<IBulkCalculator, BulkCalculator>()
            .AddSingleton<XpsUploadView>()
            .AddSingleton<XpsUploadViewModel>()
            .AddSingleton<OptionsViewModel>()
            .AddSingleton<BulkTestViewModel>()
            .AddSingleton<BulkTestView>()
            .AddSingleton<MainWindow>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();

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