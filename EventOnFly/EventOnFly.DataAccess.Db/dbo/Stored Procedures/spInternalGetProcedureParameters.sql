
                CREATE PROCEDURE [dbo].[spInternalGetProcedureParameters]
                 @procedureName NVARCHAR(256)
                AS
                SET NOCOUNT ON
                SELECT PARAMETER_NAME as ParameterName, DATA_TYPE as DataType FROM information_schema.parameters 
                WHERE specific_name = @procedureName
