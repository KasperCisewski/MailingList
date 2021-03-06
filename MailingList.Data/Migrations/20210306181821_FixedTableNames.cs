using Microsoft.EntityFrameworkCore.Migrations;

namespace MailingList.Data.Migrations
{
    public partial class FixedTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingEmailGroups_MailingEmail_MailingEmailId",
                table: "MailingEmailGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingEmailGroups_MailingGroup_MailingGroupId",
                table: "MailingEmailGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                table: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailingEmailGroups",
                table: "MailingEmailGroups");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                newName: "RoleClaim");

            migrationBuilder.RenameTable(
                name: "MailingEmailGroups",
                newName: "MailingEmailGroup");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaim",
                newName: "IX_RoleClaim_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_MailingEmailGroups_MailingGroupId",
                table: "MailingEmailGroup",
                newName: "IX_MailingEmailGroup_MailingGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_MailingEmailGroups_MailingEmailId",
                table: "MailingEmailGroup",
                newName: "IX_MailingEmailGroup_MailingEmailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaim",
                table: "RoleClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailingEmailGroup",
                table: "MailingEmailGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingEmailGroup_MailingEmail_MailingEmailId",
                table: "MailingEmailGroup",
                column: "MailingEmailId",
                principalTable: "MailingEmail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingEmailGroup_MailingGroup_MailingGroupId",
                table: "MailingEmailGroup",
                column: "MailingGroupId",
                principalTable: "MailingGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_Role_RoleId",
                table: "RoleClaim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingEmailGroup_MailingEmail_MailingEmailId",
                table: "MailingEmailGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingEmailGroup_MailingGroup_MailingGroupId",
                table: "MailingEmailGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_Role_RoleId",
                table: "RoleClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaim",
                table: "RoleClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MailingEmailGroup",
                table: "MailingEmailGroup");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                newName: "RoleClaims");

            migrationBuilder.RenameTable(
                name: "MailingEmailGroup",
                newName: "MailingEmailGroups");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_MailingEmailGroup_MailingGroupId",
                table: "MailingEmailGroups",
                newName: "IX_MailingEmailGroups_MailingGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_MailingEmailGroup_MailingEmailId",
                table: "MailingEmailGroups",
                newName: "IX_MailingEmailGroups_MailingEmailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailingEmailGroups",
                table: "MailingEmailGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingEmailGroups_MailingEmail_MailingEmailId",
                table: "MailingEmailGroups",
                column: "MailingEmailId",
                principalTable: "MailingEmail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingEmailGroups_MailingGroup_MailingGroupId",
                table: "MailingEmailGroups",
                column: "MailingGroupId",
                principalTable: "MailingGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                table: "RoleClaims",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
