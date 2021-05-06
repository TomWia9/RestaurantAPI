using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class removeIntIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Addresses_Restaurants_RestaurantId",
                "Addresses");

            migrationBuilder.DropForeignKey(
                "FK_Dishes_Restaurants_RestaurantId",
                "Dishes");

            migrationBuilder.DropPrimaryKey(
                "PK_Restaurants",
                "Restaurants");

            migrationBuilder.DropPrimaryKey(
                "PK_Dishes",
                "Dishes");

            migrationBuilder.DropIndex(
                "IX_Dishes_RestaurantId",
                "Dishes");

            migrationBuilder.DropPrimaryKey(
                "PK_Addresses",
                "Addresses");

            migrationBuilder.DropIndex(
                "IX_Addresses_RestaurantId",
                "Addresses");

            migrationBuilder.DropColumn(
                "Id",
                "Restaurants");

            migrationBuilder.DropColumn(
                "Id",
                "Dishes");

            migrationBuilder.DropColumn(
                "RestaurantId",
                "Dishes");

            migrationBuilder.DropColumn(
                "Id",
                "Addresses");

            migrationBuilder.DropColumn(
                "RestaurantId",
                "Addresses");

            migrationBuilder.AddColumn<Guid>(
                "RestaurantTRestaurant",
                "Dishes",
                "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                "PK_Restaurants",
                "Restaurants",
                "TRestaurant");

            migrationBuilder.AddPrimaryKey(
                "PK_Dishes",
                "Dishes",
                "TDish");

            migrationBuilder.AddPrimaryKey(
                "PK_Addresses",
                "Addresses",
                "TAddress");

            migrationBuilder.CreateIndex(
                "IX_Dishes_RestaurantTRestaurant",
                "Dishes",
                "RestaurantTRestaurant");

            migrationBuilder.CreateIndex(
                "IX_Addresses_TRestaurant",
                "Addresses",
                "TRestaurant",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Addresses_Restaurants_TRestaurant",
                "Addresses");

            migrationBuilder.DropForeignKey(
                "FK_Dishes_Restaurants_RestaurantTRestaurant",
                "Dishes");

            migrationBuilder.DropPrimaryKey(
                "PK_Restaurants",
                "Restaurants");

            migrationBuilder.DropPrimaryKey(
                "PK_Dishes",
                "Dishes");

            migrationBuilder.DropIndex(
                "IX_Dishes_RestaurantTRestaurant",
                "Dishes");

            migrationBuilder.DropPrimaryKey(
                "PK_Addresses",
                "Addresses");

            migrationBuilder.DropIndex(
                "IX_Addresses_TRestaurant",
                "Addresses");

            migrationBuilder.DropColumn(
                "RestaurantTRestaurant",
                "Dishes");

            migrationBuilder.AddColumn<int>(
                    "Id",
                    "Restaurants",
                    "int",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                    "Id",
                    "Dishes",
                    "int",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                "RestaurantId",
                "Dishes",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                    "Id",
                    "Addresses",
                    "int",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                "RestaurantId",
                "Addresses",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                "PK_Restaurants",
                "Restaurants",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_Dishes",
                "Dishes",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_Addresses",
                "Addresses",
                "Id");

            migrationBuilder.CreateIndex(
                "IX_Dishes_RestaurantId",
                "Dishes",
                "RestaurantId");

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

            migrationBuilder.AddForeignKey(
                "FK_Dishes_Restaurants_RestaurantId",
                "Dishes",
                "RestaurantId",
                "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}