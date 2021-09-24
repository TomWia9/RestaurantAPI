using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangeIdsNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Addresses_Restaurants_TRestaurant",
                "Addresses");

            migrationBuilder.DropForeignKey(
                "FK_Dishes_Restaurants_RestaurantTRestaurant",
                "Dishes");

            migrationBuilder.DropIndex(
                "IX_Dishes_RestaurantTRestaurant",
                "Dishes");

            migrationBuilder.DropColumn(
                "RestaurantTRestaurant",
                "Dishes");

            migrationBuilder.RenameColumn(
                "TRestaurant",
                "Restaurants",
                "Id");

            migrationBuilder.RenameColumn(
                "FRestaurant",
                "Dishes",
                "RestaurantId");

            migrationBuilder.RenameColumn(
                "TDish",
                "Dishes",
                "Id");

            migrationBuilder.RenameColumn(
                "TRestaurant",
                "Addresses",
                "RestaurantId");

            migrationBuilder.RenameColumn(
                "TAddress",
                "Addresses",
                "Id");

            migrationBuilder.RenameIndex(
                "IX_Addresses_TRestaurant",
                table: "Addresses",
                newName: "IX_Addresses_RestaurantId");

            migrationBuilder.CreateIndex(
                "IX_Dishes_RestaurantId",
                "Dishes",
                "RestaurantId");

            migrationBuilder.AddForeignKey(
                "FK_Addresses_Restaurants_RestaurantId",
                "Addresses",
                "RestaurantId",
                "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Dishes_Restaurants_RestaurantId",
                "Dishes",
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

            migrationBuilder.DropForeignKey(
                "FK_Dishes_Restaurants_RestaurantId",
                "Dishes");

            migrationBuilder.DropIndex(
                "IX_Dishes_RestaurantId",
                "Dishes");

            migrationBuilder.RenameColumn(
                "Id",
                "Restaurants",
                "TRestaurant");

            migrationBuilder.RenameColumn(
                "RestaurantId",
                "Dishes",
                "FRestaurant");

            migrationBuilder.RenameColumn(
                "Id",
                "Dishes",
                "TDish");

            migrationBuilder.RenameColumn(
                "RestaurantId",
                "Addresses",
                "TRestaurant");

            migrationBuilder.RenameColumn(
                "Id",
                "Addresses",
                "TAddress");

            migrationBuilder.RenameIndex(
                "IX_Addresses_RestaurantId",
                table: "Addresses",
                newName: "IX_Addresses_TRestaurant");

            migrationBuilder.AddColumn<Guid>(
                "RestaurantTRestaurant",
                "Dishes",
                "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Dishes_RestaurantTRestaurant",
                "Dishes",
                "RestaurantTRestaurant");

            migrationBuilder.AddForeignKey(
                "FK_Addresses_Restaurants_TRestaurant",
                "Addresses",
                "TRestaurant",
                "Restaurants",
                principalColumn: "TRestaurant",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Dishes_Restaurants_RestaurantTRestaurant",
                "Dishes",
                "RestaurantTRestaurant",
                "Restaurants",
                principalColumn: "TRestaurant",
                onDelete: ReferentialAction.Cascade);
        }
    }
}