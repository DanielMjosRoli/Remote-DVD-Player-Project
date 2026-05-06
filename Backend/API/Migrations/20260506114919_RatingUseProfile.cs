using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RatingUseProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 🔥 Safe drop FK (only if it exists)
            migrationBuilder.Sql(@"
            DO $$
            BEGIN
                IF EXISTS (
                    SELECT 1
                    FROM information_schema.table_constraints
                    WHERE constraint_name = 'FK_ratings_users_user_id'
                ) THEN
                    ALTER TABLE ratings
                    DROP CONSTRAINT ""FK_ratings_users_user_id"";
                END IF;
            END $$;
            ");

            // Rename column
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_name='ratings' AND column_name='user_id'
                    ) THEN
                        ALTER TABLE ratings RENAME COLUMN user_id TO profile_id;
                    END IF;
                END $$;
            ");
            // Add new FK
            migrationBuilder.AddForeignKey(
                name: "FK_ratings_profiles_profile_id",
                table: "ratings",
                column: "profile_id",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ratings_profiles_profile_id",
                table: "ratings");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "ratings",
                newName: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_users_user_id",
                table: "ratings",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
