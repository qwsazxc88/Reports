use WebAppTest
go

--СКРИПТ ФОРМИРУЕТ ШТАТНЫЕ ЕДИНИЦЫ И ЗАЯВКИ НА ИХ СОЗДАНИЕ

/*
возможно нужно обращать внимание на беременных при формировании штатных единиц
то есть на одну ШЕ может быть несколько человек, но один работающий, а остальные больные, отпуск и т.д.
*/

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
/*																	
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
SELECT 1
			 ,A.PositionId
			 ,A.DepartmentId
			 ,count(A.PositionId) as Quantity
			 ,isnull(A.Salary, 0)
			 ,1 as IsUsed
			 ,getdate() as BeginAccountDate
			 ,null	--пока не заполняю
			 ,null --ввод начальных данных
FROM (
			--вакансии
			SELECT B.Id as DepartmentId, C.Id as PositionId, cast(isnull(A.[Тарифная ставка (оклад) и пр#, руб#], 0) as numeric(18, 2)) as Salary --[Код подр# 7], [Код Должности], [Тарифная ставка (оклад) и пр#, руб#] 
			FROM StaffEstablishedPostTemp as A
			INNER JOIN Department as B ON B.Code1COld = A.[Код подр# 7]
			INNER JOIN Position as C ON C.Code = A.[Код Должности]
			WHERE [ФИО (краткое)] = 'вакансия' and [Код подр# 7] is not null and [Код Должности] is not null
			--358
			UNION ALL
			--занятые/замещенные должности
			SELECT A.DepartmentId, A.PositionId, A.Salary--, a.IsPregnant, case when A.Salary = 0 then b.[Тарифная ставка (оклад) и пр#, руб#] else b.[Тарифная ставка (оклад) и пр#, руб#] end as Salary, 
						 --b.[Тарифная ставка (оклад) и пр#, руб#], A.name, b.[ФИО (полные)], 
			--			 ,b.[Занимает ставку первичной беременной/ вторичной беременной], b.[таб# номер первичной беременной/вторичной]
			FROM Users as A
			inner join StaffEstablishedPostTemp as b on b.[Табельный номер] = a.Code
			WHERE A.IsActive = 1 and (A.RoleId & 2) > 0 and isnull(a.IsPregnant, 0) = 0 
			--and b.[Занимает ставку первичной беременной/ вторичной беременной] is not null b.[таб# номер первичной беременной/вторичной]
			UNION ALL
			--беременные, но не замещенные
			SELECT A.DepartmentId, A.PositionId, A.Salary--, a.IsPregnant, case when A.Salary = 0 then b.[Тарифная ставка (оклад) и пр#, руб#] else b.[Тарифная ставка (оклад) и пр#, руб#] end as Salary, 
						 --b.[Тарифная ставка (оклад) и пр#, руб#], A.name, b.[ФИО (полные)], b.[Занимает ставку первичной беременной/ вторичной беременной], b.[таб# номер первичной беременной/вторичной]
			FROM Users as A
			inner join StaffEstablishedPostTemp as b on b.[Табельный номер] = a.Code
			WHERE A.IsActive = 1 and (A.RoleId & 2) > 0 --and A.IsPregnant = 1
			--and a.DepartmentId = 105 and a.PositionId = 233 
			and isnull(a.IsPregnant, 0) = 1
			and not exists (select * from StaffEstablishedPostTemp where [таб# номер первичной беременной/вторичной] = a.code)) as A
GROUP BY A.PositionId, A.DepartmentId, isnull(A.Salary, 0)



--формируем заявки на основе штатных единиц
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestTypeId
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
UPDATE Users SET SEPId = B.Id
FROM Users as A
INNER JOIN StaffEstablishedPost as B ON B.PositionId = A.PositionId and B.DepartmentId = A.DepartmentId and B.Salary = A.Salary
--where a.DepartmentId = 11356

--обнуляем оклады сотрудников, пока штатное расписание доступно всем
--UPDATE StaffEstablishedPost SET Salary = 0
--UPDATE StaffEstablishedPostRequest SET Salary = 0
--UPDATE StaffEstablishedPostArchive SET Salary = 0