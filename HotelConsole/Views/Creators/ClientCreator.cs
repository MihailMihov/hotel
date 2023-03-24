using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Creators;

public class ClientCreator : Creator
{
    public Client Client;

    public ClientCreator()
    {
        CreatorType = CreatorType.Client;
        Client = new Client
        {
            Name = AnsiConsole.Ask<string>("Enter the [yellow]client's[/] [darkorange]name[/]:"),
            Ucn = AnsiConsole.Ask<string>("Enter the [yellow]client's[/] [darkorange]UCN[/]:"),
            RoomId = AnsiConsole.Ask<int>("Enter the [yellow]client's[/] [darkorange]room id[/]:")
        };
    }
}