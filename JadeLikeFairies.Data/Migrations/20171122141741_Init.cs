using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JadeLikeFairies.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<long>(nullable: false),
                    Hint = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<long>(nullable: false),
                    Hint = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Novels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AltTitles = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Novels_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NovelGenres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<long>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    NovelId = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NovelGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelGenres_Novels_NovelId",
                        column: x => x.NovelId,
                        principalTable: "Novels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NovelTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<long>(nullable: false),
                    NovelId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NovelTags_Novels_NovelId",
                        column: x => x.NovelId,
                        principalTable: "Novels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NovelGenres_GenreId",
                table: "NovelGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_NovelGenres_NovelId",
                table: "NovelGenres",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "IX_Novels_CreatedDate",
                table: "Novels",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Novels_TypeId",
                table: "Novels",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Novels_UpdatedDate",
                table: "Novels",
                column: "UpdatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Novels_Title_AltTitles",
                table: "Novels",
                columns: new[] { "Title", "AltTitles" });

            migrationBuilder.CreateIndex(
                name: "IX_NovelTags_NovelId",
                table: "NovelTags",
                column: "NovelId");

            migrationBuilder.CreateIndex(
                name: "IX_NovelTags_TagId",
                table: "NovelTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NovelGenres");

            migrationBuilder.DropTable(
                name: "NovelTags");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Novels");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
