using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerVoice.Infrastructure.Migrations
{
    public partial class SeedInitialReferenceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "Id", "Title", "IsActive", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "مشاوره و راهنمایی", true, new DateTime(2026, 11, 8) },
                    { 2, "ثبت‌نام و تشکیل پرونده", true, new DateTime(2026, 11, 8) },
                    { 3, "ارائه خدمات تخصصی", true, new DateTime(2026, 11, 8) },
                    { 4, "پشتیبانی و پاسخگویی", true, new DateTime(2026, 11, 8) },
                    { 5, "پیگیری درخواست", true, new DateTime(2026, 11, 8) },
                    { 6, "امور مالی و پرداخت", true, new DateTime(2026, 11, 8) },
                    { 7, "خدمات فنی و اجرایی", true, new DateTime(2026, 11, 8) },
                    { 8, "سایر موارد", true, new DateTime(2026, 11, 8) }
                });

            migrationBuilder.InsertData(
                table: "DissatisfactionReasons",
                columns: new[] { "Id", "Title", "IsActive", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "تأخیر در ارائه خدمات", true, new DateTime(2026, 11, 8) },
                    { 2, "کیفیت نامناسب خدمات", true, new DateTime(2026, 11, 8) },
                    { 3, "عدم پاسخگویی مناسب", true, new DateTime(2026, 11, 8) },
                    { 4, "برخورد نامناسب کارکنان", true, new DateTime(2026, 11, 8) },
                    { 5, "عدم رعایت تعهدات", true, new DateTime(2026, 11, 8) },
                    { 6, "نقص در اطلاع‌رسانی", true, new DateTime(2026, 11, 8) },
                    { 7, "هزینه‌های نامتناسب", true, new DateTime(2026, 11, 8) },
                    { 8, "پیچیدگی فرایندها و مراحل اداری", true, new DateTime(2026, 11, 8) },
                    { 9, "عدم تحقق نتیجه مورد انتظار", true, new DateTime(2026, 11, 8) },
                    { 10, "سایر موارد", true, new DateTime(2026, 11, 8) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });

            migrationBuilder.DeleteData(
                table: "DissatisfactionReasons",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }
    }
}