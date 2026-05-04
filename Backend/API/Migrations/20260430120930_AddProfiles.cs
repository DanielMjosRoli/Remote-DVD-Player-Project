using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_users_UserId",
                table: "Profile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profile",
                table: "Profile");

            migrationBuilder.RenameTable(
                name: "Profile",
                newName: "profiles");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "profiles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "profiles",
                newName: "avatar");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "profiles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "profiles",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsKids",
                table: "profiles",
                newName: "is_kids");

            migrationBuilder.RenameIndex(
                name: "IX_Profile_UserId",
                table: "profiles",
                newName: "IX_profiles_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_profiles",
                table: "profiles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_profiles_users_user_id",
                table: "profiles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profiles_users_user_id",
                table: "profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_profiles",
                table: "profiles");

            migrationBuilder.RenameTable(
                name: "profiles",
                newName: "Profile");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Profile",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "Profile",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Profile",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Profile",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_kids",
                table: "Profile",
                newName: "IsKids");

            migrationBuilder.RenameIndex(
                name: "IX_profiles_user_id",
                table: "Profile",
                newName: "IX_Profile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profile",
                table: "Profile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_users_UserId",
                table: "Profile",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
