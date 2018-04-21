IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Service_Create')
DROP PROCEDURE [dbo].[Service_Create]
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
CREATE PROCEDURE [dbo].[Service_Create]
(
	@Name nvarchar(max),
	@ServiceType int,
	@State int = 2 -- InRegistrationProcess
)
AS
BEGIN
   
   SET NOCOUNT ON

   INSERT INTO [dbo].[Service]
           ([Name]
           ,[ServiceType]
           ,[State])

     VALUES
           (@Name,
            @ServiceType,
			@State)

END
GO
