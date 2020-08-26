using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address_ = table.Column<string>(nullable: true),
                    Promo_Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MyUsers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Phone_Number = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    PictureURL = table.Column<string>(nullable: true),
                    IdToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RentACars",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Promo_Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentACars", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RentServices",
                columns: table => new
                {
                    Key = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentServices", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Information = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airline_ID = table.Column<int>(nullable: false),
                    Price_List = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceLists_Airlines_Airline_ID",
                        column: x => x.Airline_ID,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceLists_Airlines_Price_List",
                        column: x => x.Price_List,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirlineDestinations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airline_ID = table.Column<int>(nullable: false),
                    Destination_ID = table.Column<int>(nullable: false),
                    Airline_Destination_Connections = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineDestinations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Airlines_Airline_Destination_Connections",
                        column: x => x.Airline_Destination_Connections,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Destinations_Airline_Destination_Connections",
                        column: x => x.Airline_Destination_Connections,
                        principalTable: "Destinations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Airlines_Airline_ID",
                        column: x => x.Airline_ID,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Destinations_Destination_ID",
                        column: x => x.Destination_ID,
                        principalTable: "Destinations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_DateTime = table.Column<DateTime>(nullable: false),
                    End_DateTime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Price2 = table.Column<double>(nullable: false),
                    Flight_Number = table.Column<string>(nullable: true),
                    Number_Transfer = table.Column<int>(nullable: false),
                    Destination_Transfer = table.Column<string>(nullable: true),
                    Travel_Length = table.Column<TimeSpan>(nullable: false),
                    Destination_ID = table.Column<int>(nullable: false),
                    Flight = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Flights_Destinations_Destination_ID",
                        column: x => x.Destination_ID,
                        principalTable: "Destinations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Destinations_Flight",
                        column: x => x.Flight,
                        principalTable: "Destinations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AircraftConfigurations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row_Number = table.Column<int>(nullable: false),
                    Column_Number = table.Column<int>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false),
                    Number_Of_Seats = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: false),
                    Aircraft_Configuration = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftConfigurations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AircraftConfigurations_MyUsers_Aircraft_Configuration",
                        column: x => x.Aircraft_Configuration,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AircraftConfigurations_MyUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FastReservations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_Destination = table.Column<string>(nullable: true),
                    Start_DateTime = table.Column<DateTime>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Seat_Number = table.Column<int>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    Airline_ID = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: false),
                    Fast_Reservation = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastReservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FastReservations_Airlines_Airline_ID",
                        column: x => x.Airline_ID,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FastReservations_Airlines_Fast_Reservation",
                        column: x => x.Fast_Reservation,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FastReservations_MyUsers_Fast_Reservation",
                        column: x => x.Fast_Reservation,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FastReservations_MyUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Phone_Number = table.Column<string>(nullable: true),
                    Airline_ID = table.Column<int>(nullable: true),
                    Rent_a_Car_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Admins_Airlines_Airline_ID",
                        column: x => x.Airline_ID,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admins_RentACars_Rent_a_Car_ID",
                        column: x => x.Rent_a_Car_ID,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    Number_Of_Vehicle = table.Column<int>(nullable: false),
                    Rent_a_Car_ID = table.Column<int>(nullable: false),
                    Branche = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Branches_RentACars_Branche",
                        column: x => x.Branche,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_RentACars_Rent_a_Car_ID",
                        column: x => x.Rent_a_Car_ID,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentPriceLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rent_a_Car_ID = table.Column<int>(nullable: false),
                    Rent_Price_List = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentPriceLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RentPriceLists_RentACars_Rent_Price_List",
                        column: x => x.Rent_Price_List,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentPriceLists_RentACars_Rent_a_Car_ID",
                        column: x => x.Rent_a_Car_ID,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListServices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price_List_ID = table.Column<int>(nullable: false),
                    Service_ID = table.Column<int>(nullable: false),
                    PriceList_Service_Connections = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListServices_PriceLists_PriceList_Service_Connections",
                        column: x => x.PriceList_Service_Connections,
                        principalTable: "PriceLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListServices_Services_PriceList_Service_Connections",
                        column: x => x.PriceList_Service_Connections,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListServices_PriceLists_Price_List_ID",
                        column: x => x.Price_List_ID,
                        principalTable: "PriceLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: true),
                    Admin_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Admins_Admin_ID",
                        column: x => x.Admin_ID,
                        principalTable: "Admins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MyUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(nullable: true),
                    Desription = table.Column<string>(nullable: true),
                    Reserved = table.Column<bool>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Branche_ID = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: false),
                    Vehicle = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicles_Branches_Branche_ID",
                        column: x => x.Branche_ID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_MyUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Branches_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_MyUsers_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentaPriceListServices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rent_Price_List_ID = table.Column<int>(nullable: false),
                    Renta_Price_List_Service_Connections = table.Column<int>(nullable: true),
                    Rent_Service_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentaPriceListServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RentaPriceListServices_RentPriceLists_Renta_Price_List_Service_Connections",
                        column: x => x.Renta_Price_List_Service_Connections,
                        principalTable: "RentPriceLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentaPriceListServices_RentServices_Renta_Price_List_Service_Connections",
                        column: x => x.Renta_Price_List_Service_Connections,
                        principalTable: "RentServices",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06746913-762a-4a30-a8b0-3ea38c6b6db1", "31615e5a-8b6e-4ba3-8339-d403d09c1c9a", "Admin", "ADMIN" },
                    { "c3eb266c-f722-46c4-b6ce-e4e02f6da8ff", "d59bf6c4-2877-4edf-9dfa-0d9c05ed5520", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "2e331ca1-dcff-4f8a-a74b-3cddc60736c7", "8a6a21c2-682a-4eb6-a000-ee117667aae1", "Service_Admin", "SERVICE_ADMIN" },
                    { "a5ee3244-181d-4c08-a6c3-4d8c9f439a56", "340040f4-b5e1-4b5f-be76-732b94b5c195", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Airline_ID",
                table: "Admins",
                column: "Airline_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Rent_a_Car_ID",
                table: "Admins",
                column: "Rent_a_Car_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AircraftConfigurations_Aircraft_Configuration",
                table: "AircraftConfigurations",
                column: "Aircraft_Configuration");

            migrationBuilder.CreateIndex(
                name: "IX_AircraftConfigurations_User_ID",
                table: "AircraftConfigurations",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineDestinations_Airline_Destination_Connections",
                table: "AirlineDestinations",
                column: "Airline_Destination_Connections");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineDestinations_Airline_ID",
                table: "AirlineDestinations",
                column: "Airline_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineDestinations_Destination_ID",
                table: "AirlineDestinations",
                column: "Destination_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Admin_ID",
                table: "AspNetUsers",
                column: "Admin_ID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_User_ID",
                table: "AspNetUsers",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Branche",
                table: "Branches",
                column: "Branche");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Rent_a_Car_ID",
                table: "Branches",
                column: "Rent_a_Car_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservations_Airline_ID",
                table: "FastReservations",
                column: "Airline_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservations_Fast_Reservation",
                table: "FastReservations",
                column: "Fast_Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservations_User_ID",
                table: "FastReservations",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Destination_ID",
                table: "Flights",
                column: "Destination_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Flight",
                table: "Flights",
                column: "Flight");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_Airline_ID",
                table: "PriceLists",
                column: "Airline_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_Price_List",
                table: "PriceLists",
                column: "Price_List");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListServices_PriceList_Service_Connections",
                table: "PriceListServices",
                column: "PriceList_Service_Connections");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListServices_Price_List_ID",
                table: "PriceListServices",
                column: "Price_List_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RentaPriceListServices_Renta_Price_List_Service_Connections",
                table: "RentaPriceListServices",
                column: "Renta_Price_List_Service_Connections");

            migrationBuilder.CreateIndex(
                name: "IX_RentPriceLists_Rent_Price_List",
                table: "RentPriceLists",
                column: "Rent_Price_List");

            migrationBuilder.CreateIndex(
                name: "IX_RentPriceLists_Rent_a_Car_ID",
                table: "RentPriceLists",
                column: "Rent_a_Car_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Branche_ID",
                table: "Vehicles",
                column: "Branche_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_User_ID",
                table: "Vehicles",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Vehicle",
                table: "Vehicles",
                column: "Vehicle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AircraftConfigurations");

            migrationBuilder.DropTable(
                name: "AirlineDestinations");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FastReservations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "PriceListServices");

            migrationBuilder.DropTable(
                name: "RentaPriceListServices");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "RentPriceLists");

            migrationBuilder.DropTable(
                name: "RentServices");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "MyUsers");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "RentACars");
        }
    }
}
