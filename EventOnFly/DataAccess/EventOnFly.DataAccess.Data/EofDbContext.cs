﻿using EventOnFly.DataAccess.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventOnFly.DataAccess.Data
{
    public class EofDbContext : DbContext
    {
        public EofDbContext(DbContextOptions<EofDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyValue> PropertyValues { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<ServiceOrder> ServiceOrders { get; set; }

        public DbSet<ServiceOrderPropertyValue> ServiceOrderPropertyValues { get; set; }

        public DbSet<ServicePropertyValue> ServicePropertyValues { get; set; }

        public DbSet<ServiceRelation> ServiceRelations { get; set; }

        public DbSet<ServiceTypePropertyRel> ServiceTypePropertyRels { get; set; }

        public DbSet<ServiceTypeRelation> ServiceTypeRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.Name.Split('.').Last();
            }

            modelBuilder.Entity<ServiceOrder>()
                .HasOne(p => p.Booking)
                .WithMany()
                .HasForeignKey(p => p.BookingId);
            modelBuilder.Entity<ServiceOrder>()
                .HasOne(p => p.Service)
                .WithMany()
                .HasForeignKey(p => p.ServiceId);
            modelBuilder.Entity<ServiceOrder>().HasKey(p => new { p.ServiceId, p.BookingId });

            modelBuilder.Entity<ServiceOrderPropertyValue>()
                .HasOne(p => p.ServiceOrder)
                .WithMany()
                .HasForeignKey(p => new { p.ServiceId, p.BookingId });
            modelBuilder.Entity<ServiceOrderPropertyValue>()
                .HasOne(p => p.Property)
                .WithMany()
                .HasForeignKey(p => p.PropertyId);
            modelBuilder.Entity<ServiceOrderPropertyValue>()
                .HasOne(p => p.PropertyValue)
                .WithMany()
                .HasForeignKey(p => p.PropertyValueId);
            modelBuilder.Entity<ServiceOrderPropertyValue>().HasKey(p => new { p.ServiceId, p.BookingId, p.PropertyId });

            modelBuilder.Entity<ServicePropertyValue>()
                .HasOne(p => p.Service)
                .WithMany()
                .HasForeignKey(p => p.ServiceId);
            modelBuilder.Entity<ServicePropertyValue>()
                .HasOne(p => p.Property)
                .WithMany()
                .HasForeignKey(p => p.PropertyId);
            modelBuilder.Entity<ServicePropertyValue>()
                .HasOne(p => p.PropertyValue)
                .WithMany()
                .HasForeignKey(p => p.PropertyValueId);
            modelBuilder.Entity<ServicePropertyValue>().HasKey(p => new { p.ServiceId, p.PropertyId });

            modelBuilder.Entity<ServiceRelation>()
                .HasOne(p => p.Service1)
                .WithMany()
                .HasForeignKey(p => p.Service1Id);
            modelBuilder.Entity<ServiceRelation>()
                .HasOne(p => p.Service2)
                .WithMany()
                .HasForeignKey(p => p.Service2Id);
            modelBuilder.Entity<ServiceRelation>().HasKey(p => new { p.Service1Id, p.Service2Id });

            modelBuilder.Entity<ServiceTypePropertyRel>()
                .HasOne(p => p.Property)
                .WithMany()
                .HasForeignKey(p => p.PropertyId);
            modelBuilder.Entity<ServiceTypePropertyRel>().HasKey(p => new { p.ServiceType, p.PropertyId });

            modelBuilder.Entity<ServiceTypeRelation>()
                .HasOne(p => p.Service)
                .WithMany()
                .HasForeignKey(p => p.ServiceId);
            modelBuilder.Entity<ServiceTypeRelation>().HasKey(p => new { p.ServiceId, p.ServiceType });
        }
    }
}