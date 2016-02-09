--НЕДОДЕЛАНО
SELECT --A.Id, COUNT(A.Id)
Dep2.Name, Dep3.Name, Dep4.Name, Dep5.Name, Dep6.Name, Dep7.Name, A.SEPId, C.Name as PositionName
--, E.Code, E.Name
,D.Salary, E.Rate, D.Regional, D.Personnel, D.Territory, D.Front, D.Drive, D.North, D.Qualification, D.TotalSalary
--,E.Name
,case when (case when (isnull(E.IsPregnant, 0) = 1 or A.UserId is null) and isnull(A.ReserveType, 0) = 0 then 1 else 0 end) = 1 or (case when isnull(A.DocId, 0) = 0 then 0 else 1 end) = 1
							then (case when (case when A.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end) = 1 
												 then 'Временная вакансия' else 'Вакансия' end) 
							else E.Name end as Surname
--,case when isnull(E.IsPregnant, 0) = 1 then E.Id else G.ReplacedId end as ReplacedId
,case when E.IsPregnant = 1 then isnull(dbo.fnGetReplacedName(null, E.Id, 1), N'(' + E.Name + N')')  else isnull(dbo.fnGetReplacedName(A.Id, null, 1), N'(' + H.Name + N')') end as ReplacedName							 
,dbo.fnGetReplacedName(A.Id, null, 2) TemporaryMovementUsers
,dbo.fnGetReplacedName(A.Id, null, 3) as LongAbsencesUsers
,case when A.ReserveType = 1 then N'Перемещение' 
			when A.ReserveType = 2 then N'Прием'
			when A.ReserveType = 3 then N'Сокращение' end as Reserve
,K.Name as BasicUser
-- *
FROM StaffEstablishedPostUserLinks as A
INNER JOIN StaffEstablishedPost as B ON B.Id = A.SEPId and B.IsUsed = 1
INNER JOIN Department as Dep7 ON Dep7.Id = B.DepartmentId
INNER JOIN Department as Dep6 ON Dep6.Id = Dep7.ParentId
INNER JOIN Department as Dep5 ON Dep5.Id = Dep6.ParentId
INNER JOIN Department as Dep4 ON Dep4.Id = Dep5.ParentId
INNER JOIN Department as Dep3 ON Dep3.Id = Dep4.ParentId and Dep3.Id = 12161
INNER JOIN Department as Dep2 ON Dep2.Id = Dep3.ParentId
INNER JOIN Position as C ON C.Id = B.PositionId
LEFT JOIN vwStaffPostSalary as D ON D.UserLinkId = A.Id --and D.UserId = A.UserId
LEFT JOIN Users as E ON E.Id = A.UserId
LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = A.Id and G.IsUsed = 1
LEFT JOIN Users as H ON H.Id = G.ReplacedId
LEFT JOIN Users as K ON K.RegularUserLinkId = A.Id
WHERE A.IsUsed = 1
--group by A.Id
--having COUNT(A.id) > 1
ORDER BY Dep2.Name, Dep3.Name, Dep4.Name, Dep5.Name, Dep6.Name, Dep7.Name


/*
SELECT --A.Id, COUNT(A.Id)
Dep2.Name, Dep3.Name, Dep4.Name, Dep5.Name, Dep6.Name, Dep7.Name, 
C.Name as PositionName
,A.Name, A.Code, A.DateAccept
FROM Users as A
--INNER JOIN StaffEstablishedPost as B ON B.Id = A.SEPId
INNER JOIN Department as Dep7 ON Dep7.Id = A.DepartmentId
INNER JOIN Department as Dep6 ON Dep6.Id = Dep7.ParentId
INNER JOIN Department as Dep5 ON Dep5.Id = Dep6.ParentId
INNER JOIN Department as Dep4 ON Dep4.Id = Dep5.ParentId
INNER JOIN Department as Dep3 ON Dep3.Id = Dep4.ParentId and Dep3.Id = 4188
INNER JOIN Department as Dep2 ON Dep2.Id = Dep3.ParentId
INNER JOIN Position as C ON C.Id = A.PositionId
--LEFT JOIN vwStaffPostSalary as D ON D.UserLinkId = A.Id --and D.UserId = A.UserId
--inner JOIN Users as E ON E.Id = A.UserId
--LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = A.Id and G.IsUsed = 1
--LEFT JOIN Users as H ON H.Id = G.ReplacedId
WHERE A.IsActive = 1 and A.RoleId & 2 > 0
--group by A.Id
--having COUNT(A.id) > 1
ORDER BY Dep2.Name, Dep3.Name, Dep4.Name, Dep5.Name, Dep6.Name, Dep7.Name
*/

--select * from StaffEstablishedPostUserLinks as a WHERE A.UserId is not null and A.IsUsed = 1 and a.IsDismissal = 0
--8088

--select * from Users where Id = 26144

--select * from Department where Name = 'Кредитно-кассовый офис "Пензенский" г. Пенза'
--select * from EmploymentCandidate where UserId = 26144
--select * from Managers where Id = 3564
--select * from StaffDepartmentTaxDetails where DepartmentId in (11379, 12090, 12093, 12315)


--select * from Department where (Name like '%дирекция%' or name = 'ауп мф') and ItemLevel = 3
--4189 - Дирекция ДАЛЬНЕВОСТОЧНАЯ
--12161 - ЮГО-ЗАПАДНАЯ дирекция
--4175 - АУП МФ
--4188 - Дирекция ВОСТОЧНО-СИБИРСКАЯ