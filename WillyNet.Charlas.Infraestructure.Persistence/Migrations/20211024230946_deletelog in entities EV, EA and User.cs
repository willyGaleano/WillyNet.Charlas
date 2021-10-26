using Microsoft.EntityFrameworkCore.Migrations;

namespace WillyNet.Charlas.Infraestructure.Persistence.Migrations
{
    public partial class deleteloginentitiesEVEAandUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeleteLog",
                table: "EstadoEvento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteLog",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteLog",
                table: "EstadoEvento");

            migrationBuilder.DropColumn(
                name: "DeleteLog",
                table: "AspNetUsers");
        }
    }
}
