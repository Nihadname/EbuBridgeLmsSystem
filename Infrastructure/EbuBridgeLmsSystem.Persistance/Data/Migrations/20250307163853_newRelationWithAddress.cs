using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class newRelationWithAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_addresses_CountryId",
                table: "addresses",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_Countries_CountryId",
                table: "addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_Countries_CountryId",
                table: "addresses");

            migrationBuilder.DropIndex(
                name: "IX_addresses_CountryId",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "addresses");
        }
    }
}
