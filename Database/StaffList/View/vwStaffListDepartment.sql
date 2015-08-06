IF OBJECT_ID ('vwEmploymentFillState', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentFillState]
GO

--состояние заполнения анкет
CREATE VIEW [dbo].[vwEmploymentFillState]
AS
SELECT A.Id, A.Code, A.Name, A.Code1C, A.ParentId, A.Path, A.ItemLevel, A.CodeSKD, A.Priority, C.NameShort as DepFingradName, C.NameComment as DepFingradNameComment, C.DepCode as FinDepPointCode
FROM Department as A
LEFT JOIN StaffDepartmentRequest as B ON B.DepartmentId = A.Id and B.IsUsed = 1 
LEFT JOIN StaffDepartmentManagerDetails as C ON C.DepRequestId = B.Id
--INNER JOIN Department as C ON C.id = A.DepartmentId
GO