using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class HomeData_UpdateLinkImageSlide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "285ebd45-2b54-442f-bb20-8ae86fb4557e", "AQAAAAIAAYagAAAAECxczwIVfQ3tmcIKm8aOfhhvAdh+d26x7RfmYbyUy5bUt0xemptCxwJRSL6yHrhw8Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 8, 22, 22, 36, 48, 250, DateTimeKind.Local).AddTicks(8364));

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "themes/images/carousel/2.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "themes/images/carousel/3.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "themes/images/carousel/4.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "themes/images/carousel/5.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "themes/images/carousel/6.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dd2cdfd6-5cfd-46aa-8317-55b66b7a113d", "AQAAAAIAAYagAAAAEBc+fmuvJh6ueWB9fmpU30LEUJizx8f5pPwpVeu6iwgVUaTyU7EyOm2KFSfDmR0sOQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 8, 22, 22, 29, 26, 611, DateTimeKind.Local).AddTicks(7267));

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "themes/images/carousel/1.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "themes/images/carousel/1.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "themes/images/carousel/1.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "themes/images/carousel/1.png");

            migrationBuilder.UpdateData(
                table: "Slides",
                keyColumn: "Id",
                keyValue: 6,
                column: "Image",
                value: "themes/images/carousel/1.png");
        }
    }
}
