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
