using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Deleters;

public class ClientDeleter : Deleter
{
    public List<int> ClientIds = new();

    public ClientDeleter()
    {
        DeleterType = DeleterType.Client;
    }

    public ClientDeleter(ICollection<Client> clients) : this()
    {
        var targetClientNames = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which client(s) would you like to delete?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other clients)[/]")
                .AddChoices(clients.Select(b => b.Name)));

        if (!AnsiConsole.Confirm($"Are you sure that you want to delete {targetClientNames.Count} clients?")) return;

        foreach (var clientName in targetClientNames) ClientIds.Add(clients.First(b => b.Name == clientName).Id);
    }
}