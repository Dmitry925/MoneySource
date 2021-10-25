using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneySource.Infrastructure.Persistence.Migrations
{
    public partial class configs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Sources_SourceId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Sources_SourceId",
                table: "Transactions",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Sources_SourceId",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Sources_SourceId",
                table: "Transactions",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
