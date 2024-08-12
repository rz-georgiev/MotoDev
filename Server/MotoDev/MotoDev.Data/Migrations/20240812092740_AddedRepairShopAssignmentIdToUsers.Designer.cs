﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotoDev.Data;

#nullable disable

namespace MotoDev.Data.Migrations
{
    [DbContext(typeof(MotoDevDbContext))]
    [Migration("20240812092740_AddedRepairShopAssignmentIdToUsers")]
    partial class AddedRepairShopAssignmentIdToUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MotoDev.Data.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CarTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("EngineTypeId")
                        .HasColumnType("int");

                    b.Property<int>("EuroStandard")
                        .HasColumnType("int");

                    b.Property<int>("HorsePowers")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int>("TransmissionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CarTypeId");

                    b.HasIndex("EngineTypeId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TransmissionTypeId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("MotoDev.Data.Models.CarType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

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

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RepairShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RepairShopId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("MotoDev.Data.Models.ClientCar", b =>
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

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("OtherModifications")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VinNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientCars");
                });

            modelBuilder.Entity("MotoDev.Data.Models.ClientCarRepair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("RepairId")
                        .HasColumnType("int");

                    b.Property<int>("RepairTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RepairId");

                    b.HasIndex("RepairTypeId");

                    b.ToTable("ClientCarRepair");
                });

            modelBuilder.Entity("MotoDev.Data.Models.EngineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("EngineTypes");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Model");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientCarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("LastKilometers")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RepairTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("ClientCarId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("MotoDev.Data.Models.RepairShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("RepairShops");
                });

            modelBuilder.Entity("MotoDev.Data.Models.RepairType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RepairType");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Role", b =>
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

            modelBuilder.Entity("MotoDev.Data.Models.TransmissionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TransmissionTypes");
                });

            modelBuilder.Entity("MotoDev.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountConfirmationHash")
                        .HasColumnType("longtext");

                    b.Property<int?>("AssignedToRepairShopId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastUpdatedByUserId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("MotoDev.Data.Models.UserRole", b =>
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

            modelBuilder.Entity("MotoDev.Data.Models.Car", b =>
                {
                    b.HasOne("MotoDev.Data.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Data.Models.CarType", "CarType")
                        .WithMany()
                        .HasForeignKey("CarTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Data.Models.EngineType", "EngineType")
                        .WithMany()
                        .HasForeignKey("EngineTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Data.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Data.Models.TransmissionType", "TransmissionType")
                        .WithMany()
                        .HasForeignKey("TransmissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("CarType");

                    b.Navigation("EngineType");

                    b.Navigation("Model");

                    b.Navigation("TransmissionType");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Client", b =>
                {
                    b.HasOne("MotoDev.Data.Models.RepairShop", null)
                        .WithMany("Clients")
                        .HasForeignKey("RepairShopId");
                });

            modelBuilder.Entity("MotoDev.Data.Models.ClientCar", b =>
                {
                    b.HasOne("MotoDev.Data.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Data.Models.Client", "Client")
                        .WithMany("ClientCars")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MotoDev.Data.Models.ClientCarRepair", b =>
                {
                    b.HasOne("MotoDev.Data.Models.Repair", "Repair")
                        .WithMany("ClientRepairs")
                        .HasForeignKey("RepairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Data.Models.RepairType", "RepairType")
                        .WithMany()
                        .HasForeignKey("RepairTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repair");

                    b.Navigation("RepairType");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Model", b =>
                {
                    b.HasOne("MotoDev.Data.Models.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Repair", b =>
                {
                    b.HasOne("MotoDev.Data.Models.ClientCar", "ClientCar")
                        .WithMany("Repairs")
                        .HasForeignKey("ClientCarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCar");
                });

            modelBuilder.Entity("MotoDev.Data.Models.RepairShop", b =>
                {
                    b.HasOne("MotoDev.Data.Models.User", "OwnerUser")
                        .WithMany("RepairShops")
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerUser");
                });

            modelBuilder.Entity("MotoDev.Data.Models.UserRole", b =>
                {
                    b.HasOne("MotoDev.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotoDev.Data.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Brand", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Client", b =>
                {
                    b.Navigation("ClientCars");
                });

            modelBuilder.Entity("MotoDev.Data.Models.ClientCar", b =>
                {
                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("MotoDev.Data.Models.Repair", b =>
                {
                    b.Navigation("ClientRepairs");
                });

            modelBuilder.Entity("MotoDev.Data.Models.RepairShop", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("MotoDev.Data.Models.User", b =>
                {
                    b.Navigation("RepairShops");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
