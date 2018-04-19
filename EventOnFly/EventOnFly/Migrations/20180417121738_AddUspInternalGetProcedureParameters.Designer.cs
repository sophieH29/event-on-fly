﻿// <auto-generated />
using System;
using EventOnFly.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOnFly.Migrations
{
    [DbContext(typeof(EofDbContext))]
    [Migration("20180417121738_AddUspInternalGetProcedureParameters")]
    partial class AddUspInternalGetProcedureParameters
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventOnFly.Data.DbModels.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.HasKey("Id");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DefaultValueId");

                    b.Property<int?>("DefaultVaueId");

                    b.Property<string>("Name");

                    b.Property<int>("PropertyType");

                    b.HasKey("Id");

                    b.HasIndex("DefaultVaueId");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.PropertyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("BooleanValue");

                    b.Property<double?>("FloatValue");

                    b.Property<int?>("IntegerValue");

                    b.Property<string>("TextValue");

                    b.HasKey("Id");

                    b.ToTable("PropertyValue");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ServiceType");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceOrder", b =>
                {
                    b.Property<int>("ServiceId");

                    b.Property<int>("BookingId");

                    b.HasKey("ServiceId", "BookingId");

                    b.HasAlternateKey("BookingId", "ServiceId");

                    b.ToTable("ServiceOrder");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceOrderPropertyValue", b =>
                {
                    b.Property<int>("ServiceId");

                    b.Property<int>("BookingId");

                    b.Property<int>("PropertyId");

                    b.Property<int?>("PropertyValueId");

                    b.HasKey("ServiceId", "BookingId", "PropertyId");

                    b.HasAlternateKey("BookingId", "PropertyId", "ServiceId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("PropertyValueId");

                    b.ToTable("ServiceOrderPropertyValue");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServicePropertyValue", b =>
                {
                    b.Property<int>("ServiceId");

                    b.Property<int>("PropertyId");

                    b.Property<string>("EvaluationScript");

                    b.Property<int?>("PropertyValueId");

                    b.HasKey("ServiceId", "PropertyId");

                    b.HasAlternateKey("PropertyId", "ServiceId");

                    b.HasIndex("PropertyValueId");

                    b.ToTable("ServicePropertyValue");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceRelation", b =>
                {
                    b.Property<int>("Service1Id");

                    b.Property<int>("Service2Id");

                    b.Property<int>("RelationType");

                    b.HasKey("Service1Id", "Service2Id");

                    b.HasIndex("Service2Id");

                    b.ToTable("ServiceRelation");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceTypePropertyRel", b =>
                {
                    b.Property<int>("ServiceType");

                    b.Property<int>("PropertyId");

                    b.HasKey("ServiceType", "PropertyId");

                    b.HasAlternateKey("PropertyId", "ServiceType");

                    b.ToTable("ServiceTypePropertyRel");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceTypeRelation", b =>
                {
                    b.Property<int>("ServiceId");

                    b.Property<int>("ServiceType");

                    b.Property<string>("AlowanceScript");

                    b.HasKey("ServiceId", "ServiceType");

                    b.ToTable("ServiceTypeRelation");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.Property", b =>
                {
                    b.HasOne("EventOnFly.Data.DbModels.PropertyValue", "DefaultVaue")
                        .WithMany()
                        .HasForeignKey("DefaultVaueId");
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceOrder", b =>
                {
                    b.HasOne("EventOnFly.Data.DbModels.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventOnFly.Data.DbModels.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceOrderPropertyValue", b =>
                {
                    b.HasOne("EventOnFly.Data.DbModels.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventOnFly.Data.DbModels.PropertyValue", "PropertyValue")
                        .WithMany()
                        .HasForeignKey("PropertyValueId");

                    b.HasOne("EventOnFly.Data.DbModels.ServiceOrder", "ServiceOrder")
                        .WithMany()
                        .HasForeignKey("ServiceId", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServicePropertyValue", b =>
                {
                    b.HasOne("EventOnFly.Data.DbModels.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventOnFly.Data.DbModels.PropertyValue", "PropertyValue")
                        .WithMany()
                        .HasForeignKey("PropertyValueId");

                    b.HasOne("EventOnFly.Data.DbModels.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceRelation", b =>
                {
                    b.HasOne("EventOnFly.Data.DbModels.Service", "Service1")
                        .WithMany()
                        .HasForeignKey("Service1Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventOnFly.Data.DbModels.Service", "Service2")
                        .WithMany()
                        .HasForeignKey("Service2Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceTypePropertyRel", b =>
                {
                    b.HasOne("EventOnFly.Data.DbModels.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventOnFly.Data.DbModels.ServiceTypeRelation", b =>
                {
                    b.HasOne("EventOnFly.Data.DbModels.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}