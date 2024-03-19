using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IntelliViews.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "threads",
                columns: new[] { "Id", "created_at", "content" },
                values: new object[,]
                {
                    { "1", new DateTime(2024, 3, 19, 22, 21, 12, 85, DateTimeKind.Local).AddTicks(808), "0f058217-784c-4483-ad7d-9d4e01a81933" },
                    { "2", new DateTime(2024, 3, 19, 22, 21, 12, 85, DateTimeKind.Local).AddTicks(816), "2450d037-6235-498c-b1e7-ceaf8cafb53a" },
                    { "3", new DateTime(2024, 3, 19, 22, 21, 12, 85, DateTimeKind.Local).AddTicks(822), "cdf987f1-81df-4de6-93eb-6182e8f4031a" }
                });

            migrationBuilder.InsertData(
                table: "feedbacks",
                columns: new[] { "Id", "content", "created_at", "score", "ThreadId", "UserId" },
                values: new object[,]
                {
                    { "1111", "TestFeedback1", new DateTime(2024, 3, 19, 22, 21, 12, 85, DateTimeKind.Local).AddTicks(1072), 1, "1", "0f058217-784c-4483-ad7d-9d4e01a81933" },
                    { "2222", "TestFeedback2", new DateTime(2024, 3, 19, 22, 21, 12, 85, DateTimeKind.Local).AddTicks(1084), 10, "2", "2450d037-6235-498c-b1e7-ceaf8cafb53a" },
                    { "3333", "TestFeedback3", new DateTime(2024, 3, 19, 22, 21, 12, 85, DateTimeKind.Local).AddTicks(1090), 9, "3", "cdf987f1-81df-4de6-93eb-6182e8f4031a" }
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
