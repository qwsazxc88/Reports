--НЕДОДЕЛАНО
SELECT --A.Id, COUNT(A.Id)
Dep2.Name, Dep3.Name, Dep4.Name, Dep5.Name, Dep6.Name, Dep7.Name, A.SEPId, C.Name as PositionName
, E.Code, E.Name
,D.Salary, E.Rate, D.Regional, D.Personnel, D.Territory, D.Front, D.Drive, D.North, D.Qualification, D.TotalSalary
,E.Name
,case when (case when (isnull(E.IsPregnant, 0) = 1 or A.UserId is null) and isnull(A.ReserveType, 0) = 0 then 1 else 0 end) = 1 or (case when isnull(A.DocId, 0) = 0 then 0 else 1 end) = 1
							then (case when (case when A.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end) = 1 
												 then 'Временная вакансия' else 'Вакансия' end) 
							else E.Name end as Surname
,case when isnull(E.IsPregnant, 0) = 1 then E.Id else G.ReplacedId end as ReplacedId
,case when E.IsPregnant = 1 then isnull(dbo.fnGetReplacedName(null, E.Id), N'(' + E.Name + N')')  else isnull(dbo.fnGetReplacedName(A.Id, null), N'(' + H.Name + N')') end as ReplacedName							 
--, *
FROM StaffEstablishedPostUserLinks as A
INNER JOIN StaffEstablishedPost as B ON B.Id = A.SEPId
INNER JOIN Department as Dep7 ON Dep7.Id = B.DepartmentId
INNER JOIN Department as Dep6 ON Dep6.Id = Dep7.ParentId
INNER JOIN Department as Dep5 ON Dep5.Id = Dep6.ParentId
INNER JOIN Department as Dep4 ON Dep4.Id = Dep5.ParentId
INNER JOIN Department as Dep3 ON Dep3.Id = Dep4.ParentId
INNER JOIN Department as Dep2 ON Dep2.Id = Dep3.ParentId
INNER JOIN Position as C ON C.Id = B.PositionId
LEFT JOIN vwStaffPostSalary as D ON D.UserLinkId = A.Id --and D.UserId = A.UserId
LEFT JOIN Users as E ON E.Id = A.UserId
LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = A.Id and G.IsUsed = 1
LEFT JOIN Users as H ON H.Id = G.ReplacedId
WHERE A.IsUsed = 1
--group by A.Id
--having COUNT(A.id) > 1
ORDER BY Dep2.Name, Dep3.Name, Dep4.Name, Dep5.Name, Dep6.Name, Dep7.Name

--select * from StaffEstablishedPostUserLinks as a WHERE A.UserId is not null and A.IsUsed = 1 and a.IsDismissal = 0
--8088

--select * from Users where Id = 26144

--select * from Department where Name = 'Кредитно-кассовый офис "Пензенский" г. Пенза'
--select * from EmploymentCandidate where UserId = 26144
--select * from Managers where Id = 3564
--select * from StaffDepartmentTaxDetails where DepartmentId in (11379, 12090, 12093, 12315)