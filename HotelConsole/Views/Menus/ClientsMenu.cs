using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class ClientsMenu : Menu
{
    public ClientsMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Client menu[/]")
                .AddChoices("Add a client", "Edit a client", "Remove a client", "List all clients",
                    "Back to main menu"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Add a client" => new ClientCreator(),
            "Edit a client" => new ClientUpdater(),
            "Remove a client" => new ClientDeleter(),
            "List all clients" => new ClientReader(),
            _ => new MainMenu()
        };
    }
}