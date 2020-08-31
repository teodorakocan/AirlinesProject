using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Class",
                table: "Vehicles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c496d108-6396-4083-8665-507172c82458", "b07a737f-bbbe-4c07-8b11-21193f3ff431", "Admin", "ADMIN" },
                    { "037387a6-3fe4-426a-bf8a-a53b1478ef1a", "e72e9662-28e7-49f2-88d6-3418d19b84d9", "Airline_Admin", "AIRLINE_ADMIN" },
                    { "7e8df844-2822-4644-8d12-6c64d834758e", "8f5eb12d-cf0c-438c-8c95-e504d6f37b14", "Service_Admin", "SERVICE_ADMIN" },
                    { "9badddd1-cf17-4854-a7e8-4cce710cd777", "52b5bc84-d6e2-4836-a05f-b79b618a92a9", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "037387a6-3fe4-426a-bf8a-a53b1478ef1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e8df844-2822-4644-8d12-6c64d834758e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9badddd1-cf17-4854-a7e8-4cce710cd777");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c496d108-6396-4083-8665-507172c82458");

            migrationBuilder.AlterColumn<int>(
                name: "Class",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
