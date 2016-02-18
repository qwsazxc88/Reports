--НА ДАННЫЙ МОМЕНТ СКРИПТ НЕ ПОДДЕРЖИВАЕТ МНОГОУРОВНЕВЫЕ ОЖ/КП/ДО, ТАК КАК В ОБРАБАТЫВАЕМЫХ ДАННЫХ ЭТОГО НЕ ПРЕДУСМОТРЕНО
--КАК ТОЛЬКО ОПРЕДЕЛИМСЯ, КАК ОТОБРАЖАТЬ В ФАЙЛЕ, НУЖНО БУДЕТ ДОБАВИТЬ В ОБРАБОТКУ
DECLARE
	@RegularCode nvarchar(20)
	,@UserCode nvarchar(20)
	,@PregCode nvarchar(20)
	,@MoveCode nvarchar(20)
	,@AbsentCode nvarchar(20)
	,@TempCode nvarchar(20)
	,@Id int
	,@DateAccept datetime
	,@PregBeginDate datetime
	,@PregEndDate datetime
	,@MoveBeginDate datetime
	,@AbsentBeginDate datetime
	,@UserId int			--факт
	,@TempUserId int			--факт
	,@RegUserId int		--основа
	,@ReplaceUserId int	--последний заменяющий 
	,@PosititonId int
	,@RegPosititonId int
	,@DepartmentId int
	,@RegDepartmentId int
	,@SEPId int
	,@TempSEPId int
	,@UserLinkId int
	,@FactUserLinkId int
	,@TempUserLinkId int
	,@UserName nvarchar(250)
	,@RegUserName nvarchar(250)
	,@IsPreg bit
	,@IsRegPreg bit
	,@STDType int
	,@CountRow int
	,@OrderId int
	,@ReasonId int
	,@IsOff bit


SET NOCOUNT ON

SET @IsOff = 1	--ВЫКЛЮЧАЕМ РЯД ПРОВЕРОК ПОСЛЕ ВЫЯСНЕНИЯ
SET @DepartmentId = 4189	--Дирекция ДАЛЬНЕВОСТОЧНАЯ


SELECT IDENTITY(INT, 1, 1) as Id, CAST(0 as bit) as IsComplete, * 
,case when UserCode is not null and RegularCode is null then 1	--перевод/прием на свободную вакансию
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--основной сотрудник работает на своем месте
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--основной сотрудник в ОЖ - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--основной сотрудник в ДО - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--основной сотрудник в КП - временная вакансия
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--основной сотрудник в КП - на место был перевод или прием по СТД
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--основной сотрудник в ДО - на место был перевод или прием по СТД
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--фактический сотрудник в ОЖ 
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--фактический сотрудник в ДО
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--фактический сотрудник работает на месте основной в текущий момент времени
							else 99 end as OrderId
,CAST(null as int) as UserLink	--временное
,CAST(null as int) as RegLink		--основное
INTO #PA FROM PersonnelArrangements
ORDER BY --RegularSurname,
		case when UserCode is not null and RegularCode is null then 1	--перевод/прием на свободную вакансию
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--основной сотрудник работает на своем месте
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--основной сотрудник в ОЖ - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--основно	й сотрудник в ДО - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--основной сотрудник в КП - временная вакансия
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--основной сотрудник в КП - на место был перевод или прием по СТД
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--основной сотрудник в ДО - на место был перевод или прием по СТД
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--фактический сотрудник в ОЖ 
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--фактический сотрудник в ДО
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--фактический сотрудник работает на месте основной в текущий момент времени
							else 99 end
--НЕСКОЛЬКО ПРОВЕРОК


--если есть записи с пустыми основами, выдать сообщение (ВЫЯВЛЕННЫЕ ЗАПИСИ УТОЧНИТЬ У КАДРОВИКОВ (ВОЗМОЖНО НУЖНО СОЗДАТЬ ШТАТНУЮ ЕДИНИЦУ), ПОСЛЕ ВЫЯСНЕНИЯ ЗАКОММЕНТАРИТЬ ПРОВЕРКУ И ЗАПУСТИТЬ ОБРАБОТКУ)
IF @IsOff = 0
BEGIN
	IF EXISTS(SELECT * FROM PersonnelArrangements WHERE len(isnull(RegularCode, '')) = 0 or len(isnull(RegularSurname, '')) = 0 or len(isnull(UserCode, '')) = 0 or len(isnull(Surname, '')) = 0)
	BEGIN
		PRINT N'№1 Обнаружены записи, где не указан основной сотрудник!'
		DROP TABLE #PA
		RETURN
	END
END

--select UserCode, COUNT(UserCode) from PersonnelArrangements where UserCode <> RegularCode group by UserCode having COUNT(UserCode) > 1
IF EXISTS (SELECT * FROM #PA WHERE MoveCode is not null and RegularCode <> MoveCode)
BEGIN
	PRINT N'№2 Обнаружены записи, где некорректно внесены данные по временному переводу!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS (SELECT * FROM #PA WHERE OrderId = 99)
BEGIN
	PRINT N'№99 Обнаружены записи, которые требуют уточнений и правки перед началом обработки!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.RegularCode and (B.IsActive = 0 or B.RoleId & 2097152 > 0))
BEGIN
	PRINT N'№3.1 Обнаружены уволенные сотрудники, которые в обрабатываемых данных являются основными!'
	DROP TABLE #PA
	RETURN
END

IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode and (B.IsActive = 0 or B.RoleId & 2097152 > 0))
BEGIN
	PRINT N'№3.2 Обнаружены уволенные сотрудники, которые в обрабатываемых данных являются фактическими!'
	DROP TABLE #PA
	RETURN
END

--сопоставляем коды с фамилиями
IF @IsOff = 0
BEGIN
	IF EXISTS(SELECT * FROM PersonnelArrangements as A
						INNER JOIN Users as B ON B.Code = A.UserCode and B.IsActive = 1 and B.RoleId & 2 > 0
						WHERE B.Name <> A.Surname)
	BEGIN
		PRINT N'№4.1 Обнаружено несоответствие табельных номеров и ФИО фактических сотрудников в обрабатываемых данных и справочнике сотрудников!'
		DROP TABLE #PA
		RETURN
	END


	IF EXISTS(SELECT * FROM PersonnelArrangements as A
						INNER JOIN Users as B ON B.Code = A.RegularCode
						WHERE B.Name <> A.RegularSurname)
	BEGIN
		PRINT N'№4.2 Обнаружено несоответствие табельных номеров и ФИО основных сотрудников в обрабатываемых данных и справочнике сотрудников!'
		DROP TABLE #PA
		RETURN
	END
END

IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode
					WHERE A.PregCode is not null and B.Name <> A.Surname)
BEGIN
	PRINT N'№5 Обнаружено несоответствие табельных номеров и ФИО сотрудников в ОЖ в обрабатываемых данных и справочнике сотрудников!'
	DROP TABLE #PA
	RETURN
END

IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode
					WHERE A.MoveCode is not null and B.Name <> A.Surname)
BEGIN
	PRINT N'№6 Обнаружено несоответствие табельных номеров и ФИО сотрудников в КП в обрабатываемых данных и справочнике сотрудников!'
	DROP TABLE #PA
	RETURN
END

IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode
					WHERE A.AbsentCode is not null and B.Name <> A.Surname)
BEGIN
	PRINT N'№7 Обнаружено несоответствие табельных номеров и ФИО сотрудников в ДО в обрабатываемых данных и справочнике сотрудников!'
	DROP TABLE #PA
	RETURN
END

IF @IsOff = 0
BEGIN
	IF EXISTS (SELECT C.* FROM Department as A
						 INNER JOIN Department as B ON B.Path like A.Path + '%' and B.ItemLevel = 7
						 INNER JOIN Users as C ON C.DepartmentId = B.Id and C.IsActive = 1 and (C.RoleId & 2 > 0 or C.RoleId & 16384 > 0)
						 INNER JOIN EmploymentCandidate as D ON D.UserId = C.Id and D.Status = 8 and D.SendTo1C is not null --and D.
						 WHERE a.Id = @DepartmentId and not exists (SELECT * FROM PersonnelArrangements WHERE UserCode = C.Code))
	BEGIN
		PRINT N'№8.0 Обнаружены новые сотрудники, которых нет в обрабатываемых данных!'
		SELECT C.* FROM Department as A
		INNER JOIN Department as B ON B.Path like A.Path + '%' and B.ItemLevel = 7
		INNER JOIN Users as C ON C.DepartmentId = B.Id and C.IsActive = 1 and (C.RoleId & 2 > 0 or C.RoleId & 16384 > 0)
		INNER JOIN EmploymentCandidate as D ON D.UserId = C.Id and D.Status = 8 and D.SendTo1C is not null --and D.
		WHERE a.Id = @DepartmentId and not exists (SELECT * FROM PersonnelArrangements WHERE UserCode = C.Code)
		DROP TABLE #PA
		RETURN
	END
END



--==========================================================================
WHILE EXISTS (SELECT * FROM #PA WHERE IsComplete = 0)
BEGIN
	--берем необработанные записи
	SELECT top 1 @Id = A.Id, @OrderId = A.OrderId, @UserCode = A.UserCode, @RegularCode = A.RegularCode, @PregCode = A.PregCode, @MoveCode = A.MoveCode, @AbsentCode = A.AbsentCode
				 ,@MoveBeginDate = MoveBeginDate, @AbsentBeginDate = A.AbsentBeginDate, @DateAccept = A.DateAccept
				 ,@STDType = case when A.ContractType = 'Бессрочный' then 1 when A.ContractType = 'СТД' then 2 else 3 end 
				 ,@UserId = B.Id, @UserName = B.Name, @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @IsPreg = B.IsPregnant
				 ,@RegUserId = C.Id, @RegUserName = C.Name, @RegPosititonId = C.PositionId, @RegDepartmentId = C.DepartmentId, @IsRegPreg = C.IsPregnant
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode 
	LEFT JOIN Users as C ON C.Code = A.RegularCode 
	WHERE A.IsComplete = 0
	ORDER BY A.Id

	SELECT @PregBeginDate = null, @PregEndDate = null, @UserLinkId = null, @TempUserLinkId = null, @SEPId = null, @FactUserLinkId = null

	--учет многоэтажности, берем последнего фактического работника и определяем основное место работы основы
	SELECT top 1 @UserLinkId = C.Id
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode
	INNER JOIN StaffEstablishedPostUserLinks as C ON C.UserId = B.Id and C.IsUsed = 1
	INNER JOIN StaffEstablishedPost as D ON D.Id = C.SEPId and D.PositionId = B.PositionId and D.DepartmentId = B.DepartmentId
	WHERE A.RegularCode = @RegularCode
	ORDER BY A.OrderId desc

--для факта его основное место работы
--	IF @OrderId not in (1, 2, 3, 4, 5)
	SELECT top 1 @FactUserLinkId = C.Id
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode
	INNER JOIN StaffEstablishedPostUserLinks as C ON C.UserId = B.Id and C.IsUsed = 1
	INNER JOIN StaffEstablishedPost as D ON D.Id = C.SEPId and D.PositionId = B.PositionId and D.DepartmentId = B.DepartmentId
	WHERE A.RegularCode = @UserCode
	ORDER BY A.OrderId desc

	IF isnull(@FactUserLinkId, 0) = 0
		SELECT @FactUserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId

	UPDATE #PA SET IsComplete = 1, UserLink = @FactUserLinkId, RegLink = @UserLinkId WHERE Id = @Id
END

UPDATE #PA SET IsComplete = 0


IF EXISTS(SELECT * FROM #PA	WHERE UserLink is null)
BEGIN
	PRINT N'№8 Неопределены места в расстановке для некоторых сотрудников!'
	SELECT * FROM #PA	WHERE UserLink is null ORDER BY RegularSurname, OrderId
	DROP TABLE #PA
	RETURN
END


IF @IsOff = 0
BEGIN
	IF EXISTS (SELECT * FROM #PA as A
						 INNER JOIN (SELECT RegularCode, count(RegularCode) as cnt FROM #PA 
												 WHERE RegularCode in (SELECT RegularCode FROM #PA WHERE RegularCode = MoveCode)
												 GROUP BY RegularCode HAVING count(RegularCode) = 1) as B ON B.RegularCode = A.RegularCode and B.RegularCode = A.UserCode)
	BEGIN
		PRINT N'№9 В результате кадровых перемещений образовались временные вакансии!'
		SELECT * FROM #PA as A
		INNER JOIN (SELECT RegularCode, count(RegularCode) as cnt FROM #PA 
								WHERE RegularCode in (SELECT RegularCode FROM #PA WHERE RegularCode = MoveCode)
								GROUP BY RegularCode HAVING count(RegularCode) = 1) as B ON B.RegularCode = A.RegularCode and B.RegularCode = A.UserCode
		ORDER BY RegularSurname, OrderId
		DROP TABLE #PA
		RETURN
	END
END
--==========================================================================
SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 0
PRINT N'Нужно обработать ' + cast(@CountRow as nvarchar) + N' записей'


BEGIN TRANSACTION


--по обрабатываемым сотрудникам чистим расстановку
UPDATE StaffEstablishedPostUserLinks SET UserId = null
WHERE UserId in (SELECT Id FROM Users as A
								INNER JOIN (SELECT distinct Code
														FROM (SELECT UserCode as Code FROM #PA 
																	UNION ALL
																	--так как могут быть переводы на свободную вакансию
																	SELECT RegularCode as Code FROM #PA WHERE RegularCode is not null) as A) as B ON B.Code = A.Code)

--чистим текущие отметки о местах работы
UPDATE Users SET RegularUserLinkId = null, TempUserLinkId = null
WHERE Id in (SELECT Id FROM Users as A
								INNER JOIN (SELECT distinct Code
														FROM (SELECT UserCode as Code FROM #PA 
																	UNION ALL
																	--так как могут быть переводы на свободную вакансию
																	SELECT RegularCode as Code FROM #PA WHERE RegularCode is not null) as A) as B ON B.Code = A.Code)

--цикл по строкам
WHILE EXISTS (SELECT * FROM #PA WHERE IsComplete = 0)
BEGIN
	SELECT @PregBeginDate = null, @PregEndDate = null, @UserLinkId = null, @TempUserLinkId = null, @SEPId = null, @FactUserLinkId = null

	--берем необработанные записи
	SELECT top 1 @Id = A.Id, @OrderId = A.OrderId, @UserCode = A.UserCode, @RegularCode = A.RegularCode, @PregCode = A.PregCode, @MoveCode = A.MoveCode, @AbsentCode = A.AbsentCode
				 ,@MoveBeginDate = MoveBeginDate, @AbsentBeginDate = A.AbsentBeginDate, @DateAccept = A.DateAccept
				 ,@STDType = case when A.ContractType = 'Бессрочный' then 1 when A.ContractType = 'СТД' then 2 else 3 end 
				 ,@UserId = B.Id, @UserName = B.Name, @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @IsPreg = B.IsPregnant
				 ,@RegUserId = C.Id, @RegUserName = C.Name, @RegPosititonId = C.PositionId, @RegDepartmentId = C.DepartmentId, @IsRegPreg = C.IsPregnant
				 ,@UserLinkId = A.UserLink, @FactUserLinkId = RegLink
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode 
	LEFT JOIN Users as C ON C.Code = A.RegularCode 
	WHERE A.IsComplete = 0
	ORDER BY A.Id

	

	--если факт <> основе, то проверяем учетки сотрудников
	IF NOT EXISTS (SELECT * FROM Users WHERE Id = @UserId and IsActive = 1 and (RoleId & 2 > 0 or RoleId & 16384 > 0))
	BEGIN
		--если такое случилось, то значит уже обрабтали запись основного сотрудника
		--если фактический сотрудник уволен, значит он удален из расстановки
		IF EXISTS (SELECT * FROM Users WHERE Id = @UserId and RoleId & 2097152 > 0)
		BEGIN
			--метим запись, как обработанную и заходим на следующий круг
			UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
			SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
			PRINT N'Обработано ' + cast(@CountRow as nvarchar) + N' записей'
			CONTINUE
		END
		ELSE
		BEGIN
			PRINT N'№2 Учетная запись фактического сотрудника ' + @UserName + N' не является активной (Id = ' + cast(@UserId as nvarchar) + N')'
			ROLLBACK TRANSACTION
			DROP TABLE #PA
			RETURN
		END
	END
	

	IF @RegUserName is not null and NOT EXISTS (SELECT * FROM Users WHERE Id = isnull(@RegUserId, 0) and IsActive = 1 and RoleId & 2 > 0)
	BEGIN
		PRINT N'№3 Учетная запись основного сотрудника ' + @RegUserName + N' не является активной (Id = ' + cast(@RegUserId as nvarchar) + N')'
		ROLLBACK TRANSACTION
		DROP TABLE #PA
		RETURN
	END
	

	--если поля с ОЖ заполнены пытаемся найти даты периода
	IF @PregCode is not null
	BEGIN
		SELECT @PregBeginDate = MIN(BeginDate), @PregEndDate = MAX(EndDate)
		FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @RegUserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
					UNION ALL
					SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @RegUserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A
	END

	
	--если основа указана в колонках ОЖ, КП и ДО одновременно, проверить хронологию по датам (хронология: ОЖ, КП, ДО)
	IF @RegularCode = @PregCode and @RegularCode = @MoveCode and @RegularCode = @AbsentCode
	BEGIN
		IF @PregBeginDate is not null
		BEGIN
			IF @PregBeginDate > @MoveBeginDate or @PregBeginDate > @AbsentBeginDate or @MoveBeginDate > @AbsentBeginDate
			BEGIN
				PRINT N'№4 Обнаружено нарушение хронологии для сотрудника ' + @RegUserName
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
		END
	END


	--1. прием/перевод на свободную вакансию
	IF @OrderId = 1
	BEGIN
		--ставим в расстановку
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @UserLinkId
		--просто ставим временное место работы
		UPDATE Users SET TempUserLinkId = @UserLinkId WHERE Id = @UserId
	END
		
	--основного сотрудника ставим в расстановку и проставляем у него основное место работы
	IF @OrderId in (2, 3, 4, 5)
	BEGIN
		--ставим в расстановку
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @UserLinkId
		--просто ставим временное место работы
		UPDATE Users SET RegularUserLinkId = @UserLinkId WHERE Id = @UserId
	END
	
	--делаем замену при КП и ДО
	IF @OrderId in (6, 7)
	BEGIN
		IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId = @RegUserId and IsUsed = 1)
		BEGIN
			INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
			VALUES (@UserLinkId, @UserId, @RegUserId, 1, case when @OrderId = 6 then 2 else 3 end)

			--INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			--VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'Автоматическая обработка данных: в обрабатываемых данных кадровиками было указано длительное отсутствие.')
		END
		--ставим в расстановку
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @UserLinkId
		--ставим временное место работы основы
		UPDATE Users SET TempUserLinkId = @UserLinkId, RegularUserLinkId = @UserLinkId WHERE Id = @RegUserId
		--ставим временное место работы у факта
		UPDATE Users SET TempUserLinkId = @UserLinkId WHERE Id = @UserId
	END


	--делаем замену когда основы на факт
	IF @OrderId in (8, 9, 10)
	BEGIN
		IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId = @RegUserId and IsUsed = 1)
		BEGIN
			--нужно определить предыдущую запись по этому основному сотруднику и после этого определить причину замены
			IF (SELECT top 1 PregCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc) is not null
				SET @ReasonId = 1
			IF (SELECT top 1 MoveCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc) is not null
				SET @ReasonId = 2
			IF (SELECT top 1 AbsentCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc) is not null
				SET @ReasonId = 3

			SELECT top 1 @TempCode = UserCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc
			
			SELECT @TempUserId = Id FROM Users WHERE Code = @TempCode

			--делаем замену предыдущему персонажу по этому адресу
			INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
			VALUES (@FactUserLinkId, @UserId, @TempUserId, 1, @ReasonId)

			--INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			--VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'Автоматическая обработка данных: в обрабатываемых данных кадровиками было указано длительное отсутствие.')
		END
		--ставим в расстановку
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @FactUserLinkId
		--ставим временное место работы основы
		UPDATE Users SET RegularUserLinkId = @FactUserLinkId WHERE Id = @RegUserId
		--ставим временное место работы у факта
		UPDATE Users SET TempUserLinkId = @FactUserLinkId WHERE Id = @UserId
	END

	--метим запись, чтобы ее не учитывать в дальнейшей обработке	
	UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
	SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
	PRINT N'Обработано ' + cast(@CountRow as nvarchar) + N' записей'
END

/*
--после обработки могут остаться в расстановке лишние записи для основных сотрудников
UPDATE StaffEstablishedPostUserLinks SET UserId = case when B.RegularUserLinkId = A.Id or B.TempUserLinkId = A.Id then A.UserId else null end
FROM StaffEstablishedPostUserLinks as A
INNER JOIN Users as B ON B.Id = A.UserId
--постоянным сотрудникам ушедшим в ОЖ/КП/ДО - заявки ДО ставим на место (в процессе обработки такие заявки могли быть заведены на другие места в расстановке)
UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = B.RegularUserLinkId
FROM StaffTemporaryReleaseVacancyRequest as A
INNER JOIN Users as B ON B.Id = A.ReplacedId
INNER JOIN PersonnelArrangements as C ON C.RegularCode = B.Code and C.UserCode = B.Code
*/


COMMIT TRANSACTION
PRINT N'Данные успешно обработаны!'
--ROLLBACK TRANSACTION


DROP TABLE #PA



/*
SELECT IDENTITY(INT, 1, 1) as Id, CAST(0 as bit) as IsComplete, * 
,case when UserCode is not null and RegularCode is null then 1	--перевод/прием на свободную вакансию
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--основной сотрудник работает на своем месте
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--основной сотрудник в ОЖ - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--основной сотрудник в ДО - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--основной сотрудник в КП - временная вакансия
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--основной сотрудник в КП - на место был перевод или прием по СТД
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--основной сотрудник в ДО - на место был перевод или прием по СТД
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--фактический сотрудник в ОЖ 
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--фактический сотрудник в ДО
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--фактический сотрудник работает на месте основной в текущий момент времени
							else 99 end as orderid
INTO #PA FROM PersonnelArrangements
ORDER BY --RegularSurname,
		case when UserCode is not null and RegularCode is null then 1	--перевод/прием на свободную вакансию
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--основной сотрудник работает на своем месте
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--основной сотрудник в ОЖ - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--основно	й сотрудник в ДО - временная вакансия
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--основной сотрудник в КП - временная вакансия
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--основной сотрудник в КП - на место был перевод или прием по СТД
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--основной сотрудник в ДО - на место был перевод или прием по СТД
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--фактический сотрудник в ОЖ 
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--фактический сотрудник в ДО
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--фактический сотрудник работает на месте основной в текущий момент времени
							else 99 end



select * from #PA
--where RegularCode in ('0000003465', '0000034169')
order by regularsurname, orderid
--order by regularsurname
--order by orderid
drop table #PA
--Бичёвина Елена Михайловна - 0000011356 - сотрудник находится в ругок дирекции, был обработан при обработке Московской дирекции, так как на ее место была переведена Левина Елена Александровна - 0000026218
--select * from Users where Code = '0000000213'
--select * from Users where Name  = 'Михайлова Евгения Алексеевна'
*/

/*
--ДАЛЬНЕВОСТОЧНАЯ ДИРЕКЦИЯ

--Макаревич Екатерина Георгиевна (до обработки)
delete from  StaffPostReplacement where Id = 230
--замена не по тому адресу (до обработки)
update StaffPostReplacement set UserLinkId = 5221 where Id = 82
--Мартынова Лариса Сергеевна 0000036943 (до обработки)
update StaffEstablishedPostUserLinks set UserId = 25619 WHERE Id = 5111
insert into StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
select Id, 25619, 8741, 1, 1 from StaffEstablishedPostUserLinks WHERE Id = 5111
--Пономарева Оксана Ивановна	0000036945 (до обработки)
update StaffEstablishedPostUserLinks set UserId = 25647 WHERE Id = 5117
insert into StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
select Id, 25647, 15191, 1, 1 from StaffEstablishedPostUserLinks WHERE Id = 5117

--Власова Ольга Владимировна - 0000018076
update StaffEstablishedPostUserLinks set UserId = 8167 WHERE Id = 667
update Users SET RegularUserLinkId = 667 WHERE Id = 8167
--Докучаева Анна Александровна - 0000002248
--update StaffEstablishedPostUserLinks set UserId = 6152 WHERE Id = 25
--update Users SET RegularUserLinkId = 25 WHERE Id = 6152
--Захарова Александра Владимировна	0000035160
update StaffEstablishedPostUserLinks set UserId = 22833 WHERE Id = 111
update Users SET RegularUserLinkId = 111 WHERE Id = 22833
--Краснюкова Екатерина Назимовна	0000034532
update StaffEstablishedPostUserLinks set UserId = 20144 WHERE Id = 749
update Users SET RegularUserLinkId = 749 WHERE Id = 20144
--Суборь Олеся Константиновна	0000007810
update StaffEstablishedPostUserLinks set UserId = 1556 WHERE Id = 268
update Users SET RegularUserLinkId = 268 WHERE Id = 1556
--Усенко Ольга Николаевна	0000008321
update StaffEstablishedPostUserLinks set UserId = 2360 WHERE Id = 836
update Users SET RegularUserLinkId = 836 WHERE Id = 2360
--Квон Ольга Владимировна	0000003389
update StaffEstablishedPostUserLinks set UserId = 6558 WHERE Id = 749
update Users SET RegularUserLinkId = 749 WHERE Id = 6558
--Горбач Анастасия Федоровна 0000016008
update StaffEstablishedPostUserLinks set UserId = 4636 WHERE Id = 1759
update Users SET RegularUserLinkId = 1759 WHERE Id = 4636
*/