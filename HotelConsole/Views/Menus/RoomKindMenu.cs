using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class RoomKindMenu : Menu
{
    public RoomKindMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Room kind menu[/]")
                .AddChoices("Add a room kind", "Edit a room kind", "Remove a room kind", "List all room kinds",
                    "Back to main menu"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Add a room kind" => new RoomKindCreator(),
            "Edit a room kind" => new RoomKindUpdater(),
            "Remove a room kind" => new RoomKindDeleter(),
            "List all room kinds" => new RoomKindReader(),
            _ => new MainMenu()
        };
    }
}