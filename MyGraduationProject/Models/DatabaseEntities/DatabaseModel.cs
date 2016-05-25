namespace MyGraduationProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseConnection")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersAdress> UsersAdresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.NAME)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.PRICE_PER_DAY)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.OVERALL_PRICE)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ReservationStatus>()
                .Property(e => e.NAME)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.NAME)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.LOGIN)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.PASSWORD)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.NAME)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.SURNAME)
                .IsFixedLength();

            modelBuilder.Entity<UsersAdress>()
                .Property(e => e.STREET_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<UsersAdress>()
                .Property(e => e.STREET_NUMBER)
                .IsUnicode(false);
        }
    }
}
