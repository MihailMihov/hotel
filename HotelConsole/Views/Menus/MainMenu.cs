using Spectre.Console;

namespace HotelConsole.Views.Menus;

public class MainMenu : Menu
{
    public MainMenu()
    {
        var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Main menu[/]")
                .AddChoices("Buildings", "Rooms", "Room Kinds", "Parkings", "Vehicles", "Reservations", "Clients",
                    "Exit"));

        Select(menu);
    }

    private void Select(string menu)
    {
        Next = menu switch
        {
            "Buildings" => new BuildingMenu(),
            "Rooms" => new RoomsMenu(),
            "Room Kinds" => new RoomKindMenu(),
            "Parkings" => new ParkingsMenu(),
            "Vehicles" => new VehicleMenu(),
            "Reservations" => new ReservationsMenu(),
            "Clients" => new ClientsMenu(),
            _ => null
        };
    }
}