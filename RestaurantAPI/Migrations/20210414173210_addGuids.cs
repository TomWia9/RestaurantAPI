using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantAPI.Migrations
{
    public partial class AddGuids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TRestaurant",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FRestaurant",
                table: "Dishes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TDish",
                table: "Dishes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TAddress",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TRestaurant",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TRestaurant",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "FRestaurant",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "TDish",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "TAddress",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TRestaurant",
                table: "Addresses");
        }
    }
}
