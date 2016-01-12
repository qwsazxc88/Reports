IF OBJECT_ID ('vwStaffListDepartmentManagers', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffListDepartmentManagers]
GO


--список руководителей подразделений и их замов
CREATE VIEW [dbo].[vwStaffListDepartmentManagers]
AS
SELECT B.Id, B.Name, B.PositionId, B.DepartmentId, B.IsMainManager, B.Level, A.IsPregnant
FROM Users as A
INNER JOIN Users as B ON B.Email = A.Email and B.RoleId & 4 > 0 and B.IsActive = 1--and B.Login = A.Login + N'R'
WHERE A.IsActive = 1 and A.RoleId & 2 > 0
GROUP BY B.Id, B.Name, B.PositionId, B.DepartmentId, B.IsMainManager, B.Level, A.IsPregnant

GO