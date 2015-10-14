IF OBJECT_ID ('fnGetDepartmentOperationModes', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetDepartmentOperationModes]
GO

--функция достает режим работы для подразделения по Id управленческих реквизитов заявки
CREATE FUNCTION [dbo].[fnGetDepartmentOperationModes]
(
	@DMDetailId int
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
	,DMDetailId int
	,WeekDay int
	,WorkBegin nvarchar(5)
	,WorkEnd nvarchar(5)
	,BreakBegin nvarchar(5)
	,BreakEnd nvarchar(5)
	,IsWorkDay bit
)
AS
BEGIN

	INSERT INTO @ReturnTable
	SELECT Id, DMDetailId, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay 
	FROM StaffDepartmentOperationModes
	WHERE DMDetailId = @DMDetailId

	IF NOT EXISTS (SELECT * FROM @ReturnTable)
	BEGIN
		INSERT INTO @ReturnTable
		SELECT Id, DMDetailId, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay FROM StaffDepartmentOperationModes WHERE DMDetailId = -1
		UNION ALL
		SELECT null, null, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 7, null, null, null, null, 0
	END


	
	
--select * from dbo.fnGetDepartmentOperationModes(36) order by WeekDay

	RETURN 
END

GO


