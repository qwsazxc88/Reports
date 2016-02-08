--НА ДАННЫЙ МОМЕНТ СКРИПТ НЕ ПОДДЕРЖИВАЕТ МНОГОУРОВНЕВЫЕ ОЖ/КП/ДО, ТАК КАК В ОБРАБАТЫВАЕМЫХ ДАННЫХ ЭТОГО НЕ ПРЕДУСМОТРЕНО
--КАК ТОЛЬКО ОПРЕДЕЛИМСЯ, КАК ОТОБРАЖАТЬ В ФАЙЛЕ, НУЖНО БУДЕТ ДОБАВИТЬ В ОБРАБОТКУ
DECLARE
	@RegularCode nvarchar(20)
	,@UserCode nvarchar(20)
	,@PregCode nvarchar(20)
	,@MoveCode nvarchar(20)
	,@AbsentCode nvarchar(20)
	,@Id int
	,@DateAccept datetime
	,@PregBeginDate datetime
	,@PregEndDate datetime
	,@MoveBeginDate datetime
	,@AbsentBeginDate datetime
	,@UserId int			--факт
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

SET NOCOUNT ON

SELECT IDENTITY(INT, 1, 1) as Id, CAST(0 as bit) as IsComplete, * INTO #PA FROM PersonnelArrangements

--НЕСКОЛЬКО ПРОВЕРОК


--если есть записи с пустыми основами, выдать сообщение
IF EXISTS(SELECT * FROM PersonnelArrangements WHERE len(isnull(RegularCode, '')) = 0)
BEGIN
	PRINT N'№1 Обнаружены записи, где не указан основной сотрудник!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS (SELECT * FROM #PA WHERE MoveCode is not null and RegularCode <> MoveCode)
BEGIN
	PRINT N'№16 Обнаружены записи, где некорректно внесены данные по временному переводу!'
	DROP TABLE #PA
	RETURN
END


SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 0
PRINT N'Нужно обработать ' + cast(@CountRow as nvarchar) + N' записей'

BEGIN TRANSACTION

--цикл по строкам
WHILE EXISTS (SELECT * FROM #PA WHERE IsComplete = 0)
BEGIN
	--берем необработанные записи
	SELECT top 1 @Id = A.Id, @UserCode = A.UserCode, @RegularCode = A.RegularCode, @PregCode = A.PregCode, @MoveCode = A.MoveCode, @AbsentCode = A.AbsentCode
				 ,@MoveBeginDate = MoveBeginDate, @AbsentBeginDate = A.AbsentBeginDate, @DateAccept = A.DateAccept
				 ,@STDType = case when A.ContractType = 'Бессрочный' then 1 when A.ContractType = 'СТД' then 2 else 3 end 
				 ,@UserId = B.Id, @UserName = B.Name, @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @IsPreg = B.IsPregnant
				 ,@RegUserId = C.Id, @RegUserName = C.Name, @RegPosititonId = C.PositionId, @RegDepartmentId = C.DepartmentId, @IsRegPreg = C.IsPregnant
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode 
	INNER JOIN Users as C ON C.Code = A.RegularCode 
	WHERE A.IsComplete = 0
	ORDER BY case when A.UserCode = A.RegularCode then 0 else 1 end


	--если факт <> основе, то проверяем учетки сотрудников
	IF NOT EXISTS (SELECT * FROM Users WHERE Id = @UserId and IsActive = 1 and RoleId & 2 > 0)
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

	IF NOT EXISTS (SELECT * FROM Users WHERE Id = @RegUserId and IsActive = 1 and RoleId & 2 > 0)
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

	--находим штатную единицу по фактическому струднику
	SELECT @SEPId = A.Id FROM StaffEstablishedPost as A
	INNER JOIN StaffEstablishedPostUserLinks as B ON B.SEPId = A.Id and B.UserId = @UserId and B.IsUsed = 1
	WHERE A.PositionId = @PosititonId and A.DepartmentId = @DepartmentId and A.IsUsed = 1 and A.Quantity <> 0
	
	IF isnull(@SEPId, 0) = 0
	BEGIN
		PRINT N'№5 Невозможно определить штатную единицу для сотрудника ' + @UserName
		ROLLBACK TRANSACTION
		DROP TABLE #PA
		RETURN
	END


	--если факт совпадает с основой (значит если основа в ОЖ/КП/ДО, то ее никто не заменил)
	IF @UserCode = @RegularCode
	BEGIN
		--находим место в расстановке
		SELECT @UserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @UserId and IsUsed = 1 --and isnull(ReserveType, 0) = 0
	
		IF isnull(@UserLinkId, 0) = 0
		BEGIN
			PRINT N'№6 Невозможно определить позицию в штатной расстановке для сотрудника ' + @UserName + N' (' + @UserCode + N')'
			ROLLBACK TRANSACTION
			DROP TABLE #PA
			RETURN
		END

		--для основного ставим Id расстановки в учетке сотрудника в поле постоянного места в расстановке
		UPDATE Users SET RegularUserLinkId = @UserLinkId WHERE Id = @RegUserId


		--если основа в ОЖ и нет других данных о замене это значит, что сотрудник в расстановке занимает свое место или был заменен
		IF @RegularCode = @PregCode and NOT EXISTS (SELECT * FROM #PA WHERE UserCode <> RegularCode and RegularCode = @RegularCode)
		BEGIN
			--если по сотруднику нет действующих заявок на отпуск по уходу за ребенком, больничного по беременности, и признак беременности в учетку пришел из 1С
			IF NOT EXISTS (SELECT *
										 FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @RegUserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
											 		 UNION ALL
													 SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @RegUserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A) and isnull(@IsRegPreg, 0) = 1
			BEGIN
				--сформировать заявку на ДО (досрочницы)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'Автоматическая обработка данных: на момент обработки данных был указан признак ОЖ')
			END
		END

			

		--если основа в ВРЕМЕННОМ ПЕРЕВОДЕ это значит, что сотрудник в расстановке занимает чье-то или свободное место, такие записи будут обработаны ниже где факт <> основа
		/*
			--находим второе фактическое место работы (ВРЕМЕННО ЗАМЕЩАЕМОЕ)
			--в учетке ставим значение id позиции в расстановке в поле временного места работы
		
			--если это место имеет основного сотрудника, то нужно проверить этот сотрудник находится в расстановке или  заменах
				--если в заменах, то нужно проверить его заменяет обрабатываемый сотрудник или нет.
				--если на заменяемого сотрудника нет замены, то занести запись с заменой
				*/

		--если основа в ДЛИТЕЛЬНОМ ОТСУТСТВИИ и нет других данных о замене
		IF @RegularCode = @AbsentCode --and NOT EXISTS (SELECT * FROM #PA WHERE UserCode <> RegularCode and RegularCode = @RegularCode)
		BEGIN
			--если по сотруднику нет действующих заявок на отпуск по уходу за ребенком, больничного по беременности, и признак беременности в учетку пришел из 1С
			IF NOT EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @RegUserId and DateBegin = @AbsentBeginDate and IsUsed = 1)
			BEGIN
				--сформировать заявку на ДО 
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @AbsentBeginDate, null, 3, 1, N'Автоматическая обработка данных: в обрабатываемых данных кадровиками было указано длительное отсутствие.')
			END
		END
	END
	

	

	--если ФАКТ не совпадает с основой (основа не пустая) значит на место основного сотрудника был принят новый сотрудник по СТД или его временно кто-то заменил из уже работающих
	IF @UserCode <> isnull(@RegularCode, '') and len(isnull(@RegularCode, '')) <> 0
	BEGIN
		--для сотрудников, где факт <> основе 
		--основное место работы надо определять по фактическому сотруднику из записи, где факт <> основа (@UserLinkId)
		SELECT @UserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @UserId and IsUsed = 1 and isnull(ReserveType, 0) = 0
		--если не смогли определить, то возможно основного уже кто-то заменяет, ищем в заменах
		IF isnull(@UserLinkId, 0) = 0
			SELECT @UserLinkId = UserLinkId FROM StaffPostReplacement WHERE ReplacedId = @RegUserId and IsUsed = 1


		--временное место работы надо определять по записи, где основа является фактом (@TempUserLinkId)
		IF EXISTS(SELECT * FROM #PA WHERE UserCode = @RegularCode and RegularCode <> @RegularCode)
		BEGIN
			--находим штатную единицу по фактическому струднику
			--определяем штатную единицу для временного места работы
			
			SELECT @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @TempSEPId = C.Id
			FROM #PA as A
			INNER JOIN Users as B ON B.Code = A.UserCode
			INNER JOIN StaffEstablishedPost as C ON C.PositionId = B.PositionId and C.DepartmentId = B.DepartmentId and C.IsUsed = 1 and C.Quantity <> 0
			INNER JOIN StaffEstablishedPostUserLinks as D ON D.SEPId = C.Id and D.UserId = B.Id and D.IsUsed = 1
			WHERE A.UserCode = @RegularCode and A.RegularCode <> @RegularCode

			--SELECT @TempSEPId = Id FROM StaffEstablishedPost WHERE PositionId = @PosititonId and DepartmentId = @DepartmentId and IsUsed = 1 and Quantity <> 0


			SELECT @TempUserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @TempSEPId and UserId = @RegUserId and IsUsed = 1 and isnull(ReserveType, 0) = 0
			--если не смогли определить, то возможно основного уже кто-то заменяет, ищем в заменах
			IF isnull(@TempUserLinkId, 0) = 0
				SELECT @TempUserLinkId = UserLinkId FROM StaffPostReplacement WHERE ReplacedId = @RegUserId and IsUsed = 1

			IF isnull(@TempUserLinkId, 0) = 0
			BEGIN
				PRINT N'№15 Невозможно определить временную позицию в штатной расстановке для основного сотрудника ' + @RegUserName + N' (' + @RegularCode + N')'
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
		END
		ELSE
		BEGIN
			SET @TempUserLinkId = null
		END


		--для основного ставим Id расстановки в учетке сотрудника в поле постоянного места в расстановке
		UPDATE Users SET RegularUserLinkId = @UserLinkId, TempUserLinkId = @TempUserLinkId WHERE Id = @RegUserId
		--для факта ставим Id расстановки в учетке сотрудника в поле временного места в расстановке
		UPDATE Users SET TempUserLinkId = @UserLinkId WHERE Id = @UserId

		--если уже есть правильная замена, то уходим на следующий круг
		IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId = @RegUserId and UserLinkId = @UserLinkId)
		BEGIN
			--метим запись, как обработанную и заходим на следующий круг
			UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
			SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
			PRINT N'Обработано ' + cast(@CountRow as nvarchar) + N' записей'
			CONTINUE
		END
		ELSE
		BEGIN
			IF isnull(@UserLinkId, 0) = 0
			BEGIN
				PRINT N'№7 Невозможно определить позицию в штатной расстановке для основного сотрудника ' + @RegUserName + N' (' + @RegularCode + N')'
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
		END


		--если основа в ОЖ, 
		IF @RegularCode = @PregCode
		BEGIN
			--если по сотруднику нет действующих заявок на отпуск по уходу за ребенком, больничного по беременности, и признак беременности в учетку пришел из 1С
			IF NOT EXISTS (SELECT *
										 FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @RegUserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
											 		 UNION ALL
													 SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @RegUserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A) and isnull(@IsRegPreg, 0) = 1
			BEGIN
				--сформировать заявку на ДО (досрочницы)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'Автоматическая обработка данных: на момент обработки данных был указан признак ОЖ')
			END


			--надо найти фактического сотрудника в расстановке, если не нашли
			IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
			BEGIN
				--надо проверить в расстановке, была ли замена фактического сотрудника
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
				BEGIN
					PRINT N'№9 Фактического сотрудника нет в расстановке и нет замены фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') основного ' + @RegUserName + N' (' + @RegularCode + N')'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
					--если была, то ПОКА ИДЕМ ДАЛЬШЕ ()
			END
			ELSE--если фактический сотрудник есть в расстановке
			BEGIN
				--нужно проверить заменяет ли он основного
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
				BEGIN
					--если не заменяет, а основной сотрудник все еще в расстановке на прежнем месте или основной уже находится на другом месте, то создать строку замены 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
					BEGIN
						--создаем на основе фактического сотрудника
						INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
						SELECT Id, UserId, @RegUserId, 1, 1 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId

						--нужно в расстановке фактического сотрудника перетащить на позицию основного сотрудника
						IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
						BEGIN
							--место основного сделать вакантным
							UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

							--если ранее в обработке по основному сотруднику заводились заявки на ДО, то перенесем ее на это место
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
						END
						ELSE
						BEGIN
							--основной и фактический сотрудник ошибочно могут быть в разных штатных единицах, по этому нужно позицию основного сотрудника сделать вакантным вторым способом
							--пока это вариант с поиском по Id основного сотрудника и отсутствие замен по этой позиции
							UPDATE StaffEstablishedPostUserLinks SET UserId = null 
							FROM StaffEstablishedPostUserLinks as A
							WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
						END
					END
				END
				ELSE	--проверить наличие замены фактическим сотрудником посторонего сотрудника, не основного
				BEGIN
					IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
					BEGIN
						PRINT N'№10 Обнаружена замена фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') постороннего сотрудника!'
						ROLLBACK TRANSACTION
						DROP TABLE #PA
						RETURN
					END
				END
			END
		END


		--если факт не является основным и в ОЖ, (это сотрудники приняты по СТД и у них нет постоянного места работы)
		IF @UserCode <> @RegularCode and @UserCode = @PregCode
		BEGIN
			--если по фактическому сотруднику нет действующих заявок на отпуск по уходу за ребенком, больничного по беременности, и признак беременности в учетку пришел из 1С
			IF NOT EXISTS (SELECT *
										 FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @UserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
											 		 UNION ALL
													 SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @UserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A) and isnull(@IsPreg, 0) = 1
			BEGIN
				--сформировать заявку на ДО (досрочницы)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @UserId, @PregBeginDate, @PregEndDate, 3, 1, N'Автоматическая обработка данных: на момент обработки данных был указан признак ОЖ')
			END


			--надо найти фактического сотрудника в расстановке, если не нашли
			IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
			BEGIN
				--надо проверить в расстановке, была ли замена фактического сотрудника
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
				BEGIN
					PRINT N'№9 Фактического сотрудника нет в расстановке и нет замены фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') основного ' + @RegUserName + N' (' + @RegularCode + N')'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
					--если была, то ПОКА ИДЕМ ДАЛЬШЕ ()
			END
			ELSE--если фактический сотрудник есть в расстановке
			BEGIN
				--нужно проверить заменяет ли он основного
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
				BEGIN
					--если не заменяет, а основной сотрудник все еще в расстановке на прежнем месте или основной уже находится на другом месте, то создать строку замены 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
					BEGIN
						--создаем на основе фактического сотрудника
						INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
						SELECT Id, UserId, @RegUserId, 1, 1 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId

						--нужно в расстановке фактического сотрудника перетащить на позицию основного сотрудника
						IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
						BEGIN
							--место основного сделать вакантным
							UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

							--если ранее в обработке по основному сотруднику заводились заявки на ДО, то перенесем ее на это место
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
						END
						ELSE
						BEGIN
							--основной и фактический сотрудник ошибочно могут быть в разных штатных единицах, по этому нужно позицию основного сотрудника сделать вакантным вторым способом
							--пока это вариант с поиском по Id основного сотрудника и отсутствие замен по этой позиции
							UPDATE StaffEstablishedPostUserLinks SET UserId = null 
							FROM StaffEstablishedPostUserLinks as A
							WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
						END
					END
					ELSE	--если факт не заменяет основу и основы уже нет в расстановке (МНОГОЭТАЖНОСТЬ)
					BEGIN
						--надо найти замену основы кем-то, где этот кто-то находится в факте
						IF EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @RegUserId and IsUsed = 1)
						BEGIN
							--определяем последнего заменяющего
							SELECT top 1 @ReplaceUserId = B.UserId
							FROM StaffPostReplacement as A
							INNER JOIN StaffPostReplacement as B ON B.UserLinkId = A.UserLinkId
							WHERE A.ReplacedId = @RegUserId
							ORDER BY B.Id desc

							--проверить наличие отметки ОЖ или ДО у сотрудника, который на данный момент заменяет основу
							IF NOT EXISTS (SELECT * FROM Users as A
														 INNER JOIN #PA as B ON B.PregCode = A.Code or B.AbsentCode = A.Code or B.MoveCode = A.Code
														 WHERE A.Id = @ReplaceUserId)
							BEGIN
								--если нет отметки, то выдать сообщение (возможно нарушена сортировка записей при обработке)
								PRINT N'№17 Обнаружена замена основного сотрудника ' + @RegUserName + N' (' + @RegularCode + N') сотрудником, у которого нет признаком ОЖ и ДО!'
								ROLLBACK TRANSACTION
								DROP TABLE #PA
								RETURN
							END

							IF NOT EXISTS(SELECT * FROM StaffPostReplacement as A
														WHERE ReplacedId = @RegUserId)
							
						--если есть, то сделать замену
						--в расстановке почистить место за фактом
						END
						--если нет замены основы, выдать сообщение
					END
				END
				ELSE	
				BEGIN
					--проверить наличие замены фактическим сотрудником посторонего сотрудника, не основного
					IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
					BEGIN
						PRINT N'№10 Обнаружена замена фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') постороннего сотрудника!'
						ROLLBACK TRANSACTION
						DROP TABLE #PA
						RETURN
					END
				END
			END
		END




		--если основа в временном переводе
		IF @RegularCode = @MoveCode
		BEGIN
			--если нет перевода, то создать заявку на ДО
			IF NOT EXISTS(SELECT * FROM StaffMovements WHERE Type in (2, 3) and Status = 12 and UserId = @RegUserId and TargetStaffEstablishedPostRequest = @UserLinkId)
			BEGIN
				--сформировать заявку на ДО (досрочницы)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @MoveBeginDate, null, 3, 1, N'Автоматическая обработка данных: в обрабатываемых данных кадровиками было указано кадровое перемещение, но заявки нет!')
			END


			--надо найти фактического сотрудника в расстановке, если не нашли
			IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
			BEGIN
				--надо проверить в расстановке, была ли замена фактического сотрудника
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
				BEGIN
					PRINT N'№11 Фактического сотрудника нет в расстановке и нет замены фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') основного ' + @RegUserName + N' (' + @RegularCode + N')'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
					--если была, то ПОКА ИДЕМ ДАЛЬШЕ ()
			END
			ELSE--если фактический сотрудник есть в расстановке
			BEGIN
				--нужно проверить заменяет ли он основного
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
				BEGIN
					--если не заменяет, а основной сотрудник все еще в расстановке на прежнем месте или основной уже находится на другом месте, то создать строку замены 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
					BEGIN
						--создаем на основе фактического сотрудника
						INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
						SELECT Id, UserId, @RegUserId, 1, 2 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId
						
						--нужно в расстановке фактического сотрудника перетащить на позицию основного сотрудника
						IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
						BEGIN
							--место основного сделать вакантным
							UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

							--если ранее в обработке по основному сотруднику заводились заявки на ДО, то перенесем ее на это место
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
						END
						/*ELSE
						BEGIN
							--основной и фактический сотрудник ошибочно могут быть в разных штатных единицах, по этому нужно позицию основного сотрудника сделать вакантным вторым способом
							--пока это вариант с поиском по Id основного сотрудника и отсутствие замен по этой позиции
							UPDATE StaffEstablishedPostUserLinks SET UserId = null 
							FROM StaffEstablishedPostUserLinks as A
							WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
						END*/
					END
				END
				ELSE	--проверить наличие замены фактическим сотрудником посторонего сотрудника, не основного
				BEGIN
					IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
					BEGIN
						PRINT N'№12 Обнаружена замена фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') постороннего сотрудника!'
						ROLLBACK TRANSACTION
						DROP TABLE #PA
						RETURN
					END
				END
			END
		END



		--если основа в ДЛИТЕЛЬНОМ ОТСУТСТВИИ
		IF @RegularCode = @AbsentCode
		BEGIN
			--если по сотруднику нет действующих заявок на ДО
			IF NOT EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @RegUserId and DateBegin = @AbsentBeginDate and IsUsed = 1)
			BEGIN
				--сформировать заявку на ДО 
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @AbsentBeginDate, null, 3, 1, N'Автоматическая обработка данных: в обрабатываемых данных кадровиками было указано длительное отсутствие.')
			END
		END


		--надо найти фактического сотрудника в расстановке, если не нашли
		IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
		BEGIN
			--надо проверить в расстановке, была ли замена фактического сотрудника
			IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
			BEGIN
				PRINT N'№13 Фактического сотрудника нет в расстановке и нет замены фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') основного ' + @RegUserName + N' (' + @RegularCode + N')'
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
				--если была, то ПОКА ИДЕМ ДАЛЬШЕ ()
		END
		ELSE--если фактический сотрудник есть в расстановке
		BEGIN
			--нужно проверить заменяет ли он основного
			IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
			BEGIN
				--если не заменяет, а основной сотрудник все еще в расстановке на прежнем месте или основной уже находится на другом месте, то создать строку замены 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
				BEGIN
				--создаем на основе фактического сотрудника
					INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
					SELECT Id, UserId, @RegUserId, 1, 3 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId
					
					--нужно в расстановке фактического сотрудника перетащить на позицию основного сотрудника
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
					BEGIN
						--место основного сделать вакантным
						UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

						--если ранее в обработке по основному сотруднику заводились заявки на ДО, то перенесем ее на это место
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
					END
					ELSE
					BEGIN
						--основной и фактический сотрудник ошибочно могут быть в разных штатных единицах, по этому нужно позицию основного сотрудника сделать вакантным вторым способом
						--пока это вариант с поиском по Id основного сотрудника и отсутствие замен по этой позиции
						UPDATE StaffEstablishedPostUserLinks SET UserId = null 
						FROM StaffEstablishedPostUserLinks as A
						WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
					END
				END
			END
			ELSE	--проверить наличие замены фактическим сотрудником посторонего сотрудника, не основного
			BEGIN
				IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
				BEGIN
					PRINT N'№14 Обнаружена замена фактическим сотрудником ' + @UserName + N' (' + @UserCode + N') постороннего сотрудника!'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
			END
		END
	END


	--метим запись, чтобы ее не учитывать в дальнейшей обработке	
	UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
	SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
	PRINT N'Обработано ' + cast(@CountRow as nvarchar) + N' записей'
END


PRINT N'Данные успешно обработаны!'
COMMIT TRANSACTION
--ROLLBACK TRANSACTION


DROP TABLE #PA