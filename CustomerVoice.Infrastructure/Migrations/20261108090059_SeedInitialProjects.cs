using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerVoice.Infrastructure.Migrations
{
    public partial class SeedInitialProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Title", "IsActive", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "پروژه احیای مستقیم", true, new DateTime(2026, 11, 8) },
                    { 2, "پروژه فولاد تکنیک", true, new DateTime(2026, 11, 8) },
                    { 3, "پروژه تولید", true, new DateTime(2026, 11, 8) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });
        }
    }
}