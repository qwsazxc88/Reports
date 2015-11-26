use WebAppTest
go

--СКРИПТ ФОРМИРУЕТ ШТАТНЫЕ ЕДИНИЦЫ И ЗАЯВКИ НА ИХ СОЗДАНИЕ
SET NOCOUNT ON
DECLARE @UserId int, @ReplacedId int, @IsPregnant bit, @DepartmentId int, @PositionId int, @Salary numeric(18, 2), @SEPId int, @SPCount int, @UserCount int, @LinkId int
/*																	
--старый вариант формирования штатных единиц
--заполняем справочник штатных единиц на основе сформированных заявок
INSERT INTO StaffEstablishedPost([Version]
																	,PositionId
																	,DepartmentId
																	,Quantity
																	,Salary
																	,IsUsed
																	,BeginAccountDate
																	,[Priority]
																	,CreatorID)

SELECT 1
			 ,B.PositionId
			 ,A.Id
			 ,count(B.PositionId) as Quantity
			 ,isnull(Salary, 0)
			 ,1 as IsUsed
			 ,getdate() as BeginAccountDate
			 ,null	--пока не заполняю
			 ,null --ввод начальных данных
--FROM StaffEstablishedPostRequest
FROM Department as A
INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1 and (RoleId & 2) > 0
INNER JOIN Position as C ON C.id = B.PositionId
GROUP BY A.Id, B.PositionId, C.Name, B.Salary
*/

--новый вариант формирования штатных единиц с учетом беременных и их замещения 

--выбираем активные учетные записи с ролью сотрудника в временую структуру и идем по ним в цикле с сортировкой по признаку беременности (в начале не беременные)
SELECT * INTO #Users FROM Users WHERE RoleId & 2 > 0 and IsActive = 1

WHILE EXISTS(SELECT * FROM #Users)
BEGIN
	SET @ReplacedId = null

	SELECT top 1 @UserId = A.Id, @IsPregnant = isnull(A.IsPregnant, 0), @DepartmentId = A.DepartmentId, @PositionId = A.PositionId, 
				 @Salary = case when isnull(A.Salary, 0) = 0 then isnull(B.[Тарифная ставка (оклад) и пр#, руб#], 0) else isnull(A.Salary, 0) end,
				 @ReplacedId = C.Id
	FROM #Users as A
	LEFT JOIN StaffEstablishedPostTemp as B ON B.[Табельный номер] = A.Code
	LEFT JOIN #Users as C ON C.Code = B.[таб# номер первичной беременной/вторичной]
	ORDER BY isnull(A.IsPregnant, 0)
	--если нет признака беременности, то 
	IF @IsPregnant = 0
	BEGIN
		--если нет штатной единицы с такими подразделением, должностью и окладом
		IF NOT EXISTS (SELECT * FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary)
		BEGIN
			--формируем строку штатной единицы с количеством 1 (если оклад в учетке равен 0, то надо его взять из выгрузки)
			INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
			VALUES(1, @PositionId, @DepartmentId, 1, @Salary, 1, getdate())

			SET @SEPId = @@IDENTITY

		
		END
		ELSE
		BEGIN
			--если уже есть такая штатная единица, то увеличиваем ее количество на 1
			SELECT @SEPId = Id FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary

			UPDATE StaffEstablishedPost SET Quantity = Quantity + 1 WHERE Id = @SEPId
		END

		--проставляем полученный Id штатной единицы в учетку
		UPDATE Users SET SEPId = @SEPId WHERE Id = @UserId
		--формируем строку с линковкой штатной единицы и сотрудника
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		VALUES(1, @SEPId, @UserId, 1)

		SET @LinkId = @@IDENTITY

		--смотрим в данные выгрузки, заменяет ли этот сотрудник кого нибудь
		--если да
		IF @ReplacedId is not null
		BEGIN
				--то проставляем Id штатной единицы в учетку заменяемого сотрудника
				UPDATE Users SET SEPId = @SEPId WHERE Id = @ReplacedId

				--заносим в таблицу замещения соответствующие данные
				INSERT INTO StaffPostReplacement(UserId, ReplacedId, IsUsed, UserLinkId)
				VALUES(@UserId, @ReplacedId, 1, @LinkId)

				--удаляем из временной структуры учетку заменяемого
				DELETE FROM #Users WHERE Id = @ReplacedId
		END
	END
	ELSE
	BEGIN
		--к этому моменту должны остаться учетки беременных которых никто не заменяет или нет по ним данных в файле выгрузки
		--если нет штатной единицы с такими подразделением, должностью и окладом
		IF NOT EXISTS (SELECT * FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary)
		BEGIN
			--формируем строку штатной единицы с количеством 1 (если оклад в учетке равен 0, то надо его взять из выгрузки)
			INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
			VALUES(1, @PositionId, @DepartmentId, 1, @Salary, 1, getdate())

			SET @SEPId = @@IDENTITY
		END
		ELSE
		BEGIN
			--если уже есть такая штатная единица, то увеличиваем ее количество на 1
			SELECT @SEPId = Id FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary

			UPDATE StaffEstablishedPost SET Quantity = Quantity + 1 WHERE Id = @SEPId
		END

		--проставляем полученный Id штатной единицы в учетку
		UPDATE Users SET SEPId = @SEPId WHERE Id = @UserId

		--формируем строку с линковкой штатной единицы и сотрудника
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		VALUES(1, @SEPId, @UserId, 1)

		SET @LinkId = @@IDENTITY
	END

	--удаляем учетку сотрудника
	DELETE FROM #Users WHERE Id = @UserId

	SET @UserCount = (SELECT count(*) FROM #Users)
	print 'осталось обработать ' + cast(@UserCount as varchar) + ' учетных записей'
END

print 'СФОРМИРОВАНЫ ШТАТНЫЕ ЕДИНИЦЫ'

--из файла выгрузки выбираем вакансии, цикл
/*
SELECT DepartmentId, PositionId, Salary, sum(SPCount) as SPCount
INTO #Vacation
FROM (SELECT B.Id as DepartmentId, C.Id as PositionId, isnull(A.[Тарифная ставка (оклад) и пр#, руб#], 0) as Salary, 1 as SPCount 
			FROM StaffEstablishedPostTemp as A
			INNER JOIN Department as B ON B.Code1COld = A.[Код подр# 7]
			INNER JOIN Position as c ON C.Code = A.[Код Должности]
			WHERE [ФИО (краткое)] = 'вакансия' and [Код подр# 7] is not null and [Код Должности] is not null) as A
			GROUP BY DepartmentId, PositionId, Salary
*/


SELECT B.Id as DepartmentId, C.Id as PositionId, isnull(A.[Тарифная ставка (оклад) и пр#, руб#], 0) as Salary, 1 as SPCount 
INTO #Vacation
FROM StaffEstablishedPostTemp as A
INNER JOIN Department as B ON B.Code1COld = A.[Код подр# 7]
INNER JOIN Position as c ON C.Code = A.[Код Должности]
WHERE [ФИО (краткое)] = 'вакансия' and [Код подр# 7] is not null and [Код Должности] is not null

WHILE EXISTS(SELECT * FROM #Vacation)
BEGIN
	SELECT top 1 @DepartmentId = DepartmentId, @PositionId = PositionId, @Salary = Salary, @SPCount = SPCount FROM #Vacation

	--если нет штатной единицы с такими подразделением, должностью и окладом
	IF NOT EXISTS (SELECT * FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary)
	BEGIN
		--формируем строку штатной единицы с количеством 1 (если оклад в учетке равен 0, то надо его взять из выгрузки)
		INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
		VALUES(1, @PositionId, @DepartmentId, @SPCount, @Salary, 1, getdate())

		SET @SEPId = @@IDENTITY

		--формируем строку с линковкой штатной единицы и сотрудника
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		VALUES(1, @SEPId, null, 1)
	END
	ELSE
	BEGIN
		--если уже есть такая штатная единица, то увеличиваем ее количество 
		UPDATE StaffEstablishedPost SET Quantity = Quantity + @SPCount
		WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary

		--формируем строку с линковкой штатной единицы и сотрудника
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		SELECT 1, Id, null, 1 FROM StaffEstablishedPost 
		WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary
		--VALUES(1, @SEPId, null, 1)
	END

	

	DELETE FROM #Vacation WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary and SPCount = @SPCount
END

print 'ВАКАНСИИ ДОБАВЛЕНЫ'



--формируем заявки на основе штатных единиц
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestTypeId
																				,DateRequest
																				,SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,IsUsed
																				,IsDraft
																				,DateSendToApprove
																				,DateAccept
																				,BeginAccountDate
																				,ReasonId
																				,CreatorID)

SELECT 1
			 ,4	--ввод начальных данных 
			 ,'20151031'
			 ,Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,0
			 ,getdate()
			 ,getdate()
			 ,getdate()
			 ,null	--причину пока не указываю
			 ,null --ввод начальных данных
FROM StaffEstablishedPost
--FROM Department as A
--INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1 and (RoleId & 2) > 0
--INNER JOIN Position as C ON C.id = B.PositionId
--GROUP BY A.Id, B.PositionId, C.Name, B.Salary

--попутно заполняем архив начальными данными
INSERT INTO StaffEstablishedPostArchive(SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,IsUsed
																				,BeginAccountDate
																				,[Priority]
																				,CreatorID)
SELECT Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,BeginAccountDate
			 ,null	--пока не заполняю
			 ,CreatorID
FROM StaffEstablishedPost


--проставляем Id штатной единицы для пользователей по текущим данным
--UPDATE Users SET SEPId = B.Id
--FROM Users as A
--INNER JOIN StaffEstablishedPost as B ON B.PositionId = A.PositionId and B.DepartmentId = A.DepartmentId and B.Salary = A.Salary
--where a.DepartmentId = 11356

--обнуляем оклады сотрудников, пока штатное расписание доступно всем
--UPDATE StaffEstablishedPost SET Salary = 0
--UPDATE StaffEstablishedPostRequest SET Salary = 0
--UPDATE StaffEstablishedPostArchive SET Salary = 0


drop table #Users
drop table #Vacation