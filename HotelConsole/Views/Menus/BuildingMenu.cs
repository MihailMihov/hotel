using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class BuildingMenu : Menu
{
    public BuildingMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Building menu[/]")
                .AddChoices("Add a building", "Edit a building", "Remove a building", "List all buildings",
                    "Back to main menu"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Add a building" => new BuildingCreator(),
            "Edit a building" => new BuildingUpdater(),
            "Remove a building" => new BuildingDeleter(),
            "List all buildings" => new BuildingReader(),
            _ => new MainMenu()
        };
    }
}