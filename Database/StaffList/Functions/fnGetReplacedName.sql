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
		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + Name + case when BeginDate is null then N'' else N' ' + convert(nvarchar, BeginDate, 103) + N' - ' + convert(nvarchar, EndDate, 103) end + ')'
			FROM (--замены ОЖ
						SELECT B.Name, isnull(C.BeginDate, D.BeginDate) as BeginDate, isnull(C.EndDate, D.EndDate) as EndDate
						FROM StaffPostReplacement as A
						INNER JOIN Users as B ON B.Id = A.ReplacedId and B.IsActive = 1 and B.RoleId & 2 > 0
						--цепляемся отпускам по уходу за ребенком
						LEFT JOIN ChildVacation as C ON C.UserId = B.Id and C.SendTo1C is not null and C.DeleteDate is null and getdate() between C.BeginDate and C.EndDate 
						LEFT JOIN Sicklist as D ON D.UserId = B.Id and D.TypeId = 12 and D.SendTo1C is not null and D.DeleteDate is null and getdate() between D.BeginDate and D.EndDate 
						WHERE A.UserLinkId = @LinkId and A.IsUsed = 1 and (isnull(B.IsPregnant, 0) = 1 or isnull(C.BeginDate, D.BeginDate) is not null)
						UNION ALL
						--ОЖ в расстановке, но еще не замещенная
						SELECT B.Name, isnull(C.BeginDate, D.BeginDate) as BeginDate, isnull(C.EndDate, D.EndDate) as EndDate
						FROM StaffEstablishedPostUserLinks as A
						INNER JOIN Users as B ON B.Id = A.UserId and B.IsActive = 1 and B.RoleId & 2 > 0 
						--цепляемся отпускам по уходу за ребенком
						LEFT JOIN ChildVacation as C ON C.UserId = B.Id and C.SendTo1C is not null and C.DeleteDate is null and getdate() between C.BeginDate and C.EndDate 
						LEFT JOIN Sicklist as D ON D.UserId = B.Id and D.TypeId = 12 and D.SendTo1C is not null and D.DeleteDate is null and getdate() between D.BeginDate and D.EndDate 
						WHERE A.Id = @LinkId and A.IsUsed = 1 and (isnull(B.IsPregnant, 0) = 1 or isnull(C.BeginDate, D.BeginDate) is not null)) as A
		/*
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
			*/
	END
	

	IF @Switch = 2
	BEGIN
	
		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + Name + case when MovementDate is null then N'' else N' ' + convert(nvarchar, MovementDate, 103) + N' - ' + convert(nvarchar, MovementTempTo, 103) end + ')'
		FROM (--замены через заявки
					SELECT B.Name, C.MovementDate, C.MovementTempTo
					FROM StaffPostReplacement as A
					INNER JOIN Users as B ON B.Id = A.ReplacedId and B.IsActive = 1 and B.RoleId & 2 > 0
					INNER JOIN StaffMovements as C ON C.UserId = A.UserId and C.IsTempMoving = 1 and C.Type in (2, 3) and C.Status = 12
					WHERE A.UserLinkId = @LinkId and A.ReasonId = 2 and A.IsUsed = 1 and exists(SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = A.ReplacedId and Id <> @LinkId)
					UNION ALL
					--замены старые, которых в заявках нет
					SELECT A.Name, null as  MovementDate, null as MovementTempTo
					FROM Users as A
					INNER JOIN StaffPostReplacement as B ON B.ReplacedId = A.Id and B.IsUsed = 1 and B.ReasonId = 2
					WHERE A.RegularUserLinkId = @LinkId and exists(SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = B.ReplacedId and Id <> @LinkId)) as A

		/*
		--старый вариант
		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + case when C.Id is null then N'' else N' ' + convert(nvarchar, C.MovementDate, 103) + N' - ' + convert(nvarchar, C.MovementTempTo, 103) end + ')'
		FROM StaffPostReplacement as A
		INNER JOIN Users as B ON B.Id = A.ReplacedId and B.IsActive = 1 and B.RoleId & 2 > 0
		LEFT JOIN StaffMovements as C ON C.UserId = A.UserId and C.IsTempMoving = 1 and C.Type in (2, 3) and C.Status = 12
		WHERE A.UserLinkId = @LinkId and A.ReasonId = 2 and A.IsUsed = 1
		*/
	END


	IF @Switch = 3
	BEGIN
		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + Name + N' ' + case when DateBegin is not null then convert(nvarchar, DateBegin, 103) else N'' end + N' - ' + case when DateEnd is not null then convert(nvarchar, DateEnd, 103) else N'' end + ')' 
		FROM (SELECT B.Name, A.DateBegin, A.DateEnd
					FROM StaffTemporaryReleaseVacancyRequest as A
					INNER JOIN Users as B ON B.Id = A.ReplacedId
					WHERE A.UserLinkId = @LinkId and A.IsUsed = 1
					UNION ALL
					SELECT B.Name, C.DateBegin, C.DateEnd
					FROM StaffPostReplacement as A
					INNER JOIN Users as B ON B.Id = A.ReplacedId
					LEFT JOIN StaffTemporaryReleaseVacancyRequest as C ON C.UserLinkId = A.Id and C.IsUsed = 1
					WHERE A.UserLinkId = @LinkId and A.IsUsed = 1 and A.ReasonId = 3) as A




		/*
		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + N' ' + case when A.DateBegin is not null then convert(nvarchar, A.DateBegin, 103) else N'' end + N' - ' + case when A.DateEnd is not null then convert(nvarchar, A.DateEnd, 103) else N'' end + ')' 
		FROM StaffTemporaryReleaseVacancyRequest as A
		INNER JOIN Users as B ON B.Id = A.ReplacedId
		WHERE A.UserLinkId = @LinkId and A.IsUsed = 1
		ORDER BY A.Id

		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + N' ' + case when C.DateBegin is not null then convert(nvarchar, C.DateBegin, 103) else N'' end + N' - ' + case when C.DateEnd is not null then convert(nvarchar, C.DateEnd, 103) else N'' end + ')' 
		FROM StaffPostReplacement as A
		INNER JOIN Users as B ON B.Id = A.ReplacedId
		LEFT JOIN StaffTemporaryReleaseVacancyRequest as C ON C.UserLinkId = A.Id and C.IsUsed = 1
		WHERE A.UserLinkId = @LinkId and A.IsUsed = 1 and A.ReasonId = 3
		*/
	END
	
	RETURN @ReplacedName
--SELECT dbo.fnGetReplacedName(780, NULL, 2)
--SELECT dbo.fnGetReplacedName(5664, 17488, 1)


END
GO

