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
                keyValue: "5b51c132-73b8-4a39-8e33-957dce6b3a11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63db630a-7b1b-4793-ac83-55c31ab727a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa387b78-8df9-4fd1-8404-3fa989611a6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd6aa222-e047-4978-ab8f-e9d1cf57c801");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Branches",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Name",
                table: "Branches");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b51c132-73b8-4a39-8e33-957dce6b3a11", "7db56da8-f3f1-4e0d-b7c7-ea7b9cc90776", "Admin", "ADMIN" },
                    { "aa387b78-8df9-4fd1-8404-3fa989611a6e", "3bd76808-5bb3-4569-9f41-5493706a693e", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "cd6aa222-e047-4978-ab8f-e9d1cf57c801", "6ec06093-efe0-4c07-ac80-f0629c792f34", "Service_Admin", "SERVICE_ADMIN" },
                    { "63db630a-7b1b-4793-ac83-55c31ab727a0", "b49c6ae7-7b31-45bf-964d-749c7a05b768", "User", "USER" }
                });
        }
    }
}
