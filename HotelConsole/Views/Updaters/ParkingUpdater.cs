using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class ParkingUpdater : Updater
{
    public readonly Parking Parking;

    public ParkingUpdater()
    {
        UpdaterType = UpdaterType.Parking;
    }

    public ParkingUpdater(ICollection<Parking> parkings) : this()
    {
        var targetParkingName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which parking would you like to update?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other parkings)[/]")
                .AddChoices(parkings.Select(b => b.Name)));

        Parking = parkings.First(b => b.Name == targetParkingName);

        while (true)
        {
            var targetField = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to update?")
                    .AddChoices("Name", "Capacity", "Back to Parkings Menu"));

            if (targetField == "Back to Parkings Menu") break;

            switch (targetField)
            {
                case "Name":
                    Parking.Name =
                        AnsiConsole.Ask<string>($"Enter [yellow]{Parking.Name}'s[/] new [darkorange]name[/]:");
                    break;
                case "Capacity":
                    Parking.Capacity =
                        AnsiConsole.Ask<int>($"Enter [yellow]{Parking.Name}'s[/] new [darkorange]capacity[/]:");
                    break;
            }
        }
    }
}