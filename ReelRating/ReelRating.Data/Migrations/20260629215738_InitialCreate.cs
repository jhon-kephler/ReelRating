using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReelRating.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MOVIEDB");

            migrationBuilder.CreateTable(
                name: "AVERAGE_HOURS",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    HOURS_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    HOURS = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    MOUNT = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AVERAGE_HOURS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NICKNAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    CREATEDAT = table.Column<DateTime>(type: "DATE", nullable: false, defaultValueSql: "SYSDATE"),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer_Average_Hours",
                schema: "MOVIEDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Customer_Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Hours = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Field = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Average_Hours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STATUS",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TYPE_CINE",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYPE_CINE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WHATCHIN",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    REGION = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    AVAILABLE = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHATCHIN", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TYPE_ID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CATEGORY_TYPE",
                        column: x => x.TYPE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "TYPE_CINE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CINE",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    YEAR = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    MONTH = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TMDBID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    WHATCH_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TYPE_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    URL_POSTER = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CINE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CINE_TYPE",
                        column: x => x.TYPE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "TYPE_CINE",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CINE_WHATCH",
                        column: x => x.WHATCH_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "WHATCHIN",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER_WHATCH",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    QTT_ACCESS = table.Column<int>(type: "NUMBER(10)", nullable: true, defaultValue: 0),
                    WHATCH_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER_WHATCH", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CW_CUSTOMER",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CUSTOMER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CW_WHATCH",
                        column: x => x.WHATCH_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "WHATCHIN",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PREFERENCES",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CATEGORIES_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NOTE_ORIGIN = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PREFERENCES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PREF_CATEGORY",
                        column: x => x.CATEGORIES_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CATEGORIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PREF_CUSTOMER",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CUSTOMER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CINE_CATEGORIES",
                schema: "MOVIEDB",
                columns: table => new
                {
                    CINE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CATEGORIES_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CineId1 = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CINE_CATEGORIES", x => new { x.CINE_ID, x.CATEGORIES_ID });
                    table.ForeignKey(
                        name: "FK_CC_CATEGORY",
                        column: x => x.CATEGORIES_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CATEGORIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CC_CINE",
                        column: x => x.CINE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CINE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CINE_CATEGORIES_CINE_CineId1",
                        column: x => x.CineId1,
                        principalSchema: "MOVIEDB",
                        principalTable: "CINE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "COMMENTS",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CINE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COMMENT_TEXT = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    DELETED = table.Column<bool>(type: "BOOLEAN", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COMMENTS_CINE",
                        column: x => x.CINE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CINE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMMENTS_CUSTOMER",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CUSTOMER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAVORITES",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CINE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DELETED = table.Column<bool>(type: "BOOLEAN", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAVORITES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FAVORITES_CINE",
                        column: x => x.CINE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CINE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FAVORITES_CUSTOMER",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CUSTOMER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NOTES",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CINE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TMDB_NOTE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    CUSTOMER_NOTES = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTES_CINE",
                        column: x => x.CINE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CINE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REVIEW",
                schema: "MOVIEDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CINE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CATEGORIES_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TYPE_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    STATUS_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    REVIEW = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    NOTE = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DELETED = table.Column<bool>(type: "BOOLEAN", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEW", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REVIEW_CATEGORY",
                        column: x => x.CATEGORIES_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CATEGORIES",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_REVIEW_CINE",
                        column: x => x.CINE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CINE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REVIEW_CUSTOMER",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "CUSTOMER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REVIEW_STATUS",
                        column: x => x.STATUS_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "STATUS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_REVIEW_TYPE",
                        column: x => x.TYPE_ID,
                        principalSchema: "MOVIEDB",
                        principalTable: "TYPE_CINE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_TYPE_ID",
                schema: "MOVIEDB",
                table: "CATEGORIES",
                column: "TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IDX_CINE_NAME",
                schema: "MOVIEDB",
                table: "CINE",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "IX_CINE_TMDBID",
                schema: "MOVIEDB",
                table: "CINE",
                column: "TMDBID",
                unique: true,
                filter: "\"TMDBID\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CINE_TYPE_ID",
                schema: "MOVIEDB",
                table: "CINE",
                column: "TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CINE_WHATCH_ID",
                schema: "MOVIEDB",
                table: "CINE",
                column: "WHATCH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CINE_CATEGORIES_CATEGORIES_ID",
                schema: "MOVIEDB",
                table: "CINE_CATEGORIES",
                column: "CATEGORIES_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CINE_CATEGORIES_CineId1",
                schema: "MOVIEDB",
                table: "CINE_CATEGORIES",
                column: "CineId1");

            migrationBuilder.CreateIndex(
                name: "IDX_COMMENT_CINE",
                schema: "MOVIEDB",
                table: "COMMENTS",
                column: "CINE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_CUSTOMER_ID",
                schema: "MOVIEDB",
                table: "COMMENTS",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IDX_CUSTOMER_EMAIL",
                schema: "MOVIEDB",
                table: "CUSTOMER",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_WHATCH_CUSTOMER_ID",
                schema: "MOVIEDB",
                table: "CUSTOMER_WHATCH",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_WHATCH_WHATCH_ID",
                schema: "MOVIEDB",
                table: "CUSTOMER_WHATCH",
                column: "WHATCH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITES_CINE_ID",
                schema: "MOVIEDB",
                table: "FAVORITES",
                column: "CINE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITES_CUSTOMER_ID",
                schema: "MOVIEDB",
                table: "FAVORITES",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTES_CINE_ID",
                schema: "MOVIEDB",
                table: "NOTES",
                column: "CINE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PREFERENCES_CATEGORIES_ID",
                schema: "MOVIEDB",
                table: "PREFERENCES",
                column: "CATEGORIES_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PREFERENCES_CUSTOMER_ID",
                schema: "MOVIEDB",
                table: "PREFERENCES",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IDX_REVIEW_STATUS",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_CATEGORIES_ID",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "CATEGORIES_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_CINE_ID",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "CINE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_CUSTOMER_ID",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_TYPE_ID",
                schema: "MOVIEDB",
                table: "REVIEW",
                column: "TYPE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AVERAGE_HOURS",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "CINE_CATEGORIES",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "COMMENTS",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "Customer_Average_Hours",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "CUSTOMER_WHATCH",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "FAVORITES",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "NOTES",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "PREFERENCES",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "REVIEW",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "CATEGORIES",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "CINE",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "CUSTOMER",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "STATUS",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "TYPE_CINE",
                schema: "MOVIEDB");

            migrationBuilder.DropTable(
                name: "WHATCHIN",
                schema: "MOVIEDB");
        }
    }
}
