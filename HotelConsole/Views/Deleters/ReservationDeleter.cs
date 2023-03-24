using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Deleters;

public class ReservationDeleter : Deleter
{
    public List<int> ReservationIds = new();
    public ReservationDeleter()
    {
        DeleterType = DeleterType.Reservation;
    }
    public ReservationDeleter(ICollection<Reservation> reservations) : this()
    {
        var targetReservationClientEmails = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Which reservation(s) would you like to delete?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other reservations)[/]")
                .AddChoices(reservations.Select(b => b.ClientEmail)));

        if (!AnsiConsole.Confirm($"Are you sure that you want to delete {targetReservationClientEmails.Count} reservations?"))
        {
            return;
        }
        
        foreach (var reservationClientEmail in targetReservationClientEmails)
        {
            ReservationIds.Add(reservations.First(b => b.ClientEmail == reservationClientEmail).Id);
        }
    }
}