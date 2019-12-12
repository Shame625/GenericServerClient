using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseCore.Migrations
{
    public partial class PlayerDbo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<long>(nullable: false, defaultValue: 1),
                    Class = table.Column<int>(nullable: false),
                    Pos_X = table.Column<float>(nullable: false, defaultValue: 0),
                    Pos_Y = table.Column<float>(nullable: false, defaultValue: 0),
                    Pos_Z = table.Column<float>(nullable: false, defaultValue: 0),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
