﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Authentication;

namespace WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200830213417_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "5b51c132-73b8-4a39-8e33-957dce6b3a11",
                            ConcurrencyStamp = "7db56da8-f3f1-4e0d-b7c7-ea7b9cc90776",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "aa387b78-8df9-4fd1-8404-3fa989611a6e",
                            ConcurrencyStamp = "3bd76808-5bb3-4569-9f41-5493706a693e",
                            Name = "Airline_Admin",
                            NormalizedName = "AIRLINE_ADMIN"
                        },
                        new
                        {
                            Id = "cd6aa222-e047-4978-ab8f-e9d1cf57c801",
                            ConcurrencyStamp = "6ec06093-efe0-4c07-ac80-f0629c792f34",
                            Name = "Service_Admin",
                            NormalizedName = "SERVICE_ADMIN"
                        },
                        new
                        {
                            Id = "63db630a-7b1b-4793-ac83-55c31ab727a0",
                            ConcurrencyStamp = "b49c6ae7-7b31-45bf-964d-749c7a05b768",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApp.Authentication.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("Admin_ID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int?>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Admin_ID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("User_ID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebApp.Models.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Airline_ID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rent_a_Car_ID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Airline_ID");

                    b.HasIndex("Rent_a_Car_ID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("WebApp.Models.AircraftConfiguration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Aircraft_Configuration")
                        .HasColumnType("int");

                    b.Property<int>("Column_Number")
                        .HasColumnType("int");

                    b.Property<int>("Number_Of_Seats")
                        .HasColumnType("int");

                    b.Property<bool>("Reserved")
                        .HasColumnType("bit");

                    b.Property<int>("Row_Number")
                        .HasColumnType("int");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Aircraft_Configuration");

                    b.HasIndex("User_ID");

                    b.ToTable("AircraftConfigurations");
                });

            modelBuilder.Entity("WebApp.Models.Airline", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address_")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Promo_Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("WebApp.Models.AirlineDestinationConnection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Airline_Destination_Connections")
                        .HasColumnType("int");

                    b.Property<int>("Airline_ID")
                        .HasColumnType("int");

                    b.Property<int>("Destination_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Airline_Destination_Connections");

                    b.HasIndex("Airline_ID");

                    b.HasIndex("Destination_ID");

                    b.ToTable("AirlineDestinations");
                });

            modelBuilder.Entity("WebApp.Models.Branch", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Branch")
                        .HasColumnType("int");

                    b.Property<int>("Number_Of_Vehicle")
                        .HasColumnType("int");

                    b.Property<int>("Rent_a_Car_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Branch");

                    b.HasIndex("Rent_a_Car_ID");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("WebApp.Models.CarReservation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Car_Reservations")
                        .HasColumnType("int");

                    b.Property<string>("Reservation_Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.Property<int>("Vehicle_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Car_Reservations");

                    b.HasIndex("User_ID");

                    b.HasIndex("Vehicle_ID");

                    b.ToTable("CarReservations");
                });

            modelBuilder.Entity("WebApp.Models.Destination", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("WebApp.Models.Discounts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<int>("Pints")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("WebApp.Models.FastReservation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Airline_ID")
                        .HasColumnType("int");

                    b.Property<string>("Destination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<int?>("Fast_Reservation")
                        .HasColumnType("int");

                    b.Property<int>("Seat_Number")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Start_Destination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Airline_ID");

                    b.HasIndex("Fast_Reservation");

                    b.HasIndex("User_ID");

                    b.ToTable("FastReservations");
                });

            modelBuilder.Entity("WebApp.Models.Flight", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Destination_ID")
                        .HasColumnType("int");

                    b.Property<string>("Destination_Transfer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Flight")
                        .HasColumnType("int");

                    b.Property<string>("Flight_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number_Transfer")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price2")
                        .HasColumnType("float");

                    b.Property<DateTime>("Start_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Travel_Length")
                        .HasColumnType("time");

                    b.HasKey("ID");

                    b.HasIndex("Destination_ID");

                    b.HasIndex("Flight");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("WebApp.Models.MyUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("MyUsers");
                });

            modelBuilder.Entity("WebApp.Models.PriceList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Airline_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Price_List")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Airline_ID");

                    b.HasIndex("Price_List");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("WebApp.Models.PriceListServiceConnection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PriceList_Service_Connections")
                        .HasColumnType("int");

                    b.Property<int>("Price_List_ID")
                        .HasColumnType("int");

                    b.Property<int>("Service_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PriceList_Service_Connections");

                    b.HasIndex("Price_List_ID");

                    b.ToTable("PriceListServices");
                });

            modelBuilder.Entity("WebApp.Models.RentACar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Promo_Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("RentACars");
                });

            modelBuilder.Entity("WebApp.Models.RentPriceList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Rent_a_Car_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Rent_a_Car_ID");

                    b.ToTable("RentPriceLists");
                });

            modelBuilder.Entity("WebApp.Models.RentService", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Key");

                    b.ToTable("RentServices");
                });

            modelBuilder.Entity("WebApp.Models.RentaPriceListServiceConnection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Rent_Price_List_ID")
                        .HasColumnType("int");

                    b.Property<int>("Rent_Service_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Renta_Price_List_Service_Connections")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Renta_Price_List_Service_Connections");

                    b.ToTable("RentaPriceListServices");
                });

            modelBuilder.Entity("WebApp.Models.Service", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Information")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("WebApp.Models.Vehicle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Branch_ID")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("Reserved")
                        .HasColumnType("bit");

                    b.Property<int?>("Vehicle")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Branch_ID");

                    b.HasIndex("Vehicle");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApp.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApp.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApp.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Authentication.ApplicationUser", b =>
                {
                    b.HasOne("WebApp.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("Admin_ID");

                    b.HasOne("WebApp.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("User_ID");
                });

            modelBuilder.Entity("WebApp.Models.Admin", b =>
                {
                    b.HasOne("WebApp.Models.Airline", "Airline")
                        .WithMany()
                        .HasForeignKey("Airline_ID");

                    b.HasOne("WebApp.Models.RentACar", "Rent_a_Car")
                        .WithMany()
                        .HasForeignKey("Rent_a_Car_ID");
                });

            modelBuilder.Entity("WebApp.Models.AircraftConfiguration", b =>
                {
                    b.HasOne("WebApp.Models.MyUser", null)
                        .WithMany("Aircraft_Configurations")
                        .HasForeignKey("Aircraft_Configuration");

                    b.HasOne("WebApp.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.AirlineDestinationConnection", b =>
                {
                    b.HasOne("WebApp.Models.Airline", null)
                        .WithMany("Airline_Destination_Connections")
                        .HasForeignKey("Airline_Destination_Connections");

                    b.HasOne("WebApp.Models.Destination", null)
                        .WithMany("Airline_Destination_Connections")
                        .HasForeignKey("Airline_Destination_Connections");

                    b.HasOne("WebApp.Models.Airline", "Airline")
                        .WithMany()
                        .HasForeignKey("Airline_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Destination", "Destination")
                        .WithMany()
                        .HasForeignKey("Destination_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.Branch", b =>
                {
                    b.HasOne("WebApp.Models.RentACar", null)
                        .WithMany("Branches")
                        .HasForeignKey("Branch");

                    b.HasOne("WebApp.Models.RentACar", "Rent_a_Car")
                        .WithMany()
                        .HasForeignKey("Rent_a_Car_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.CarReservation", b =>
                {
                    b.HasOne("WebApp.Models.MyUser", null)
                        .WithMany("Car_Reservations")
                        .HasForeignKey("Car_Reservations");

                    b.HasOne("WebApp.Models.Vehicle", null)
                        .WithMany("Car_Reservations")
                        .HasForeignKey("Car_Reservations");

                    b.HasOne("WebApp.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("Vehicle_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.FastReservation", b =>
                {
                    b.HasOne("WebApp.Models.Airline", "Airline")
                        .WithMany()
                        .HasForeignKey("Airline_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Airline", null)
                        .WithMany("Fast_Reservations")
                        .HasForeignKey("Fast_Reservation");

                    b.HasOne("WebApp.Models.MyUser", null)
                        .WithMany("Fast_Reservations")
                        .HasForeignKey("Fast_Reservation");

                    b.HasOne("WebApp.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.Flight", b =>
                {
                    b.HasOne("WebApp.Models.Destination", "Destination")
                        .WithMany()
                        .HasForeignKey("Destination_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Destination", null)
                        .WithMany("Flights")
                        .HasForeignKey("Flight");
                });

            modelBuilder.Entity("WebApp.Models.PriceList", b =>
                {
                    b.HasOne("WebApp.Models.Airline", "Airline")
                        .WithMany()
                        .HasForeignKey("Airline_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Airline", null)
                        .WithMany("Prise_Lists")
                        .HasForeignKey("Price_List");
                });

            modelBuilder.Entity("WebApp.Models.PriceListServiceConnection", b =>
                {
                    b.HasOne("WebApp.Models.PriceList", null)
                        .WithMany("PriceList_Service_Connections")
                        .HasForeignKey("PriceList_Service_Connections");

                    b.HasOne("WebApp.Models.Service", "Srvice")
                        .WithMany("PriceList_Service_Connections")
                        .HasForeignKey("PriceList_Service_Connections");

                    b.HasOne("WebApp.Models.PriceList", "Price_List")
                        .WithMany()
                        .HasForeignKey("Price_List_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.RentPriceList", b =>
                {
                    b.HasOne("WebApp.Models.RentACar", "Rent_a_Car")
                        .WithMany()
                        .HasForeignKey("Rent_a_Car_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.RentaPriceListServiceConnection", b =>
                {
                    b.HasOne("WebApp.Models.RentPriceList", "RentPriceList")
                        .WithMany("Renta_Price_List_Service_Connections")
                        .HasForeignKey("Renta_Price_List_Service_Connections");

                    b.HasOne("WebApp.Models.RentService", "RentService")
                        .WithMany("Renta_Price_List_Service_Connections")
                        .HasForeignKey("Renta_Price_List_Service_Connections");
                });

            modelBuilder.Entity("WebApp.Models.Vehicle", b =>
                {
                    b.HasOne("WebApp.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("Branch_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Branch", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("Vehicle");
                });
#pragma warning restore 612, 618
        }
    }
}