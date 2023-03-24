using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Creators;

public class VehicleCreator : Creator
{
    public Vehicle Vehicle;
    
    public VehicleCreator()
    {
        CreatorType = CreatorType.Vehicle;
        Vehicle = new Vehicle
        {
            Registration = AnsiConsole.Ask<string>("Enter the [yellow]vehicle's[/] [darkorange]registration[/]:"),
            ClientId = AnsiConsole.Ask<int>("Enter the [yellow]vehicle's[/] [darkorange]client id[/]:"),
            ParkingId = AnsiConsole.Ask<int>("Enter the [yellow]vehicle's[/] [darkorange]parking id[/]:"),
        };
    }
}