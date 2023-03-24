using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class ReservationsMenu : Menu
{
    public ReservationsMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Reservation menu[/]")
                .AddChoices("Add a reservation", "Edit a reservation", "Remove a reservation", "List all reservations",
                    "Back to main menu"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Add a reservation" => new ReservationCreator(),
            "Edit a reservation" => new ReservationUpdater(),
            "Remove a reservation" => new ReservationDeleter(),
            "List all reservations" => new ReservationReader(),
            _ => new MainMenu()
        };
    }
}