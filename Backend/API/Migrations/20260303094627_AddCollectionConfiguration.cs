using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collection_movies_Collections_collection_id",
                table: "collection_movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "collection");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "collection",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "collection",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "collection",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_collection",
                table: "collection",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_collection_movies_collection_collection_id",
                table: "collection_movies",
                column: "collection_id",
                principalTable: "collection",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collection_movies_collection_collection_id",
                table: "collection_movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_collection",
                table: "collection");

            migrationBuilder.RenameTable(
                name: "collection",
                newName: "Collections");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Collections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Collections",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Collections",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_collection_movies_Collections_collection_id",
                table: "collection_movies",
                column: "collection_id",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
