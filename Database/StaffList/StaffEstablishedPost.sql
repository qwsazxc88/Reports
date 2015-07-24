--СКРИПТ ФОРМИРУЕТ ШТАТНЫЕ ЕДИНИЦЫ И ЗАЯВКИ НА ИХ СОЗДАНИЕ

--формируем заявки
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestType
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
			 ,1 -- пока справочника нет, ставлю 1 как заявка на создание штатной единицы
			 ,null	--это поле для ссылок на существующие штатные единициы (заявки на изменение)
			 ,B.PositionId
			 ,A.Id
			 ,count(B.PositionId)
			 ,isnull(B.Salary, 0)
			 ,0
			 ,1
			 ,0
			 ,getdate()
			 ,getdate()
			 ,null	--пичину пока не указываю
			 ,null	--создателя тоже
FROM Department as A
INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1
INNER JOIN Position as C ON C.id = B.PositionId
GROUP BY A.Id, B.PositionId, C.Name, B.Salary



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
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,StaffECSalary
			 ,IsUsed
			 ,BeginAccountDate
			 ,null	--пока не заполняю
			 ,null	--тоже
FROM StaffEstablishedPostRequest



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
			 ,null	--тоже
FROM StaffEstablishedPost