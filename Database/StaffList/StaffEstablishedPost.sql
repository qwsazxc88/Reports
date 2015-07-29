--СКРИПТ ФОРМИРУЕТ ШТАТНЫЕ ЕДИНИЦЫ И ЗАЯВКИ НА ИХ СОЗДАНИЕ
--кусок для определения руководителей подрзделений, взято из скрипта по обработке данных из финграда
SELECT * INTO #TMP4
FROM Users as A WHERE A.RoleId = 4 and A.IsMainManager = 1 and A.IsActive = 1
--отключаем беременных
and not exists (SELECT * FROM ChildVacation WHERE getdate() between BeginDate and EndDate and UserId in (SELECT id FROM Users WHERE RoleId = 2 and IsActive = 1 and Email = A.Email)) 

SELECT Id, UserId
INTO #TMP5
FROM(--по дирекциям
			SELECT A.Id, A.ParentId, A.ItemLevel ,isnull(C.Id, isnull(E.id, isnull(G.id, isnull(I.id, K.Id)))) as UserId
			FROM Department as A
			INNER JOIN Department as B ON B.Code1C = A.ParentId and B.ItemLevel = 6
			LEFT JOIN #TMP4 as C ON C.DepartmentId = B.id and C.Level = 6
			INNER JOIN Department as D ON D.Code1C = B.ParentId and D.ItemLevel = 5
			LEFT JOIN #TMP4 as E ON E.DepartmentId = D.id and E.Level = 5
			INNER JOIN Department as F ON F.Code1C = D.ParentId and F.ItemLevel = 4
			LEFT JOIN #TMP4 as G ON G.DepartmentId = F.id and G.Level = 4
			INNER JOIN Department as H ON H.Code1C = F.ParentId and H.ItemLevel = 3
			LEFT JOIN #TMP4 as I ON I.DepartmentId = H.id and I.Level = 3
			INNER JOIN Department as J ON J.Code1C = H.ParentId and J.ItemLevel = 2
			LEFT JOIN #TMP4 as K ON K.DepartmentId = J.id and K.Level = 2
			WHERE isnull(C.Id, isnull(E.id, isnull(G.id, isnull(I.id, K.Id)))) is not null
			UNION ALL
			--по ручным привязкам
			SELECT A.Id, A.ParentId, A.ItemLevel 
							,isnull(B6.UserId, isnull(B5.UserId, isnull(B4.UserId, isnull(B3.UserId, B2.UserId)))) as UserId
			FROM Department as A
			INNER JOIN Department as B ON B.Code1C = A.ParentId and B.ItemLevel = 6
				LEFT JOIN ManualRoleRecord as B6 ON B6.TargetDepartmentId = B.Id
			LEFT JOIN #TMP4 as C ON C.DepartmentId = B.id and C.Level = 6
			INNER JOIN Department as D ON D.Code1C = B.ParentId and D.ItemLevel = 5
				LEFT JOIN ManualRoleRecord as B5 ON B5.TargetDepartmentId = D.Id
			LEFT JOIN #TMP4 as E ON E.DepartmentId = D.id and E.Level = 5
			INNER JOIN Department as F ON F.Code1C = D.ParentId and F.ItemLevel = 4
				LEFT JOIN ManualRoleRecord as B4 ON B4.TargetDepartmentId = F.Id
			LEFT JOIN #TMP4 as G ON G.DepartmentId = F.id and G.Level = 4
			INNER JOIN Department as H ON H.Code1C = F.ParentId and H.ItemLevel = 3
				LEFT JOIN ManualRoleRecord as B3 ON B3.TargetDepartmentId = H.Id
			LEFT JOIN #TMP4 as I ON I.DepartmentId = H.id and I.Level = 3
			INNER JOIN Department as J ON J.Code1C = H.ParentId and J.ItemLevel = 2
				LEFT JOIN ManualRoleRecord as B2 ON B2.TargetDepartmentId = J.Id
			LEFT JOIN #TMP4 as K ON K.DepartmentId = J.id and K.Level = 2
			WHERE isnull(C.Id, isnull(E.id, isnull(G.id, isnull(I.id, K.Id)))) is null) as A

--формируем заявки
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
			 ,null	--причину пока не указываю
			 ,(select top 1 cc.Id
					from StaffEstablishedPostRequest as aa
					inner join #TMP5 as bb on bb.id = aa.DepartmentId
					inner join Users as CC on cc.id = bb.userId and (cc.RoleId & 4) > 0
					where aa.DepartmentId = A.id
					order by cc.IsMainManager desc)
FROM Department as A
INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1 and (RoleId & 2) > 0
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
			 ,CreatorID
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
			 ,CreatorID
FROM StaffEstablishedPost


DROP TABLE #TMP4
DROP TABLE #TMP5