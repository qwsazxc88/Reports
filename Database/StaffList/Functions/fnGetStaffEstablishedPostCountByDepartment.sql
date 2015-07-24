IF OBJECT_ID ('fnGetStaffEstablishedPostCountByDepartment', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedPostCountByDepartment]
GO


--функция возвращает количество штатных единиц подразделения учитывая внутреннюю структуру подразделения до 7 уровня включительно
CREATE FUNCTION dbo.fnGetStaffEstablishedPostCountByDepartment
(
	@DepartmentId int	--Id подразделения
)
RETURNS int
AS
BEGIN
	DECLARE @SEPCount int, @DepId int
	DECLARE @tbl table (DepartmentId int)

	--подсчитываем по текущему подразделению количесвто штатных единиц
	IF (SELECT ItemLevel FROM Department WHERE Id = @DepartmentId) = 1
	BEGIN
		SELECT @SEPCount = isnull(sum(Quantity), 0) FROM StaffEstablishedPost WHERE IsUsed = 1
		RETURN @SEPCount
	END
	ELSE
		SELECT @SEPCount = isnull(sum(Quantity), 0) FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and IsUsed = 1

	--если есть подчиненные подразделения, то считаем и там
	IF EXISTS (SELECT * FROM Department as A
						 INNER JOIN Department as B ON B.ParentId = A.Code1C and B.ItemLevel <= 7
						 WHERE A.Id = @DepartmentId)
	BEGIN
		INSERT INTO @tbl 
		SELECT B.Id FROM Department as A
		INNER JOIN Department as B ON B.ParentId = A.Code1C and B.ItemLevel <= 7
		WHERE A.Id = @DepartmentId

		WHILE EXISTS (SELECT * FROM @tbl)
		BEGIN
			SELECT top 1 @DepId = DepartmentId FROM @tbl

			SET @SEPCount += (SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(@DepId))

			DELETE FROM @tbl WHERE DepartmentId = @DepId
		END

		
	END

	
	RETURN @SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4128) as SEPCount
END
GO

