using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IntelliViews.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecMigration666 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "threads",
                columns: new[] { "Id", "content", "created_at", "UserId" },
                values: new object[,]
                {
                    { "1", "Test111", new DateTime(2024, 3, 19, 15, 16, 56, 767, DateTimeKind.Local).AddTicks(2341), "88adc46e-ba2d-4941-9175-e9e041a61d03" },
                    { "2", "Test222", new DateTime(2024, 3, 19, 15, 16, 56, 767, DateTimeKind.Local).AddTicks(2346), "c4505e94-21ac-47ae-84db-722ce907ad3c" },
                    { "3", "Test333", new DateTime(2024, 3, 19, 15, 16, 56, 767, DateTimeKind.Local).AddTicks(2350), "f245ef4c-683c-4f95-aa3e-dd6202109f0a" }
                });

            migrationBuilder.InsertData(
                table: "feedbacks",
                columns: new[] { "Id", "content", "created_at", "score", "ThreadId", "UserId" },
                values: new object[,]
                {
                    { "1111", "TestFeedback1", new DateTime(2024, 3, 19, 15, 16, 56, 767, DateTimeKind.Local).AddTicks(2474), 1, "1", "88adc46e-ba2d-4941-9175-e9e041a61d03" },
                    { "2222", "TestFeedback2", new DateTime(2024, 3, 19, 15, 16, 56, 767, DateTimeKind.Local).AddTicks(2481), 10, "2", "c4505e94-21ac-47ae-84db-722ce907ad3c" },
                    { "3333", "TestFeedback3", new DateTime(2024, 3, 19, 15, 16, 56, 767, DateTimeKind.Local).AddTicks(2485), 9, "3", "f245ef4c-683c-4f95-aa3e-dd6202109f0a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "feedbacks",
                keyColumn: "Id",
                keyValue: "1111");

            migrationBuilder.DeleteData(
                table: "feedbacks",
                keyColumn: "Id",
                keyValue: "2222");

            migrationBuilder.DeleteData(
                table: "feedbacks",
                keyColumn: "Id",
                keyValue: "3333");

            migrationBuilder.DeleteData(
                table: "threads",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "threads",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "threads",
                keyColumn: "Id",
                keyValue: "3");
        }
    }
}
