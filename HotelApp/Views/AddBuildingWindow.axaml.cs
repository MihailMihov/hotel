using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HotelApp.Views;

public partial class AddBuildingWindow : Window
{
    public AddBuildingWindow()
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
}