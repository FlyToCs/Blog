using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infa.Db.SqlServer.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class addcontexttopost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Context",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Context",
                table: "Posts");
        }
    }
}
