using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThuvienMvc.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    IdAmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAdmin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.IdAmin);
                });

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    IdAuthor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAuthor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.IdAuthor);
                });

            migrationBuilder.CreateTable(
                name: "geres",
                columns: table => new
                {
                    IdGenres = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGenres = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geres", x => x.IdGenres);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameUser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    IdBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IdAuthor = table.Column<int>(type: "int", nullable: true),
                    IdGenres = table.Column<int>(type: "int", nullable: true),
                    PublishingYear = table.Column<int>(type: "int", nullable: true),
                    QuantityInStock = table.Column<int>(type: "int", nullable: true),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.IdBook);
                    table.ForeignKey(
                        name: "FK_books_authors_IdAuthor",
                        column: x => x.IdAuthor,
                        principalTable: "authors",
                        principalColumn: "IdAuthor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_books_geres_IdGenres",
                        column: x => x.IdGenres,
                        principalTable: "geres",
                        principalColumn: "IdGenres",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "borrowings",
                columns: table => new
                {
                    IdBor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Startat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Endat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualEndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrowings", x => x.IdBor);
                    table.ForeignKey(
                        name: "FK_borrowings_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    IdRate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Star = table.Column<int>(type: "int", nullable: false),
                    IdBook = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.IdRate);
                    table.ForeignKey(
                        name: "FK_ratings_books_IdBook",
                        column: x => x.IdBook,
                        principalTable: "books",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ratings_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "borrowing_items",
                columns: table => new
                {
                    IDitem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BorrowingId = table.Column<int>(type: "int", nullable: false),
                    IdBook = table.Column<int>(type: "int", nullable: false),
                    Createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrowing_items", x => x.IDitem);
                    table.ForeignKey(
                        name: "FK_borrowing_items_books_IdBook",
                        column: x => x.IdBook,
                        principalTable: "books",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_borrowing_items_borrowings_BorrowingId",
                        column: x => x.BorrowingId,
                        principalTable: "borrowings",
                        principalColumn: "IdBor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_IdAuthor",
                table: "books",
                column: "IdAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_books_IdGenres",
                table: "books",
                column: "IdGenres");

            migrationBuilder.CreateIndex(
                name: "IX_borrowing_items_BorrowingId",
                table: "borrowing_items",
                column: "BorrowingId");

            migrationBuilder.CreateIndex(
                name: "IX_borrowing_items_IdBook",
                table: "borrowing_items",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_borrowings_IdUser",
                table: "borrowings",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_IdBook",
                table: "ratings",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_IdUser",
                table: "ratings",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "borrowing_items");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "borrowings");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "geres");
        }
    }
}
