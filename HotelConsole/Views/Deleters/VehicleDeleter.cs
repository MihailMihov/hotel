using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Deleters;

public class VehicleDeleter : Deleter
{
    public List<string> VehicleRegistations = new();

    public VehicleDeleter()
    {
        DeleterType = DeleterType.Vehicle;
    }

    public VehicleDeleter(ICollection<Vehicle> vehicles) : this()
    {
        var targetVehicleRegistrations = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which vehicle(s) would you like to delete?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other vehicles)[/]")
                .AddChoices(vehicles.Select(b => b.Registration)));

        if (AnsiConsole.Confirm($"Are you sure that you want to delete {targetVehicleRegistrations.Count} vehicles?"))
            VehicleRegistations = targetVehicleRegistrations;
    }
}