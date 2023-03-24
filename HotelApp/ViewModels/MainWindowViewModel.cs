using System.Windows.Input;
using ReactiveUI;

namespace HotelApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        ShowDialog = new Interaction<HotelViewModel, BuildingViewModel?>();

        AddBuildingCommand = ReactiveCommand.Create(() =>
        {
            var hotel = new HotelViewModel();

            var result = ShowDialog.Handle(hotel);
        });
    }

    public ICommand AddBuildingCommand { get; }

    public Interaction<HotelViewModel, BuildingViewModel?> ShowDialog { get; }
}