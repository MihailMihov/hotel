using HotelConsole.Views;
using HotelConsole.Views.Creators;
using HotelConsole.Views.Deleters;
using HotelConsole.Views.Menus;
using HotelConsole.Views.Readers;
using HotelConsole.Views.Updaters;
using Spectre.Console;

namespace HotelConsole.Controllers;

public class HotelConsoleController
{
    private readonly HotelApiController _hotelApiController;

    private View? _view;

    public HotelConsoleController(string apiUrl)
    {
        Utility.WriteFiglet();

        _hotelApiController = new HotelApiController(apiUrl, Utility.CreateHttpClient());

        OpenView(new MainMenu());
    }

    private void OpenView(View view)
    {
        Utility.WriteFiglet();
        _view = view;
        while (true)
        {
            AnsiConsole.Clear();
            Utility.WriteFiglet();

            switch (_view.ViewType)
            {
                case ViewType.Menu:
                    break;
                case ViewType.Creator:
                    HandleCreator();
                    break;
                case ViewType.Reader:
                    HandleReader();
                    break;
                case ViewType.Updater:
                    HandleUpdater();
                    break;
                case ViewType.Deleter:
                    HandleDeleter();
                    break;
                case ViewType.View:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_view.Next == null)
            {
                OpenView(new MainMenu());
                if (_view.Next == null) break;
            }

            _view = _view.Next;
        }
    }

    private void HandleCreator()
    {
        var creator = (Creator) _view!;
        switch (creator.CreatorType)
        {
            case CreatorType.Building:
                var buildingCreator = (BuildingCreator) creator;
                _hotelApiController.BuildingsPostAsync(buildingCreator.Building);
                break;
            case CreatorType.Client:
                var clientCreator = (ClientCreator) creator;
                _hotelApiController.ClientsPostAsync(clientCreator.Client);
                break;
            case CreatorType.Parking:
                var parkingCreator = (ParkingCreator) creator;
                _hotelApiController.ParkingsPostAsync(parkingCreator.Parking);
                break;
            case CreatorType.Reservation:
                var reservationCreator = (ReservationCreator) creator;
                _hotelApiController.ReservationsPostAsync(reservationCreator.Reservation);
                break;
            case CreatorType.Room:
                var roomCreator = (RoomCreator) creator;
                _hotelApiController.RoomsPostAsync(roomCreator.Room);
                break;
            case CreatorType.RoomKind:
                var roomKindCreator = (RoomKindCreator) creator;
                _hotelApiController.RoomkindsPostAsync(roomKindCreator.RoomKind);
                break;
            case CreatorType.Vehicle:
                var vehicleCreator = (VehicleCreator) creator;
                _hotelApiController.VehiclesPostAsync(vehicleCreator.Vehicle);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleReader()
    {
        var reader = (Reader) _view!;
        switch (reader.ReaderType)
        {
            case ReaderType.Building:
                _view = new BuildingReader(_hotelApiController.BuildingsAllAsync().Result);
                break;
            case ReaderType.Client:
                _view = new ClientReader(_hotelApiController.ClientsAllAsync().Result);
                break;
            case ReaderType.Parking:
                _view = new ParkingReader(_hotelApiController.ParkingsAllAsync().Result);
                break;
            case ReaderType.Reservation:
                _view = new ReservationReader(_hotelApiController.ReservationsAllAsync().Result);
                break;
            case ReaderType.Room:
                _view = new RoomReader(_hotelApiController.RoomsAllAsync().Result);
                break;
            case ReaderType.RoomKind:
                _view = new RoomKindReader(_hotelApiController.RoomkindsAllAsync().Result);
                break;
            case ReaderType.Vehicle:
                _view = new VehicleReader(_hotelApiController.VehiclesAllAsync().Result);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleUpdater()
    {
        var updater = (Updater) _view!;
        switch (updater.UpdaterType)
        {
            case UpdaterType.Building:
                var buildingUpdater = new BuildingUpdater(_hotelApiController.BuildingsAllAsync().Result);
                _hotelApiController.BuildingsPutAsync(buildingUpdater.Building.Id, buildingUpdater.Building);
                _view = buildingUpdater;
                break;
            case UpdaterType.Client:
                var clientUpdater = new ClientUpdater(_hotelApiController.ClientsAllAsync().Result);
                _hotelApiController.ClientsPutAsync(clientUpdater.Client.Id, clientUpdater.Client);
                _view = clientUpdater;
                break;
            case UpdaterType.Parking:
                var parkingUpdater = new ParkingUpdater(_hotelApiController.ParkingsAllAsync().Result);
                _hotelApiController.ParkingsPutAsync(parkingUpdater.Parking.Id, parkingUpdater.Parking);
                _view = parkingUpdater;
                break;
            case UpdaterType.Reservation:
                var reservationUpdater = new ReservationUpdater(_hotelApiController.ReservationsAllAsync().Result);
                _hotelApiController.ReservationsPutAsync(reservationUpdater.Reservation.Id,
                    reservationUpdater.Reservation);
                _view = reservationUpdater;
                break;
            case UpdaterType.Room:
                var roomUpdater = new RoomUpdater(_hotelApiController.RoomsAllAsync().Result);
                _hotelApiController.RoomsPutAsync(roomUpdater.Room.Id, roomUpdater.Room);
                _view = roomUpdater;
                break;
            case UpdaterType.RoomKind:
                var roomKindUpdater = new RoomKindUpdater(_hotelApiController.RoomkindsAllAsync().Result);
                _hotelApiController.RoomkindsPutAsync(roomKindUpdater.RoomKind.Id, roomKindUpdater.RoomKind);
                _view = roomKindUpdater;
                break;
            case UpdaterType.Vehicle:
                var vehicleUpdater = new VehicleUpdater(_hotelApiController.VehiclesAllAsync().Result);
                _hotelApiController.VehiclesPutAsync(vehicleUpdater.Vehicle.Registration, vehicleUpdater.Vehicle);
                _view = vehicleUpdater;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleDeleter()
    {
        var deleter = (Deleter) _view!;
        switch (deleter.DeleterType)
        {
            case DeleterType.Building:
                var buildingDeleter = new BuildingDeleter(_hotelApiController.BuildingsAllAsync().Result);
                foreach (var buildingId in buildingDeleter.BuildingIds)
                    _hotelApiController.BuildingsDeleteAsync(buildingId);
                _view = buildingDeleter;
                break;
            case DeleterType.Client:
                var clientDeleter = new ClientDeleter(_hotelApiController.ClientsAllAsync().Result);
                foreach (var clientId in clientDeleter.ClientIds)
                    _hotelApiController.ClientsDeleteAsync(clientId);
                _view = clientDeleter;
                break;
            case DeleterType.Parking:
                var parkingDeleter = new ParkingDeleter(_hotelApiController.ParkingsAllAsync().Result);
                foreach (var parkingId in parkingDeleter.ParkingIds)
                    _hotelApiController.ParkingsDeleteAsync(parkingId);
                _view = parkingDeleter;
                break;
            case DeleterType.Reservation:
                var reservationDeleter = new ReservationDeleter(_hotelApiController.ReservationsAllAsync().Result);
                foreach (var reservationId in reservationDeleter.ReservationIds)
                    _hotelApiController.ReservationsDeleteAsync(reservationId);
                _view = reservationDeleter;
                break;
            case DeleterType.Room:
                var roomDeleter = new RoomDeleter(_hotelApiController.RoomsAllAsync().Result);
                foreach (var roomId in roomDeleter.RoomIds)
                    _hotelApiController.RoomsDeleteAsync(roomId);
                _view = roomDeleter;
                break;
            case DeleterType.RoomKind:
                var roomKindDeleter = new RoomKindDeleter(_hotelApiController.RoomkindsAllAsync().Result);
                foreach (var roomKindId in roomKindDeleter.RoomKindIds)
                    _hotelApiController.RoomkindsDeleteAsync(roomKindId);
                _view = roomKindDeleter;
                break;
            case DeleterType.Vehicle:
                var vehicleDeleter = new VehicleDeleter(_hotelApiController.VehiclesAllAsync().Result);
                foreach (var vehicleRegistration in vehicleDeleter.VehicleRegistations)
                    _hotelApiController.VehiclesDeleteAsync(vehicleRegistration);
                _view = vehicleDeleter;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}