using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Readers;

public class ClientReader : Reader
{
    public ClientReader()
    {
        ReaderType = ReaderType.Client;
    }
    public ClientReader(IEnumerable<Client> clients) : this()
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.Expand();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("UCN");
        table.AddColumn("Room ID");
        foreach (var client in clients)
        {
            table.AddRow(client.Id.ToString(), client.Name, client.Ucn, client.RoomId.ToString());
        }
        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[yellow]Press any key to go back[/]"));
        Console.Read();
    }
}