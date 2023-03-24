using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class VehicleUpdater : Updater
{
    public readonly Vehicle Vehicle;

    public VehicleUpdater()
    {
        UpdaterType = UpdaterType.Vehicle;
    }

    public VehicleUpdater(ICollection<Vehicle> vehicles) : this()
    {
        var targetVehicleRegistration = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which vehicle would you like to update?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other vehicles)[/]")
                .AddChoices(vehicles.Select(b => b.Registration)));

        Vehicle = vehicles.First(b => b.Registration == targetVehicleRegistration);

        while (true)
        {
            var targetField = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to update?")
                    .AddChoices("Registration", "Client ID", "Parking ID", "Back to Vehicles Menu"));

            if (targetField == "Back to Vehicles Menu") break;

            switch (targetField)
            {
                case "Name":
                    Vehicle.Registration =
                        AnsiConsole.Ask<string>(
                            $"Enter [yellow]{Vehicle.Registration}'s[/] new [darkorange]registration[/]:");
                    break;
                case "Client ID":
                    Vehicle.ClientId =
                        AnsiConsole.Ask<int>(
                            $"Enter [yellow]{Vehicle.Registration}'s[/] new [darkorange]client id[/]:");
                    break;
                case "Parking ID":
                    Vehicle.ParkingId =
                        AnsiConsole.Ask<int>(
                            $"Enter [yellow]{Vehicle.Registration}'s[/] new [darkorange]parking id[/]:");
                    break;
            }
        }
    }
}