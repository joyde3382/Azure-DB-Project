using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E18I4DABH4Gr4.Models
{
    public class SmartGridDBContext : DbContext
    {
        public SmartGridDBContext(DbContextOptions<SmartGridDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmartGrid>().Property<int>("ProducerForeignKey");
            modelBuilder.Entity<SmartGrid>().Property<int>("ConsumerForeignKey");

            modelBuilder.Entity<SmartGrid>().HasKey(s => s.SmartGridId);

            modelBuilder.Entity<SmartGrid>().HasMany(p => p.Consumers)
                .WithOne()
                .HasForeignKey("ConsumerForeignKey");
            modelBuilder.Entity<SmartGrid>().HasMany(p => p.Producers)
                .WithOne()
                .HasForeignKey("ProducerForeignKey");
           
        }

        public DbSet<SmartGrid> SmartGrids { get; set; }
        public DbSet<Prosumer> Prosumers { get; set; }
    }
}
