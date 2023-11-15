
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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity<AccountRole>();
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Accounts);

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

