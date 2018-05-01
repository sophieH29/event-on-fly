using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOnFly.Migrations
{
    public partial class AddUspInternalGetProcedureParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[uspInternalGetProcedureParameters]
                 @procedureName NVARCHAR(256)
                AS
                SET NOCOUNT ON
                SELECT PARAMETER_NAME as ParameterName, DATA_TYPE as DataType FROM information_schema.parameters 
                WHERE specific_name = @procedureName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[uspInternalGetProcedureParameters]");
        }
    }
}
