

IF OBJECT_ID ('fnGetManagerFromDepartment', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetManagerFromDepartment]
GO



--функция возвращает строчный список руководителей подразделений заданного уровня
CREATE FUNCTION [dbo].[fnGetManagerFromDepartment]
(
	@Level int	--уровень подразделения
)
RETURNS 
@Result TABLE 
(
	Id int, Manager nvarchar(500)
)
AS
BEGIN
	DECLARE @TMP TABLE (RowNum int, Id int, Name nvarchar(100), Manager nvarchar(500))
	DECLARE @i int, @Id int, @Name varchar(100)

	INSERT INTO @TMP (RowNum, Id, Name)
	SELECT ROW_NUMBER() OVER(ORDER BY A.Id, B.Name) as RowNum,  A.Id, B.Name
	FROM Department as A
	INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1 and B.IsMainManager = 1
	WHERE A.ItemLevel = @Level

	SET @i = 1

	WHILE EXISTS (SELECT * FROM @TMP WHERE RowNum = @i)
	BEGIN
		SELECT top 1 @i = RowNum, @Id = Id, @Name = Name FROM @TMP WHERE RowNum = @i

		UPDATE @TMP SET Manager = isnull(Manager, '') + case when Manager is null then '' else '; ' end + @Name WHERE Id = @Id

		SET @i += 1
	END

	INSERT INTO @Result
	SELECT distinct Id, Manager FROM @TMP

	RETURN 
END

GO


