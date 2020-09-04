using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a87c35a-88e2-4c73-98e7-ce7f260d8cc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0ead611-3222-42d2-a626-3d4f633670d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca462160-cca0-4c40-9aeb-7082b4006ec2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc69f2e0-be3d-4670-91d5-5a7f02110a27");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Branches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Branches",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bcada7b0-7090-4d68-8a5c-32062cba3a77", "18bf7d4f-ff89-49d7-b46f-858f381ba368", "Admin", "ADMIN" },
                    { "75bae19f-a013-488a-b00d-70cc4a150733", "5b972b0e-9f96-40a8-b4ce-1e34fbe2b60e", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "df90fe2d-2571-4d87-8785-505528326b9b", "d3e5d625-df89-4e4c-90f7-c2e4e1d7e2e4", "Service_Admin", "SERVICE_ADMIN" },
                    { "cd390278-f3b1-4aeb-8dd1-7c8ed5b0f4bb", "b53bc2bf-70ec-4cc2-95b1-b9957c006d6c", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75bae19f-a013-488a-b00d-70cc4a150733");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcada7b0-7090-4d68-8a5c-32062cba3a77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd390278-f3b1-4aeb-8dd1-7c8ed5b0f4bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df90fe2d-2571-4d87-8785-505528326b9b");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Branches");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a87c35a-88e2-4c73-98e7-ce7f260d8cc1", "1f80cebe-f9d7-4bbc-afed-bf0212752794", "Admin", "ADMIN" },
                    { "b0ead611-3222-42d2-a626-3d4f633670d9", "62532d75-267d-4163-a556-e444965678be", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "ca462160-cca0-4c40-9aeb-7082b4006ec2", "eecae168-166a-4422-807b-ee9fa242c676", "Service_Admin", "SERVICE_ADMIN" },
                    { "cc69f2e0-be3d-4670-91d5-5a7f02110a27", "710f9f9e-7911-4959-9e62-daee2a9f5934", "User", "USER" }
                });
        }
    }
}
