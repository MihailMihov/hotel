using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class ClientUpdater : Updater
{
    public readonly Client Client;

    public ClientUpdater()
    {
        UpdaterType = UpdaterType.Client;
    }
    public ClientUpdater(ICollection<Client> clients) : this()
    {
        var targetClientName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which client would you like to update?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other clients)[/]")
                .AddChoices(clients.Select(b => b.Name)));

        Client = clients.First(b => b.Name == targetClientName);
        
        while (true)
        {
            var targetField = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to update?")
                    .AddChoices("Name", "UCN", "Room ID", "Back to Clients Menu"));
            
            if(targetField == "Back to Clients Menu") break;

            switch (targetField)
            {
                case "Name":
                    Client.Name = AnsiConsole.Ask<string>($"Enter [yellow]{Client.Name}'s[/] new [darkorange]name[/]:");
                    break;
                case "UCN":
                    Client.Ucn =
                        AnsiConsole.Ask<string>($"Enter [yellow]{Client.Name}'s[/] new [darkorange]UCN[/]:");
                    break;
                case "Room ID":
                    Client.RoomId =
                        AnsiConsole.Ask<int>($"Enter [yellow]{Client.Name}'s[/] new [darkorange]room id[/]:");
                    break;
            }
        }
    }
}