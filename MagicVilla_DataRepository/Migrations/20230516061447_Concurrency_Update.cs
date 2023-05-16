using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_DataRepository.Migrations
{
    /// <inheritdoc />
    public partial class Concurrency_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Version",
                table: "Villas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Villas");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedData" },
                values: new object[] { 1, "", new DateTime(2023, 5, 16, 10, 37, 13, 745, DateTimeKind.Local).AddTicks(3245), "Fusce 11 tincidunt maximus leo", "https://th.bing.com/th/id/OIP.995V-2MsmR9OdZqidvyDiAHaE1?pid=ImgDet&rs=1", "Royal Villa", 5, 200.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
