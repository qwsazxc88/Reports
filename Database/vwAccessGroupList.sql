IF OBJECT_ID ('vwAccessGroupList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwAccessGroupList]
GO

CREATE VIEW [dbo].[vwAccessGroupList]
AS

SELECT A.Id as UserId, A.Name as UserName, B.Name as PositionName, dep3.Name as Dep3Name, C.[Name] as Dep7Name, A.DepartmentId, D.Code as AccessGroupCode, D.Name as AccessGroupName,
			 C.Path as UserDepPath, A.Email, E.EndDate, User6.Manager as Manager6, User5.Manager as Manager5, User4.Manager as Manager4
FROM dbo.Users as A
LEFT JOIN dbo.Position as B ON B.Id = A.PositionId
INNER JOIN dbo.Department as C ON C.Id = A.DepartmentId 
LEFT JOIN dbo.Department as dep3 ON C.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
LEFT JOIN AccessGroup as D ON D.Code = A.AccessGroupCode
LEFT JOIN (SELECT UserId, max(EndDate) as EndDate 
					 FROM ChildVacation 
					 WHERE SendTo1C is not null and DeleteDate is null and EndDate >= cast(getdate() as date) 
					 GROUP BY UserId) as E ON E.UserId = A.Id
LEFT JOIN dbo.Department as dep6 ON C.[Path] like dep6.[Path] + N'%' and dep6.ItemLevel = 6 
LEFT JOIN dbo.Department as dep5 ON C.[Path] like dep5.[Path] + N'%' and dep5.ItemLevel = 5 
LEFT JOIN dbo.Department as dep4 ON C.[Path] like dep4.[Path] + N'%' and dep4.ItemLevel = 4 
LEFT JOIN (SELECT Id, Manager FROM dbo.fnGetManagerFromDepartment(6)) as User6 ON User6.Id = dep6.Id
LEFT JOIN (SELECT Id, Manager FROM dbo.fnGetManagerFromDepartment(5)) as User5 ON User5.Id = dep5.Id
LEFT JOIN (SELECT Id, Manager FROM dbo.fnGetManagerFromDepartment(4)) as User4 ON User4.Id = dep4.Id
WHERE (A.RoleId = 2 or EXISTS(SELECT Id FROM dbo.fnGetRolesFromBiteCode(A.RoleId) WHERE Id = 2)) and A.IsActive = 1 


GO