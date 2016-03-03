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
		--SELECT @SEPCount = isnull(sum(Quantity), 0) FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and IsUsed = 1
		SELECT @SEPCount = isnull(sum(C.Quantity), 0) 
		FROM Department as A
		INNER JOIN Department as B ON B.Path like A.Path + N'%'
		INNER JOIN StaffEstablishedPost as C ON C.DepartmentId = B.Id and C.IsUsed = 1 and C.PositionId is not null
		WHERE A.Id = @DepartmentId 
/*
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
*/
	
	RETURN @SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4129) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4130) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4131) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4132) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4205) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(8010) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(8171) as SEPCount
END
GO

