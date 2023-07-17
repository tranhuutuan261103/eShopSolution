using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileLengthType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImage",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "88e4c605-b74d-4829-a2e0-ab62fd169623", "AQAAAAIAAYagAAAAEJrfokiDaRop9FyOP+n6jnL33LMEfPfCGl4fbQ5wi4HIuYy6TIo/1HUCpVwuk5+TdA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 16, 21, 59, 17, 586, DateTimeKind.Local).AddTicks(9592));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImage",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a38c2d5e-f0d0-4758-b543-fef2264e0a64", "AQAAAAIAAYagAAAAEL9GBo8YvY/HA2q4HRyzNqxPk4zD8oDQmFQR8iEh6EGWAraEGZKl7nlAGWPXM/IklQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 13, 22, 44, 29, 858, DateTimeKind.Local).AddTicks(1344));
        }
    }
}
