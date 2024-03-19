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
            migrationBuilder.DropForeignKey(
                name: "FK_threads_AspNetUsers_UserId",
                table: "threads");

            migrationBuilder.DropIndex(
                name: "IX_threads_UserId",
                table: "threads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "threads");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "threads",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "threads",
                columns: new[] { "Id", "created_at", "content" },
                values: new object[,]
                {
                    { "1", new DateTime(2024, 3, 19, 18, 7, 18, 348, DateTimeKind.Local).AddTicks(4480), "0f058217-784c-4483-ad7d-9d4e01a81933" },
                    { "2", new DateTime(2024, 3, 19, 18, 7, 18, 348, DateTimeKind.Local).AddTicks(4488), "2450d037-6235-498c-b1e7-ceaf8cafb53a" },
                    { "3", new DateTime(2024, 3, 19, 18, 7, 18, 348, DateTimeKind.Local).AddTicks(4493), "cdf987f1-81df-4de6-93eb-6182e8f4031a" }
                });

            migrationBuilder.InsertData(
                table: "feedbacks",
                columns: new[] { "Id", "content", "created_at", "score", "ThreadId", "UserId" },
                values: new object[,]
                {
                    { "1111", "TestFeedback1", new DateTime(2024, 3, 19, 18, 7, 18, 348, DateTimeKind.Local).AddTicks(4787), 1, "1", "0f058217-784c-4483-ad7d-9d4e01a81933" },
                    { "2222", "TestFeedback2", new DateTime(2024, 3, 19, 18, 7, 18, 348, DateTimeKind.Local).AddTicks(4797), 10, "2", "2450d037-6235-498c-b1e7-ceaf8cafb53a" },
                    { "3333", "TestFeedback3", new DateTime(2024, 3, 19, 18, 7, 18, 348, DateTimeKind.Local).AddTicks(4803), 9, "3", "cdf987f1-81df-4de6-93eb-6182e8f4031a" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_threads_content",
                table: "threads",
                column: "content");

            migrationBuilder.AddForeignKey(
                name: "FK_threads_AspNetUsers_content",
                table: "threads",
                column: "content",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_threads_AspNetUsers_content",
                table: "threads");

            migrationBuilder.DropIndex(
                name: "IX_threads_content",
                table: "threads");

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

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "threads",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "threads",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_threads_UserId",
                table: "threads",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_threads_AspNetUsers_UserId",
                table: "threads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
