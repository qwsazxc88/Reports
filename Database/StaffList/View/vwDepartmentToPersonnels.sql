IF OBJECT_ID ('vwEmploymentPersonnels', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentPersonnels]
GO



--опредялем доступ кадровиков по ID одного из них
CREATE VIEW [dbo].[vwEmploymentPersonnels]
AS
SELECT A.Id as UserId, D.Id as PersonnelId
FROM Users as A
INNER JOIN UserAccessGroup as B ON B.UserCode = A.Code
INNER JOIN UserAccessGroup as C ON C.AccessGroupCode = B.AccessGroupCode
INNER JOIN Users as D ON D.Code = C.UserCode



GO


