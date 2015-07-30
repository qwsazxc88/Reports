--СКРИПТ ФОРМИРУЕТ ШТАТНЫЕ ЕДИНИЦЫ И ЗАЯВКИ НА ИХ СОЗДАНИЕ
--заполняем справочник штатных единиц на основе сформированных заявок
INSERT INTO StaffEstablishedPost([Version]
																	,PositionId
																	,DepartmentId
																	,Quantity
																	,Salary
																	,StaffECSalary
																	,IsUsed
																	,BeginAccountDate
																	,[Priority]
																	,CreatorID)
SELECT 1
			 ,B.PositionId
			 ,A.Id
			 ,count(B.PositionId) as Quantity
			 ,isnull(Salary, 0)
			 ,0 as StaffECSalary
			 ,1 as IsUsed
			 ,getdate() as BeginAccountDate
			 ,null	--пока не заполняю
			 ,null --ввод начальных данных
--FROM StaffEstablishedPostRequest
FROM Department as A
INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1 and (RoleId & 2) > 0
INNER JOIN Position as C ON C.id = B.PositionId
GROUP BY A.Id, B.PositionId, C.Name, B.Salary


--формируем заявки на основе штатных единиц
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestTypeId
																				,SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,StaffECSalary
																				,IsUsed
																				,IsDraft
																				,DateSendToApprove
																				,BeginAccountDate
																				,ReasonId
																				,CreatorID)

SELECT 1
			 ,1 
			 ,Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,StaffECSalary
			 ,IsUsed
			 ,0
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
																				,StaffECSalary
																				,IsUsed
																				,BeginAccountDate
																				,[Priority]
																				,CreatorID)
SELECT Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,StaffECSalary
			 ,IsUsed
			 ,BeginAccountDate
			 ,null	--пока не заполняю
			 ,CreatorID
FROM StaffEstablishedPost


