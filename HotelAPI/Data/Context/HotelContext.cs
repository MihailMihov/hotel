using HotelAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Data.Context;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building>? Buildings { get; set; }

    public virtual DbSet<Client>? Clients { get; set; }

    public virtual DbSet<Parking>? Parkings { get; set; }

    public virtual DbSet<Reservation>? Reservations { get; set; }

    public virtual DbSet<Room>? Rooms { get; set; }

    public virtual DbSet<RoomKind>? RoomKinds { get; set; }

    public virtual DbSet<Vehicle>? Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("buildings_pkey");

            entity.ToTable("buildings");

            entity.HasIndex(e => e.Name, "buildings_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Floors).HasColumnName("floors");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Ucn)
                .HasMaxLength(20)
                .HasColumnName("ucn");

            entity.HasOne(d => d.Room).WithMany(p => p.Clients)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clients_room_id_fkey");
        });

        modelBuilder.Entity<Parking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parking_pkey");

            entity.ToTable("parking");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reservations_pkey");

            entity.ToTable("reservations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientEmail)
                .HasMaxLength(50)
                .HasColumnName("client_email");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.LateCheckout).HasColumnName("late_checkout");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservations_room_id_fkey");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rooms_pkey");

            entity.ToTable("rooms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuildingId).HasColumnName("building_id");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.KindId).HasColumnName("kind_id");

            entity.HasOne(d => d.Building).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rooms_building_id_fkey");

            entity.HasOne(d => d.Kind).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.KindId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rooms_kind_id_fkey");
        });

        modelBuilder.Entity<RoomKind>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("room_kinds_pkey");

            entity.ToTable("room_kinds");

            entity.HasIndex(e => e.Name, "room_kinds_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Registration).HasName("vehicles_pkey");

            entity.ToTable("vehicles");

            entity.Property(e => e.Registration)
                .HasMaxLength(10)
                .HasColumnName("registration");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ParkingId).HasColumnName("parking_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_client_id_fkey");

            entity.HasOne(d => d.Parking).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ParkingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicles_parking_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}