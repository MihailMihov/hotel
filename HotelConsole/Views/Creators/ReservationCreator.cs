using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Creators;

public class ReservationCreator : Creator
{
    public Reservation Reservation;

    public ReservationCreator()
    {
        CreatorType = CreatorType.Reservation;
        Reservation = new Reservation
        {
            RoomId = AnsiConsole.Ask<int>("Enter the [yellow]reservation's[/] [darkorange]room id[/]:"),
            ClientEmail =
                AnsiConsole.Ask<string>("Enter the [yellow]reservation's[/] [darkorange]contact email address[/]:"),
            StartDate = AnsiConsole.Ask<string>("Enter the [yellow]reservation's[/] [darkorange]start date[/]:"),
            EndDate = AnsiConsole.Ask<string>("Enter the [yellow]reservation's[/] [darkorange]end date[/]:"),
            LateCheckout = AnsiConsole.Confirm("Should this reservation be a late checkout?", false)
        };
    }
}