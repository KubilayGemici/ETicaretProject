using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BilgeAdamBitirmeProjesi.Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 250, nullable: true),
                    Adress = table.Column<string>(maxLength: 250, nullable: true),
                    Number = table.Column<string>(maxLength: 12, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 12, nullable: false),
                    LastLogin = table.Column<DateTime>(maxLength: 50, nullable: true),
                    LastIPAdress = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedUserID",
                        column: x => x.CreatedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_ModifiedUserID",
                        column: x => x.ModifiedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_CreatedUserID",
                        column: x => x.CreatedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Users_ModifiedUserID",
                        column: x => x.ModifiedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    CustomerName = table.Column<string>(maxLength: 20, nullable: false),
                    CustomerSurName = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    TotalPrice = table.Column<decimal>(maxLength: 20, nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(maxLength: 10, nullable: false),
                    Image = table.Column<string>(nullable: false),
                    UnitsInStock = table.Column<short>(nullable: false),
                    QuantityPerUnit = table.Column<string>(nullable: true),
                    ProductDetail = table.Column<string>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedUserID",
                        column: x => x.CreatedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_ModifiedUserID",
                        column: x => x.ModifiedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CartId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    CommentText = table.Column<string>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CreatedUserID",
                        column: x => x.CreatedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_ModifiedUserID",
                        column: x => x.ModifiedUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CreatedUserID = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedUserID = table.Column<Guid>(nullable: true),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    TotalPrice = table.Column<decimal>(maxLength: 255, nullable: false),
                    Quantity = table.Column<int>(maxLength: 20, nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Adress", "CreatedComputerName", "CreatedDate", "CreatedIP", "CreatedUserID", "Email", "FirstName", "ImageUrl", "LastIPAdress", "LastLogin", "LastName", "ModifiedComputerName", "ModifiedDate", "ModifiedIP", "ModifiedUserID", "Number", "Password", "Status", "Title" },
                values: new object[] { new Guid("f6d0f9d9-f28d-4b9d-ae95-ca703f1aa29e"), "SeyyidÖmer Mah.", null, null, null, null, "admin@admin.com", "Admin", "/", "94.54.234.138", new DateTime(2020, 10, 1, 21, 38, 32, 450, DateTimeKind.Local).AddTicks(5622), "Admin", null, null, null, null, "05363192727", "123", 1, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedUserID",
                table: "Categories",
                column: "CreatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedUserID",
                table: "Categories",
                column: "ModifiedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedUserID",
                table: "Comments",
                column: "CreatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ModifiedUserID",
                table: "Comments",
                column: "ModifiedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedUserID",
                table: "Products",
                column: "CreatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedUserID",
                table: "Products",
                column: "ModifiedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedUserID",
                table: "Users",
                column: "CreatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModifiedUserID",
                table: "Users",
                column: "ModifiedUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
