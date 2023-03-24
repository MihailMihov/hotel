using HotelConsole.Models;
using Spectre.Console;

namespace HotelConsole.Views.Updaters;

public class ReservationUpdater : Updater
{
    public readonly Reservation Reservation;

    public ReservationUpdater()
    {
        UpdaterType = UpdaterType.Reservation;
    }
    public ReservationUpdater(ICollection<Reservation> reservations) : this()
    {
        var targetReservationClientEmail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which reservation would you like to update?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal the other reservations)[/]")
                .AddChoices(reservations.Select(b => b.ClientEmail)));

        Reservation = reservations.First(b => b.ClientEmail == targetReservationClientEmail);
        
        while (true)
        {
            var targetField = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to update?")
                    .AddChoices("Client Email", "Start Date", "End Date", "Room ID", "Late Checkout", "Back to Reservations Menu"));
            
            if(targetField == "Back to Reservations Menu") break;

            switch (targetField)
            {
                case "Client Email":
                    Reservation.ClientEmail = AnsiConsole.Ask<string>($"Enter [yellow]{Reservation.ClientEmail}'s reservation's[/] new [darkorange]client email[/]:");
                    break;
                case "Start Date":
                    Reservation.StartDate =
                        AnsiConsole.Ask<string>($"Enter [yellow]{Reservation.ClientEmail}'s reservation's[/] new [darkorange]start date[/]:");
                    break;
                case "End Date":
                    Reservation.EndDate =
                        AnsiConsole.Ask<string>($"Enter [yellow]{Reservation.ClientEmail}'s reservation's[/] new [darkorange]end date[/]:");
                    break;
                case "Room ID":
                    Reservation.RoomId =
                        AnsiConsole.Ask<int>($"Enter [yellow]{Reservation.ClientEmail}'s reservation's[/] new [darkorange]room id[/]:");
                    break;
                case "Late checkout":
                    Reservation.LateCheckout =
                        AnsiConsole.Confirm("Should this reservation have a late checkout?");
                    break;
            }
        }
    }
}