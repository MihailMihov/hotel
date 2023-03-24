using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Deleters;

public class ParkingDeleter : Deleter
{
    public List<int> ParkingIds = new();

    public ParkingDeleter()
    {
        DeleterType = DeleterType.Parking;
    }

    public ParkingDeleter(ICollection<Parking> parkings) : this()
    {
        var targetParkingNames = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which parking(s) would you like to delete?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other parkings)[/]")
                .AddChoices(parkings.Select(b => b.Name)));

        if (!AnsiConsole.Confirm($"Are you sure that you want to delete {targetParkingNames.Count} parkings?")) return;

        foreach (var parkingName in targetParkingNames) ParkingIds.Add(parkings.First(b => b.Name == parkingName).Id);
    }
}