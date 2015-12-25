	SELECT M.Name as Dep2, L.Name as Dep3, K.Name as Dep4, J.Name as Dep5, I.Name as Dep6, C.Name as Dep7, A.DepartmentId,
				 F.Id, A.Id as SEPId, B.Code as PositionCode, B.Name as PositionName, A.Salary,
				 isnull(E.Rate, 0) as Rate	--ставка
				 ,isnull(N.Regional, 0) as Regional
				 ,isnull(N.Personnel, 0) as Personnel
				 ,isnull(N.Territory, 0) as Territory
				 ,isnull(N.Front, 0) as Front
				 ,isnull(N.Drive, 0) as Drive
				 ,isnull(case when N.NorthAuto = 0 then N.North else N.NorthAuto end, 0) as North
				 ,isnull(N.Qualification, 0) as Qualification
				 ,isnull(case when E.IsPregnant = 1 then null else E.Code end, '') as UserId, 
				 case when (case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) and isnull(F.ReserveType, 0) = 0 then 1 else 0 end) = 1 or (case when isnull(F.DocId, 0) = 0 then 0 else 1 end) = 1
							then (case when (case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end) = 1 
												 then 'Временная вакансия' else 'Вакансия' end) 
							else E.Name end as Surname, 
				 isnull(case when E.IsPregnant = 1 then E.Code else H.Code end, '') as ReplacedId
				 ,isnull(case when E.IsPregnant = 1 then isnull(dbo.fnGetReplacedName(null, E.Id), E.Name)  else isnull(dbo.fnGetReplacedName(F.Id, null), H.Name) end, '') as ReplacedName
	FROM StaffEstablishedPost as A
	INNER JOIN Position as B ON B.Id = A.PositionId
	INNER JOIN Department as C ON C.Id = A.DepartmentId
	INNER JOIN StaffEstablishedPostRequest as D ON D.SEPId = A.Id and D.IsUsed = 1
	INNER JOIN StaffEstablishedPostUserLinks as F ON F.SEPId = A.Id and F.IsUsed = 1
	LEFT JOIN Users as E ON E.Id = F.UserId and E.IsActive = 1 and E.RoleId & 2 > 0 --and E.IsPregnant = 0
	LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = F.Id and F.IsUsed = 1
	LEFT JOIN Users as H ON H.Id = G.ReplacedId
	INNER JOIN Department as I ON C.ParentId = I.Id
	INNER JOIN Department as J ON I.ParentId = J.Id
	INNER JOIN Department as K ON J.ParentId = K.Id
	INNER JOIN Department as L ON K.ParentId = L.Id
	INNER JOIN Department as M ON L.ParentId = M.Id
	LEFT JOIN vwStaffPostSalary as N ON N.UserId = E.Id
	WHERE A.IsUsed = 1 
	ORDER BY M.Name, L.Name, K.Name, J.Name, I.Name, C.Name, B.Name,
					 case when (case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) and isnull(F.ReserveType, 0) = 0 then 1 else 0 end) = 1 or (case when isnull(F.DocId, 0) = 0 then 0 else 1 end) = 1
							then (case when (case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end) = 1 
												 then 'Временная вакансия' else 'Вакансия' end) 
							else E.Name end