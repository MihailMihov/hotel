using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Readers;

public class VehicleReader : Reader
{
    public VehicleReader()
    {
        ReaderType = ReaderType.Vehicle;
    }
    public VehicleReader(IEnumerable<Vehicle> vehicles) : this()
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.Expand();
        table.AddColumn("Registration");
        table.AddColumn("Client ID");
        table.AddColumn("Parking ID");
        foreach (var vehicle in vehicles)
        {
            table.AddRow(vehicle.Registration, vehicle.ClientId.ToString(), vehicle.ParkingId.ToString());
        }
        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[yellow]Press any key to go back[/]"));
        Console.Read();
    }
}