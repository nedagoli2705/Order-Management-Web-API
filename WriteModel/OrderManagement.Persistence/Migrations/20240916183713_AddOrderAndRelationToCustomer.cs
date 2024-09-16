using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndRelationToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                schema: "CustomerContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "CustomerContext",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "CustomerContext",
                table: "Order",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order",
                schema: "CustomerContext");
        }
    }
}
