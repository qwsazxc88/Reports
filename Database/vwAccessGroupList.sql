IF OBJECT_ID ('vwAccessGroupList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwAccessGroupList]
GO

CREATE VIEW [dbo].[vwAccessGroupList]
AS

SELECT A.Id as UserId, A.Name as UserName, B.Name as PositionName, dep3.Name as Dep3Name, C.[Name] as Dep7Name, A.DepartmentId, D.Code as AccessGroupCode, D.Name as AccessGroupName,
			 C.Path as UserDepPath, A.Email, E.EndDate
FROM dbo.Users as A
LEFT JOIN dbo.Position as B ON B.Id = A.PositionId
INNER JOIN dbo.Department as C ON C.Id = A.DepartmentId 
LEFT JOIN dbo.Department as dep3 ON C.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
LEFT JOIN AccessGroup as D ON D.Code = A.AccessGroupCode
LEFT JOIN (SELECT UserId, max(EndDate) as EndDate 
					 FROM ChildVacation 
					 WHERE SendTo1C is not null and DeleteDate is null and EndDate >= cast(getdate() as date) 
					 GROUP BY UserId) as E ON E.UserId = A.Id
WHERE A.RoleId = 2 and A.IsActive = 1


GO