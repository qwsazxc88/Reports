IF OBJECT_ID ('fnGetRolesFromBiteCode', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetRolesFromBiteCode]
GO

CREATE FUNCTION dbo.fnGetRolesFromBiteCode
(
	@RoleId int
)
RETURNS 
@Roles TABLE 
(
	Id int,
	Name nvarchar(100)
)
AS
BEGIN
	DECLARE @i int
	WHILE EXISTS (SELECT * FROM Role WHERE Id < @RoleId)
	BEGIN
		SELECT @i = max(Id) FROM Role WHERE Id <= @RoleId
	
		INSERT INTO @Roles
		SELECT Id, Name FROM Role WHERE id = @i

		SET @RoleId -= @i

	END

	RETURN 
END
GO