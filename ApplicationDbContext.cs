
using System;
using System.Data;
using CBM_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CBM_API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        //////////////////////////////////////////////////
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Substation> Substations { get; set; }
        public DbSet<Bay> Bays { get; set; }
        //////////////////////////////////////////////////
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ManufactureModel> ManufactureModel { get; set; }
        public DbSet<ManufactureDeviceType> ManufactureDeviceType { get; set; }
        //////////////////////////////////////////////////
        public DbSet<Device> Devices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity<AccountRole>();
            
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Accounts);

            modelBuilder.Entity<Province>()
                .HasMany(d => d.Substations);

            modelBuilder.Entity<Substation>()
                .HasMany(d => d.Bays);

            modelBuilder.Entity<Bay>()
                .HasMany(d => d.Devices);

            modelBuilder.Entity<Manufacture>()
                .HasMany(d => d.DeviceTypes)
                .WithMany(m => m.Manufactures)
                .UsingEntity<ManufactureDeviceType>();

            modelBuilder.Entity<Manufacture>()
                .HasMany(d => d.Devices);
            
            modelBuilder.Entity<DeviceType>()
                .HasMany(m => m.Manufactures)
                .WithMany(d=>d.DeviceTypes)
                .UsingEntity<ManufactureDeviceType>();

            modelBuilder.Entity<DeviceType>()
                .HasMany(m => m.Models);

            modelBuilder.Entity<Model>()
                .HasMany(mf => mf.Manufactures)
                .WithMany(md => md.Models)
                .UsingEntity<ManufactureModel>();

            modelBuilder.Entity<Model>()
                .HasMany(d => d.Devices);


            /*    modelBuilder.Entity<Menu>()
                    .HasMany(e => e.Foods)
                    .WithMany(e => e.Menus)
                    .UsingEntity<MenuFood>();

                modelBuilder.Entity<Account>()
                    .HasMany(r => r.Registrations)
                    .WithOne(a=>a.Account);



                modelBuilder.Entity<Account>()
                    .HasMany(e => e.Notifications)
                    .WithMany(e => e.Accounts)
                    .UsingEntity<AccountNotification>();*/

        }        
    }
}

