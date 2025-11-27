using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infa.Db.SqlServer.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class updatecomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "PostComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PostComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PostComments");
        }
    }
}
