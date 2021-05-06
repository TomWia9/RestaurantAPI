using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Addresses",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>("nvarchar(max)", nullable: true),
                    Street = table.Column<string>("nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Addresses", x => x.Id); });

            migrationBuilder.CreateTable(
                "Restaurants",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    Description = table.Column<string>("nvarchar(max)", nullable: true),
                    Category = table.Column<string>("nvarchar(max)", nullable: true),
                    HasDelivery = table.Column<bool>("bit", nullable: false),
                    ContactEmail = table.Column<string>("nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>("nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        "FK_Restaurants_Addresses_AddressId",
                        x => x.AddressId,
                        "Addresses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Dishes",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    Description = table.Column<string>("nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>("decimal(18,4)", nullable: false),
                    RestaurantId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        "FK_Dishes_Restaurants_RestaurantId",
                        x => x.RestaurantId,
                        "Restaurants",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Dishes_RestaurantId",
                "Dishes",
                "RestaurantId");

            migrationBuilder.CreateIndex(
                "IX_Restaurants_AddressId",
                "Restaurants",
                "AddressId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Dishes");

            migrationBuilder.DropTable(
                "Restaurants");

            migrationBuilder.DropTable(
                "Addresses");
        }
    }
}