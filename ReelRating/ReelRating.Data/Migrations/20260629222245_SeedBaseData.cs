using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReelRating.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedBaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =====================================================
            // TYPE_CINE
            // =====================================================
            migrationBuilder.InsertData(
                schema: "MOVIEDB",
                table: "TYPE_CINE",
                columns: ["ID", "NAME"],
                values: [1, "Filme"]);

            migrationBuilder.InsertData(
                schema: "MOVIEDB",
                table: "TYPE_CINE",
                columns: ["ID", "NAME"],
                values: [2, "Série"]);

            // =====================================================
            // CATEGORIES — Filmes (TypeId = 1)
            // =====================================================
            var filmeCategories = new[]
            {
                "Ação", "Aventura", "Animação", "Comédia", "Crime",
                "Documentário", "Drama", "Família", "Fantasia", "Histórico",
                "Horror", "Musical", "Mistério", "Romance", "Ficção Científica",
                "Curta-metragem", "Esporte", "Suspense", "Guerra", "Faroeste"
            };

            for (int i = 0; i < filmeCategories.Length; i++)
            {
                migrationBuilder.InsertData(
                    schema: "MOVIEDB",
                    table: "CATEGORIES",
                    columns: ["ID", "NAME", "TYPE_ID"],
                    values: [i + 1, filmeCategories[i], 1]);
            }

            // =====================================================
            // CATEGORIES — Séries (TypeId = 2)
            // =====================================================
            var serieCategories = new[]
            {
                "Sitcom", "Reality Show", "Talk Show", "Minissérie", "Novela",
                "Policial", "Teen", "Ação", "Aventura", "Animação",
                "Comédia", "Crime", "Documentário", "Drama", "Família",
                "Fantasia", "Histórico", "Horror", "Musical", "Mistério",
                "Romance", "Ficção Científica", "Curta-metragem", "Esporte",
                "Suspense", "Guerra", "Faroeste"
            };

            for (int i = 0; i < serieCategories.Length; i++)
            {
                migrationBuilder.InsertData(
                    schema: "MOVIEDB",
                    table: "CATEGORIES",
                    columns: ["ID", "NAME", "TYPE_ID"],
                    values: [filmeCategories.Length + i + 1, serieCategories[i], 2]);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MOVIEDB.CATEGORIES");
            migrationBuilder.Sql("DELETE FROM MOVIEDB.TYPE_CINE");
        }
    }
}
