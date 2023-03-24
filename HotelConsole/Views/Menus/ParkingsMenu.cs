using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class ParkingsMenu : Menu
{
    public ParkingsMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Parking menu[/]")
                .AddChoices("Add a parking", "Edit a parking", "Remove a parking", "List all parkings",
                    "Back to main menu"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Add a parking" => new ParkingCreator(),
            "Edit a parking" => new ParkingUpdater(),
            "Remove a parking" => new ParkingDeleter(),
            "List all parkings" => new ParkingReader(),
            _ => new MainMenu()
        };
    }
}