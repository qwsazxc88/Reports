SET NOCOUNT ON
BEGIN TRANSACTION
--делаем временную таблицу с пользователями
SELECT * INTO #Users FROM Users as A WHERE IsActive = 1 and RoleId & 2 > 0

--удаляем из нее всех кто есть в таблице StaffPostReplacement замещенные и замещаемые
DELETE FROM #Users WHERE Id in (SELECT UserId FROM StaffPostReplacement)
DELETE FROM #Users WHERE Id in (SELECT ReplacedId FROM StaffPostReplacement)
--удаляем из нее всех, кто остался в расстановке
DELETE FROM #Users WHERE Id in (SELECT UserId FROM StaffEstablishedPostUserLinks)

-- в расстановке восстанавливаем из этой таблицы замещаемых
UPDATE StaffEstablishedPostUserLinks SET UserId = B.UserId
FROM StaffEstablishedPostUserLinks as A
INNER JOIN StaffPostReplacement as B ON B.UserLinkId = A.Id

DECLARE 
	@UserId int
	,@SEPId int
	,@DepartmentId int
	,@PositionId int
	,@Salary numeric(18, 4)
	,@linkId int
	,@Count int
--делаем цикл по оставшимся в временной таблице
WHILE EXISTS(SELECT * FROM #Users)
BEGIN
	SELECT TOP 1 @UserId = Id, @SEPId = SEPId, @DepartmentId = DepartmentId, @PositionId = PositionId, @Salary = Salary FROM #Users

	-- если есть значение в поле SEPId и есть такая штатная единица
	IF @SEPId is not null and EXISTS (SELECT * FROM StaffEstablishedPost WHERE Id = @SEPId)
	BEGIN
		--смотрим есть ли в расстановке свободное место
		IF EXISTS(SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId is null and (DocId is null or DocId = 0))
		BEGIN
			SELECT top 1 @linkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId is null and (DocId is null or DocId = 0)
			--если есть, ставим этого сотрудника на это место
			UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @linkId
		END
		ELSE	--если нет, то добавляем место для этой штатной единицы и увеличиваем у нее количество
		BEGIN
			INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed, ReserveType, DocId, IsDismissal)
			VALUES(1, @SEPId, @UserId, 1, null, null, 0)

			UPDATE StaffEstablishedPost SET Quantity = Quantity + 1 WHERE Id = @SEPId
		END
	END
	ELSE--если поле SEPId пустое
	BEGIN
		--смотрим есть ли такая штатная единица по подразделению, должности и окладу
		IF EXISTS (SELECT * FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary)
		BEGIN
		--если есть
			SELECT @SEPId = Id FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary
			--то смотрим есть ли свободное место в расстановке
			IF EXISTS(SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId is null and (DocId is null or DocId = 0))
			BEGIN
				SELECT top 1 @linkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId is null and (DocId is null or DocId = 0)
				--если есть, ставим этого сотрудника на это место
				UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @linkId
			END
			ELSE	--если нет, то добавляем место для этой штатной единицы и увеличиваем у нее количество
			BEGIN
				INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed, ReserveType, DocId, IsDismissal)
				VALUES(1, @SEPId, @UserId, 1, null, null, 0)

				UPDATE StaffEstablishedPost SET Quantity = Quantity + 1 WHERE Id = @SEPId
			END

			--проставляем сотруднику значение Id штатной единицы в учетку
			UPDATE Users SET SEPId = @SEPId WHEre Id = @UserId
		END
		ELSE--если нет 
		BEGIN
			--то создаем ее и добавляем в расстановку этого сотрудника
			INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
			VALUES(1, @PositionId, @DepartmentId, 1, @Salary, 1, getdate())

			SET @SEPId = @@IDENTITY

			INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
			VALUES(1, @SEPId, @UserId, 1)

			--создаем заявку для этой штатной единицы
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
																				,SendTo1C
																				,ReasonId
																				,CreatorID)

			SELECT 1
						 ,4	--ввод начальных данных 
						 ,'20151224'
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
						 , getdate()
						 ,null	--причину пока не указываю
						 ,null --ввод начальных данных
			FROM StaffEstablishedPost WHERE Id = @SEPId

			--заносим данные по штатной единице в архив
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
			FROM StaffEstablishedPost WHERE Id = @SEPId
		END
	END

	DELETE FROM #Users WHERE Id = @UserId

	SET @Count = (SELECT count(*) FROM #users)

	PRINT cast(@Count as varchar)

END

DROP TABLE #Users

--ROLLBACK TRANSACTION
--COMMIT TRANSACTION
/*
--проверочный запрос
select b.id, B.Quantity, count(a.SEPId)
from StaffEstablishedPostUserLinks as a
inner join StaffEstablishedPost as b on b.id = a.SEPId
group by B.id, B.Quantity
HAVING B.Quantity <> count(a.SEPId)
*/

--UPDATE StaffEstablishedPostUserLinks SET UserId = null
--select * from StaffEstablishedPostUserLinks