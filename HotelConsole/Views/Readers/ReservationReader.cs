using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Readers;

public class ReservationReader : Reader
{
    public ReservationReader()
    {
        ReaderType = ReaderType.Reservation;
    }

    public ReservationReader(IEnumerable<Reservation> reservations) : this()
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.Expand();
        table.AddColumn("ID");
        table.AddColumn("Client Email");
        table.AddColumn("Start Date");
        table.AddColumn("End Date");
        table.AddColumn("Late checkout");
        foreach (var reservation in reservations)
            table.AddRow(reservation.Id.ToString(), reservation.ClientEmail, reservation.StartDate, reservation.EndDate,
                reservation.LateCheckout.ToString());
        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[yellow]Press any key to go back[/]"));
        Console.Read();
    }
}