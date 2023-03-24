using System.Threading.Tasks;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using HotelApp.ViewModels;
using ReactiveUI;

namespace HotelApp.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task DoShowDialogAsync(InteractionContext<HotelViewModel, BuildingViewModel?> interaction)
    {
        var dialog = new MainWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<BuildingViewModel?>(this);
        interaction.SetOutput(result);
    }
}