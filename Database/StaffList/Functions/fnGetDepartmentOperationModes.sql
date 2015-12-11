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
	,ModeType int
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
	SELECT Id, DMDetailId, ModeType, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay 
	FROM StaffDepartmentOperationModes
	WHERE DMDetailId = @DMDetailId

	IF NOT EXISTS (SELECT * FROM @ReturnTable)
	BEGIN
		INSERT INTO @ReturnTable
		SELECT Id, DMDetailId, ModeType, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay FROM StaffDepartmentOperationModes WHERE DMDetailId = -1
		UNION ALL
		SELECT null, null, 1, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 7, null, null, null, null, 0
		--для кассы
		UNION ALL
		SELECT null, null, 2, 7, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 7, null, null, null, null, 0
		--для банкомата
		UNION ALL
		SELECT null, null, 3, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 7, null, null, null, null, 0
		--для кэшина
		UNION ALL
		SELECT null, null, 4, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 7, null, null, null, null, 0
	END


	
	
--select * from dbo.fnGetDepartmentOperationModes(36) order by WeekDay

	RETURN 
END

GO


