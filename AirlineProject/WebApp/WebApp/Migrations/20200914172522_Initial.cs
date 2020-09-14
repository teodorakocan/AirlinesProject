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
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PromoDescription = table.Column<string>(nullable: true)
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
                name: "Discounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<int>(nullable: false),
                    Discount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.ID);
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
                    PhoneNumber = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    PictureURL = table.Column<string>(nullable: true),
                    IdToken = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false)
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
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PromoDescription = table.Column<string>(nullable: true)
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
                    AirlineID = table.Column<int>(nullable: false),
                    Price_List = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceLists_Airlines_AirlineID",
                        column: x => x.AirlineID,
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
                    AirlineID = table.Column<int>(nullable: false),
                    DestinationID = table.Column<int>(nullable: false),
                    AirlineDestinationConnections = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineDestinations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Airlines_AirlineDestinationConnections",
                        column: x => x.AirlineDestinationConnections,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Destinations_AirlineDestinationConnections",
                        column: x => x.AirlineDestinationConnections,
                        principalTable: "Destinations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirlineDestinations_Destinations_DestinationID",
                        column: x => x.DestinationID,
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
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Price2 = table.Column<double>(nullable: false),
                    FlightNumber = table.Column<string>(nullable: true),
                    NumberTransfer = table.Column<int>(nullable: false),
                    DestinationTransfer = table.Column<string>(nullable: true),
                    TravelLength = table.Column<TimeSpan>(nullable: false),
                    DestinationID = table.Column<int>(nullable: false),
                    Flight = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Flights_Destinations_DestinationID",
                        column: x => x.DestinationID,
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
                    RowNumber = table.Column<int>(nullable: false),
                    ColumnNumber = table.Column<int>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false),
                    Number_Of_Seats = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    AircraftConfiguration = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftConfigurations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AircraftConfigurations_MyUsers_AircraftConfiguration",
                        column: x => x.AircraftConfiguration,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AircraftConfigurations_MyUsers_UserID",
                        column: x => x.UserID,
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
                    StartDestination = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    SeatNumber = table.Column<int>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    AirlineID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    FastReservation = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastReservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FastReservations_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FastReservations_Airlines_FastReservation",
                        column: x => x.FastReservation,
                        principalTable: "Airlines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FastReservations_MyUsers_FastReservation",
                        column: x => x.FastReservation,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FastReservations_MyUsers_UserID",
                        column: x => x.UserID,
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
                    PhoneNumber = table.Column<string>(nullable: true),
                    Airline_ID = table.Column<int>(nullable: true),
                    RentACarID = table.Column<int>(nullable: true)
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
                        name: "FK_Admins_RentACars_RentACarID",
                        column: x => x.RentACarID,
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
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    NumberOfVehicle = table.Column<int>(nullable: false),
                    RentACarID = table.Column<int>(nullable: false),
                    Branch = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Branches_RentACars_Branch",
                        column: x => x.Branch,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_RentACars_RentACarID",
                        column: x => x.RentACarID,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentACarRaiting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<int>(nullable: false),
                    RentACarID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RentACarRaitings = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentACarRaiting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RentACarRaiting_RentACars_RentACarID",
                        column: x => x.RentACarID,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentACarRaiting_MyUsers_RentACarRaitings",
                        column: x => x.RentACarRaitings,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentACarRaiting_RentACars_RentACarRaitings",
                        column: x => x.RentACarRaitings,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentACarRaiting_MyUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentPriceLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentACarID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentPriceLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RentPriceLists_RentACars_RentACarID",
                        column: x => x.RentACarID,
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
                    PriceListID = table.Column<int>(nullable: false),
                    ServiceID = table.Column<int>(nullable: false),
                    PriceListServiceConnections = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListServices_PriceLists_PriceListID",
                        column: x => x.PriceListID,
                        principalTable: "PriceLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceListServices_PriceLists_PriceListServiceConnections",
                        column: x => x.PriceListServiceConnections,
                        principalTable: "PriceLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListServices_Services_PriceListServiceConnections",
                        column: x => x.PriceListServiceConnections,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    UserID = table.Column<int>(nullable: true),
                    AdminID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MyUsers_UserID",
                        column: x => x.UserID,
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
                    NumberOfSeats = table.Column<int>(nullable: false),
                    Class = table.Column<string>(nullable: true),
                    Reserved = table.Column<bool>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    BranchID = table.Column<int>(nullable: false),
                    Vehicle = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicles_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Branches_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentaPriceListServices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentPriceListID = table.Column<int>(nullable: false),
                    RentServiceID = table.Column<int>(nullable: false),
                    RentaPriceListServiceConnections = table.Column<int>(nullable: true),
                    Renta_Price_List_Service_Connections = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentaPriceListServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RentaPriceListServices_RentPriceLists_RentPriceListID",
                        column: x => x.RentPriceListID,
                        principalTable: "RentPriceLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentaPriceListServices_RentServices_RentServiceID",
                        column: x => x.RentServiceID,
                        principalTable: "RentServices",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentaPriceListServices_RentPriceLists_RentaPriceListServiceConnections",
                        column: x => x.RentaPriceListServiceConnections,
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

            migrationBuilder.CreateTable(
                name: "CarReservations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationFrom = table.Column<DateTime>(nullable: false),
                    ReservationTo = table.Column<DateTime>(nullable: false),
                    VehicleID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    CarReservations = table.Column<int>(nullable: true),
                    RentACarID = table.Column<int>(nullable: false),
                    Reservations = table.Column<int>(nullable: true),
                    BranchID = table.Column<int>(nullable: false),
                    Resrvations = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarReservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CarReservations_MyUsers_CarReservations",
                        column: x => x.CarReservations,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarReservations_Vehicles_CarReservations",
                        column: x => x.CarReservations,
                        principalTable: "Vehicles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarReservations_RentACars_Reservations",
                        column: x => x.Reservations,
                        principalTable: "RentACars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarReservations_Branches_Resrvations",
                        column: x => x.Resrvations,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleRaiting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<int>(nullable: false),
                    VehicleID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    VehicleRaitings = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRaiting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VehicleRaiting_MyUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleRaiting_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleRaiting_MyUsers_VehicleRaitings",
                        column: x => x.VehicleRaitings,
                        principalTable: "MyUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleRaiting_Vehicles_VehicleRaitings",
                        column: x => x.VehicleRaitings,
                        principalTable: "Vehicles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69f20f0f-8dae-4c43-bc20-312d083b39d1", "b9800f29-ed61-4e34-aa34-0f99d67fe72c", "Admin", "ADMIN" },
                    { "4181f231-0493-440c-b420-d8e1bf46db86", "281140a7-93b8-41d1-ab05-3d476422e371", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "a426403f-f569-4e7e-bee6-20127d0ec7cd", "664e1b23-6e81-484b-9995-5b3d970806c6", "Service_Admin", "SERVICE_ADMIN" },
                    { "92276a4c-68c3-4b60-aff5-983c35a6c842", "289d570c-4aec-4f52-9add-4e930f3da0fc", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Airline_ID",
                table: "Admins",
                column: "Airline_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_RentACarID",
                table: "Admins",
                column: "RentACarID");

            migrationBuilder.CreateIndex(
                name: "IX_AircraftConfigurations_AircraftConfiguration",
                table: "AircraftConfigurations",
                column: "AircraftConfiguration");

            migrationBuilder.CreateIndex(
                name: "IX_AircraftConfigurations_UserID",
                table: "AircraftConfigurations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineDestinations_AirlineDestinationConnections",
                table: "AirlineDestinations",
                column: "AirlineDestinationConnections");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineDestinations_AirlineID",
                table: "AirlineDestinations",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineDestinations_DestinationID",
                table: "AirlineDestinations",
                column: "DestinationID");

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
                name: "IX_AspNetUsers_AdminID",
                table: "AspNetUsers",
                column: "AdminID");

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
                name: "IX_AspNetUsers_UserID",
                table: "AspNetUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Branch",
                table: "Branches",
                column: "Branch");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RentACarID",
                table: "Branches",
                column: "RentACarID");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_CarReservations",
                table: "CarReservations",
                column: "CarReservations");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_Reservations",
                table: "CarReservations",
                column: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_Resrvations",
                table: "CarReservations",
                column: "Resrvations");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservations_AirlineID",
                table: "FastReservations",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservations_FastReservation",
                table: "FastReservations",
                column: "FastReservation");

            migrationBuilder.CreateIndex(
                name: "IX_FastReservations_UserID",
                table: "FastReservations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationID",
                table: "Flights",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Flight",
                table: "Flights",
                column: "Flight");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_AirlineID",
                table: "PriceLists",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_Price_List",
                table: "PriceLists",
                column: "Price_List");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListServices_PriceListID",
                table: "PriceListServices",
                column: "PriceListID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListServices_PriceListServiceConnections",
                table: "PriceListServices",
                column: "PriceListServiceConnections");

            migrationBuilder.CreateIndex(
                name: "IX_RentACarRaiting_RentACarID",
                table: "RentACarRaiting",
                column: "RentACarID");

            migrationBuilder.CreateIndex(
                name: "IX_RentACarRaiting_RentACarRaitings",
                table: "RentACarRaiting",
                column: "RentACarRaitings");

            migrationBuilder.CreateIndex(
                name: "IX_RentACarRaiting_UserID",
                table: "RentACarRaiting",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RentaPriceListServices_RentPriceListID",
                table: "RentaPriceListServices",
                column: "RentPriceListID");

            migrationBuilder.CreateIndex(
                name: "IX_RentaPriceListServices_RentServiceID",
                table: "RentaPriceListServices",
                column: "RentServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_RentaPriceListServices_RentaPriceListServiceConnections",
                table: "RentaPriceListServices",
                column: "RentaPriceListServiceConnections");

            migrationBuilder.CreateIndex(
                name: "IX_RentaPriceListServices_Renta_Price_List_Service_Connections",
                table: "RentaPriceListServices",
                column: "Renta_Price_List_Service_Connections");

            migrationBuilder.CreateIndex(
                name: "IX_RentPriceLists_RentACarID",
                table: "RentPriceLists",
                column: "RentACarID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRaiting_UserID",
                table: "VehicleRaiting",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRaiting_VehicleID",
                table: "VehicleRaiting",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRaiting_VehicleRaitings",
                table: "VehicleRaiting",
                column: "VehicleRaitings");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_BranchID",
                table: "Vehicles",
                column: "BranchID");

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
                name: "CarReservations");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "FastReservations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "PriceListServices");

            migrationBuilder.DropTable(
                name: "RentACarRaiting");

            migrationBuilder.DropTable(
                name: "RentaPriceListServices");

            migrationBuilder.DropTable(
                name: "VehicleRaiting");

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
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "MyUsers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "RentACars");
        }
    }
}
