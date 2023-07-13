using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 13, 17, 4, 23, 511, DateTimeKind.Local).AddTicks(6049),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 13, 16, 48, 41, 697, DateTimeKind.Local).AddTicks(4217));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("e2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"), null, "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"), new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"), 0, "f9c08e96-7630-4145-97be-c3f89e465be5", new DateTime(2003, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "some-admin@gmail.com", true, "Tuân", "Trần", false, null, "some-admin@gmail.com", "admin", "AQAAAAIAAYagAAAAEOjh0hue30CyLyNzHaBSaIyqxUPk5p3vFjcw6Zapn12KpM25pj2Q9Qui6evtbcehfA==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 13, 17, 4, 23, 513, DateTimeKind.Local).AddTicks(2233));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("e2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"), new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2f0a1f9-7d0c-4f0b-b7e3-2b0b265c1f6e"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 13, 16, 48, 41, 697, DateTimeKind.Local).AddTicks(4217),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 13, 17, 4, 23, 511, DateTimeKind.Local).AddTicks(6049));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 13, 16, 48, 41, 699, DateTimeKind.Local).AddTicks(8607));
        }
    }
}
