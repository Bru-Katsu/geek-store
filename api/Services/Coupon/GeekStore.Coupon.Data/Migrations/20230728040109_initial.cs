using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Coupon.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "id", "CouponCode", "DiscountAmount" },
                values: new object[,]
                {
                    { new Guid("846fe6e9-cdc0-4378-9349-0ee7f6ee0b2a"), "GEEK10", 10m },
                    { new Guid("c309eadf-63f9-4f79-90b5-f2a61d6d5a64"), "GEEK25", 25m },
                    { new Guid("cb105c8d-c7d3-4c41-b6e3-e8a1492572b1"), "GEEK20", 20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
