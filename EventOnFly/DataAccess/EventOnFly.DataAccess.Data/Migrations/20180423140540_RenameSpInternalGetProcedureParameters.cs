using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOnFly.Migrations
{
    public partial class RenameSpInternalGetProcedureParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EXEC sp_rename 'dbo.uspInternalGetProcedureParameters', 'spInternalGetProcedureParameters'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("EXEC sp_rename 'dbo.spInternalGetProcedureParameters', 'uspInternalGetProcedureParameters'");
        }
    }
}
