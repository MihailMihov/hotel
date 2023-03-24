using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class RoomsMenu : Menu
{
    public RoomsMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Room menu[/]")
                .AddChoices("Add a room", "Edit a room", "Remove a room", "List all rooms",
                    "Back to main menu"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Add a room" => new RoomCreator(),
            "Edit a room" => new RoomUpdater(),
            "Remove a room" => new RoomDeleter(),
            "List all rooms" => new RoomReader(),
            _ => new MainMenu()
        };
    }
}