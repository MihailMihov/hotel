using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class VehicleMenu : Menu
{
    public VehicleMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Vehicle menu[/]")
                .AddChoices("Add a vehicle", "Edit a vehicle", "Remove a vehicle", "List all vehicles",
                    "Back to main menu"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Add a vehicle" => new VehicleCreator(),
            "Edit a vehicle" => new VehicleUpdater(),
            "Remove a vehicle" => new VehicleDeleter(),
            "List all vehicles" => new VehicleReader(),
            _ => new MainMenu()
        };
    }
}