IF OBJECT_ID ('fnGetBeginDeparmentForUser', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetBeginDeparmentForUser]
GO

--функция начальный уровень подразделений для построения дерева с учетом автоматического и ручного доступа
CREATE FUNCTION [dbo].[fnGetBeginDeparmentForUser]
(
	@UserId int	--Id пользователя
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int
	 ,ItemLevel int
	 ,IsNeed bit
)
AS
BEGIN
DECLARE 
	@DepartmentId int
	
	INSERT INTO @ReturnTable(Id, ItemLevel, IsNeed)
	SELECT A.DepartmentId, B.ItemLevel, 0 FROM Users as A
	INNER JOIN Department as B ON B.Id = A.DepartmentId
	WHERE A.Id = @UserId


	INSERT INTO @ReturnTable(Id, ItemLevel, IsNeed)
	SELECT A.TargetDepartmentId, B.ItemLevel, 0 FROM ManualRoleRecord as A
	INNER JOIN Department as B ON B.Id = A.TargetDepartmentId
	WHERE A.UserId = @UserId and RoleId in (5, 6) and not exists (SELECT * FROM @ReturnTable WHERE Id = A.TargetDepartmentId)


	WHILE EXISTS (SELECT * FROM @ReturnTable WHERE IsNeed = 0)
	BEGIN
		SELECT top 1 @DepartmentId = Id FROM @ReturnTable WHERE IsNeed = 0 ORDER BY ItemLevel

		DELETE FROM @ReturnTable
		WHERE Id in (SELECT A.Id 
								 FROM (SELECT C.*
											 FROM @ReturnTable as A
											 INNER JOIN Department as B ON B.Id = A.Id
											 INNER JOIN Department as C ON C.Path like B.Path + N'%'
											 WHERE A.Id = @DepartmentId) as A
								 WHERE A.Id <> @DepartmentId)

		UPDATE @ReturnTable SET IsNeed = 1 WHERE Id = @DepartmentId
	END
--select * from dbo.fnGetBeginDeparmentForUser(168) 

	RETURN 
END

GO


