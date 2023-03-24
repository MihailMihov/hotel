using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Readers;

public class ParkingReader : Reader
{
    public ParkingReader()
    {
        ReaderType = ReaderType.Parking;
    }
    public ParkingReader(IEnumerable<Parking> parkings) : this()
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.Expand();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Capacity");
        table.AddColumn("Vehicle Count");
        foreach (var parking in parkings)
        {
            table.AddRow(parking.Id.ToString(), parking.Name, parking.Capacity.ToString(), parking.Vehicles.Count.ToString());
        }
        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[yellow]Press any key to go back[/]"));
        Console.Read();
    }
}