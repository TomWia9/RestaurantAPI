using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class fixRelationBetweenRestaurantAndAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Restaurants_Addresses_AddressId",
                "Restaurants");

            migrationBuilder.DropIndex(
                "IX_Restaurants_AddressId",
                "Restaurants");

            migrationBuilder.DropColumn(
                "AddressId",
                "Restaurants");

            migrationBuilder.AddColumn<int>(
                "RestaurantId",
                "Addresses",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_Addresses_RestaurantId",
                "Addresses",
                "RestaurantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Addresses_Restaurants_RestaurantId",
                "Addresses",
                "RestaurantId",
                "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Addresses_Restaurants_RestaurantId",
                "Addresses");

            migrationBuilder.DropIndex(
                "IX_Addresses_RestaurantId",
                "Addresses");

            migrationBuilder.DropColumn(
                "RestaurantId",
                "Addresses");

            migrationBuilder.AddColumn<int>(
                "AddressId",
                "Restaurants",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_Restaurants_AddressId",
                "Restaurants",
                "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Restaurants_Addresses_AddressId",
                "Restaurants",
                "AddressId",
                "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}