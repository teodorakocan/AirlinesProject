using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68e433ae-059c-45e3-bca4-96fb849d269a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89026a3a-1e27-4d98-8bb3-8d1508a807e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf6b2ca2-8e6e-492b-a008-68c15c3cb83e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da6119c0-9923-4725-9f83-b9838ecd2807");

            migrationBuilder.DropColumn(
                name: "Desription",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f287d519-dc29-4f92-9d50-a2f9d163ab45", "18a24d8d-f625-4bd5-bd0f-e012a568048f", "Admin", "ADMIN" },
                    { "8fce42e5-12d8-4b0c-99dd-197db3c38979", "325d4e0a-149d-44b7-9a88-c01c2e662f2e", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "c8f48019-233b-4d3a-8845-3d00b64f2020", "98d76356-3109-4c9f-a78c-a525ff38b58d", "Service_Admin", "SERVICE_ADMIN" },
                    { "8eb0c5a0-ec59-48d5-9d5e-30c7b948d565", "ca298062-8fda-4386-84f8-7cf50ad0fec4", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eb0c5a0-ec59-48d5-9d5e-30c7b948d565");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fce42e5-12d8-4b0c-99dd-197db3c38979");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8f48019-233b-4d3a-8845-3d00b64f2020");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f287d519-dc29-4f92-9d50-a2f9d163ab45");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "Desription",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89026a3a-1e27-4d98-8bb3-8d1508a807e9", "26cb7fcf-42a3-4ae5-8035-b774741eb186", "Admin", "ADMIN" },
                    { "68e433ae-059c-45e3-bca4-96fb849d269a", "b28de044-7616-4767-953d-03faa79d7bc0", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "bf6b2ca2-8e6e-492b-a008-68c15c3cb83e", "2f053315-258a-472c-b42b-717826f64cc6", "Service_Admin", "SERVICE_ADMIN" },
                    { "da6119c0-9923-4725-9f83-b9838ecd2807", "051a5fc2-cb53-4057-be55-9ae0f8c737de", "User", "USER" }
                });
        }
    }
}
