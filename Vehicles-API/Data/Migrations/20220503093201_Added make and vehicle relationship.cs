using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles_API.Data.Migrations
{
  public partial class Addedmakeandvehiclerelationship : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Make",
          table: "Vehicles");

      migrationBuilder.AddColumn<int>(
          name: "MakeId",
          table: "Vehicles",
          type: "INTEGER",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.CreateTable(
          name: "Manufacturers",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Manufacturers", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Vehicles_MakeId",
          table: "Vehicles",
          column: "MakeId");

      migrationBuilder.AddForeignKey(
          name: "FK_Vehicles_Manufacturers_MakeId",
          table: "Vehicles",
          column: "MakeId",
          principalTable: "Manufacturers",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Vehicles_Manufacturers_MakeId",
          table: "Vehicles");

      migrationBuilder.DropTable(
          name: "Manufacturers");

      migrationBuilder.DropIndex(
          name: "IX_Vehicles_MakeId",
          table: "Vehicles");

      migrationBuilder.DropColumn(
          name: "MakeId",
          table: "Vehicles");

      migrationBuilder.AddColumn<string>(
          name: "Make",
          table: "Vehicles",
          type: "TEXT",
          nullable: true);
    }
  }
}
