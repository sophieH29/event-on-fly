using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOnFly.Migrations
{
    public partial class spCreateNewService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string storedProcedure = @"IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spCreateNewService')
                DROP PROCEDURE [dbo].[spCreateNewService]
                GO

                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                -- =============================================
                -- Author:      Sofia Huts
                -- Create Date: 04/21/2018
                -- Description: Create Service
                -- =============================================
                CREATE PROCEDURE [dbo].spCreateNewService
                (
	                @Name nvarchar(max),
	                @ServiceType int,
	                @State int = 2, -- InRegistrationProcess
	                @UserId int
                )
                AS
                BEGIN
   
                   SET NOCOUNT ON

                   INSERT INTO [dbo].[Service]
                           ([Name]
                           ,[ServiceType]
                           ,[State]
		                   ,[UserId])

                     VALUES
                           (@Name,
                            @ServiceType,
			                @State,
			                @UserId)

                END
                GO";

            migrationBuilder.Sql(storedProcedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[spCreateNewService]");
        }
    }
}
