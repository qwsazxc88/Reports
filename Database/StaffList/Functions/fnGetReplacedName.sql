IF OBJECT_ID ('fnGetReplacedName', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetReplacedName]
GO


--функция возвращает строку с замещенными сотрудниками в штатной единице для данного сотрудника
CREATE FUNCTION dbo.fnGetReplacedName
(
	--для запуска функции нужно указать один из параметров
	@LinkId int	--Id связи штатной единицы и сотрудника
	,@ReplacedId int	--Id сотрудника который в отпуске, но его никто не замещает
	,@Switch int	--переключатель источника для поиска записей
			--1 - ОЖ
			--2 - временные перемещения
			--3 - длительное отсутствие
)
RETURNS nvarchar(500)
AS
BEGIN
DECLARE 
	@ReplacedName nvarchar(500)

	SET @ReplacedName = N''

	IF @Switch = 1
	BEGIN
		--определяем кого замещает сотрудник
		IF @ReplacedId is null	
		BEGIN
			SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + case when isnull(C.Id, D.Id) is null then N'' else N' ' + convert(nvarchar, isnull(C.BeginDate, D.BeginDate), 103) + N' - ' + convert(nvarchar, isnull(C.EndDate, D.EndDate), 103) end + ')'
			FROM StaffPostReplacement as A
			INNER JOIN Users as B ON B.Id = A.ReplacedId and B.IsActive = 1 and B.RoleId & 2 > 0 and isnull(B.IsPregnant, 0) = 1
			--пока цепляемся отпускам по уходу за ребенком
			LEFT JOIN ChildVacation as C ON C.UserId = B.Id and C.SendTo1C is not null and C.DeleteDate is null and getdate() between C.BeginDate and C.EndDate 
			LEFT JOIN Sicklist as D ON D.UserId = B.Id and D.TypeId = 12 and D.SendTo1C is not null and D.DeleteDate is null and getdate() between D.BeginDate and D.EndDate 
			WHERE A.UserLinkId = @LinkId and A.IsUsed = 1
			--ORDER BY A.Id desc
		END
		ELSE	--определяем сотрудника, который ушел в отпуск по уходу за ребенком, но должность его свободна
			SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end + 
														  N'(' + A.Name + N' ' + convert(nvarchar, isnull(B.BeginDate, C.BeginDate), 103) + N' - ' + convert(nvarchar, isnull(B.EndDate, C.EndDate), 103) + N')'
			FROM Users as A 
			--пока цепляемся отпускам по уходу за ребенком
			LEFT JOIN ChildVacation as B ON B.UserId = A.Id and B.SendTo1C is not null and B.DeleteDate is null and getdate() between B.BeginDate and B.EndDate 
			LEFT JOIN Sicklist as C ON C.UserId = A.Id and C.TypeId = 12 and C.SendTo1C is not null and C.DeleteDate is null and getdate() between C.BeginDate and C.EndDate 
			WHERE A.Id = @ReplacedId and A.IsActive = 1 and A.RoleId & 2 > 0
	END
	

	IF @Switch = 2
	BEGIN
		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + case when C.Id is null then N'' else N' ' + convert(nvarchar, C.MovementDate, 103) + N' - ' + convert(nvarchar, C.MovementTempTo, 103) end + ')'
		FROM StaffPostReplacement as A
		INNER JOIN Users as B ON B.Id = A.ReplacedId and B.IsActive = 1 and B.RoleId & 2 > 0
		LEFT JOIN StaffMovements as C ON C.UserId = A.UserId and C.IsTempMoving = 1 and C.Type in (2, 3) and C.Status = 12
		WHERE A.UserLinkId = @LinkId and A.ReasonId = 2 and A.IsUsed = 1
	END


	IF @Switch = 3
	BEGIN
		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + N' ' + case when A.DateBegin is not null then convert(nvarchar, A.DateBegin, 103) else N'' end + N' - ' + case when A.DateEnd is not null then convert(nvarchar, A.DateEnd, 103) else N'' end + ')' 
		FROM StaffTemporaryReleaseVacancyRequest as A
		INNER JOIN Users as B ON B.Id = A.ReplacedId
		WHERE A.UserLinkId = @LinkId and A.IsUsed = 1
		ORDER BY A.Id

	END
	
	RETURN @ReplacedName
--SELECT dbo.fnGetReplacedName(6227, NULL, 1)
--SELECT dbo.fnGetReplacedName(6902, 4670, 1)


END
GO

