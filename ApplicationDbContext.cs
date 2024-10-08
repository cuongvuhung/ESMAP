
using System;
using System.Data;
using ESMAP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ESMAP
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Typ> Typs { get; set; }
        public DbSet<Wire> Wires { get; set; }
        public DbSet<StationLine> StationLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasOne(e => e.Station)
                .WithMany(r => r.Devices);
            
            modelBuilder.Entity<Device>()
                .HasOne(e => e.Typ)
                .WithMany(r => r.Devices);
            
            modelBuilder.Entity<Line>()
                .HasMany(e=>e.Stations)
                .WithMany(r => r.Lines)
                .UsingEntity<StationLine>();
            
            modelBuilder.Entity<Wire>()
                .HasOne(a => a.Line)
                .WithMany(a => a.Wires);
        }        
    }
}


