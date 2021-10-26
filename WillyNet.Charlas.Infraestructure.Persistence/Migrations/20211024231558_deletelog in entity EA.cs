using Microsoft.EntityFrameworkCore.Migrations;

namespace WillyNet.Charlas.Infraestructure.Persistence.Migrations
{
    public partial class deleteloginentityEA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeleteLog",
                table: "EstadoAsistencia",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteLog",
                table: "EstadoAsistencia");
        }
    }
}
