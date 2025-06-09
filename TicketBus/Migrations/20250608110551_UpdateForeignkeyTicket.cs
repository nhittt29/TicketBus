using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignkeyTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRoute",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdRoute",
                table: "Tickets",
                column: "IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_BusRoutes_IdRoute",
                table: "Tickets",
                column: "IdRoute",
                principalTable: "BusRoutes",
                principalColumn: "IdRoute");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_BusRoutes_IdRoute",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_IdRoute",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IdRoute",
                table: "Tickets");
        }
    }
}
