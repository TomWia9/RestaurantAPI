using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantAPI.Migrations
{
    public partial class ChangeIdsNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Restaurants_TRestaurant",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantTRestaurant",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_RestaurantTRestaurant",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "RestaurantTRestaurant",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "TRestaurant",
                table: "Restaurants",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FRestaurant",
                table: "Dishes",
                newName: "RestaurantId");

            migrationBuilder.RenameColumn(
                name: "TDish",
                table: "Dishes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TRestaurant",
                table: "Addresses",
                newName: "RestaurantId");

            migrationBuilder.RenameColumn(
                name: "TAddress",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_TRestaurant",
                table: "Addresses",
                newName: "IX_Addresses_RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Restaurants_RestaurantId",
                table: "Addresses",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Restaurants_RestaurantId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Restaurants",
                newName: "TRestaurant");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Dishes",
                newName: "FRestaurant");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dishes",
                newName: "TDish");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Addresses",
                newName: "TRestaurant");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "TAddress");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_RestaurantId",
                table: "Addresses",
                newName: "IX_Addresses_TRestaurant");

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantTRestaurant",
                table: "Dishes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_RestaurantTRestaurant",
                table: "Dishes",
                column: "RestaurantTRestaurant");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Restaurants_TRestaurant",
                table: "Addresses",
                column: "TRestaurant",
                principalTable: "Restaurants",
                principalColumn: "TRestaurant",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantTRestaurant",
                table: "Dishes",
                column: "RestaurantTRestaurant",
                principalTable: "Restaurants",
                principalColumn: "TRestaurant",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
