IF OBJECT_ID ('vwDepartmentToPersonnels', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwDepartmentToPersonnels]
GO



--опредялем доступ кадровиков по ID одного из них
CREATE VIEW [dbo].[vwDepartmentToPersonnels]
AS
SELECT     D.Id AS DepartmentId, A.PersonnelId
FROM         dbo.UserToPersonnel AS A INNER JOIN
                      dbo.Users AS B ON B.Id = A.UserId INNER JOIN
                      dbo.Department AS C ON C.Id = B.DepartmentId INNER JOIN
                      dbo.Department AS D ON C.Path LIKE D.Path + N'%'
WHERE     (B.IsActive = 1)
GROUP BY D.Id, A.PersonnelId



GO


