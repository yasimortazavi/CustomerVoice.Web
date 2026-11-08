using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerVoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddComplaintReferenceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Complaints");

            migrationBuilder.AlterColumn<string>(
                name: "TrackingCode",
                table: "Complaints",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Complaints",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Complaints",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Complaints",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Complaints",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LandlineNumber",
                table: "Complaints",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Complaints",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Complaints",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SuggestedSolution",
                table: "Complaints",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComplaintAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintAttachments_Complaints_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DissatisfactionReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DissatisfactionReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintDissatisfactionReasons",
                columns: table => new
                {
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    DissatisfactionReasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintDissatisfactionReasons", x => new { x.ComplaintId, x.DissatisfactionReasonId });
                    table.ForeignKey(
                        name: "FK_ComplaintDissatisfactionReasons_Complaints_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintDissatisfactionReasons_DissatisfactionReasons_DissatisfactionReasonId",
                        column: x => x.DissatisfactionReasonId,
                        principalTable: "DissatisfactionReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ProjectId",
                table: "Complaints",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ServiceTypeId",
                table: "Complaints",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_TrackingCode",
                table: "Complaints",
                column: "TrackingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintAttachments_ComplaintId",
                table: "ComplaintAttachments",
                column: "ComplaintId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintDissatisfactionReasons_DissatisfactionReasonId",
                table: "ComplaintDissatisfactionReasons",
                column: "DissatisfactionReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DissatisfactionReasons_Title",
                table: "DissatisfactionReasons",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Title",
                table: "Projects",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_Title",
                table: "ServiceTypes",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Projects_ProjectId",
                table: "Complaints",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ServiceTypes_ServiceTypeId",
                table: "Complaints",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Projects_ProjectId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ServiceTypes_ServiceTypeId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintAttachments");

            migrationBuilder.DropTable(
                name: "ComplaintDissatisfactionReasons");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "DissatisfactionReasons");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ProjectId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ServiceTypeId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_TrackingCode",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "LandlineNumber",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "SuggestedSolution",
                table: "Complaints");

            migrationBuilder.AlterColumn<string>(
                name: "TrackingCode",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
