using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quizz.Migrations
{
    public partial class new_alltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzs",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 255, nullable: false),
                    PassWord = table.Column<string>(type: "char(255)", nullable: false),
                    PassWordHash = table.Column<string>(type: "char(64)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 255, nullable: false),
                    NameHash = table.Column<string>(type: "char(64)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzs_PassWordHash",
                table: "Quizzs",
                column: "PassWordHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_NameHash",
                table: "Topics",
                column: "NameHash",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quizzs");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
