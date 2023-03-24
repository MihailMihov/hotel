using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Creators;

public class ParkingCreator : Creator
{
    public Parking Parking;

    public ParkingCreator()
    {
        CreatorType = CreatorType.Parking;
        Parking = new Parking
        {
            Name = AnsiConsole.Ask<string>("Enter the [yellow]parking's[/] [darkorange]name[/]:"),
            Capacity = AnsiConsole.Ask<int>("Enter the [yellow]parking's[/] [darkorange]capacity[/]:")
        };
    }
}