﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(MotoDevDbContext))]
    [Migration("20240624092046_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MotoDev.Domain.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CarTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EngineTypeId")
                        .HasColumnType("int");

                    b.Property<int>("EuroStandard")
                        .HasColumnType("int");

                    b.Property<int>("HorsePowers")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TransmissionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarTypeId");

                    b.HasIndex("EngineTypeId");

                    b.HasIndex("TransmissionTypeId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.CarType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.ClientCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientCars");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.EngineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("EngineTypes");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientCarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LastKilometers")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("RepairTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientCarId");

                    b.HasIndex("RepairTypeId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.RepairType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RepairType");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.TransmissionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TransmissionTypes");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdatedByUserId")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.Car", b =>
                {
                    b.HasOne("MotoDev.Domain.Entities.CarType", "CarType")
                        .WithMany()
                        .HasForeignKey("CarTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Domain.Entities.EngineType", "EngineType")
                        .WithMany()
                        .HasForeignKey("EngineTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Domain.Entities.TransmissionType", "TransmissionType")
                        .WithMany()
                        .HasForeignKey("TransmissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarType");

                    b.Navigation("EngineType");

                    b.Navigation("TransmissionType");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.ClientCar", b =>
                {
                    b.HasOne("MotoDev.Domain.Entities.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Domain.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.Repair", b =>
                {
                    b.HasOne("MotoDev.Domain.Entities.ClientCar", "ClientCar")
                        .WithMany("Repairs")
                        .HasForeignKey("ClientCarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Domain.Entities.RepairType", "RepairType")
                        .WithMany()
                        .HasForeignKey("RepairTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCar");

                    b.Navigation("RepairType");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("MotoDev.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.ClientCar", b =>
                {
                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("MotoDev.Domain.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}