IF OBJECT_ID ('vwStaffListDepartment', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffListDepartment]
GO

--состояние заполнения анкет
CREATE VIEW [dbo].[vwStaffListDepartment]
AS
SELECT A.Id, A.Code, A.Name, A.Code1C, A.ParentId, A.Path, A.ItemLevel, A.CodeSKD, A.Priority, 
			 case when A.ItemLevel = 2 then D.Name 
						when A.ItemLevel = 3 then E.Name 
						when A.ItemLevel = 4 then F.Name 
						when A.ItemLevel = 5 then G.Name 
						when A.ItemLevel = 6 then H.Name
						else C.NameShort end as DepFingradName, 
			 --C.NameComment as DepFingradNameComment, 
			 case A.BFGId when 1 then 'Бэк'
										when 2 then 'Фронт'
										when 3 then 'ГПД'
										when 4 then 'Управленческое' end as DepFingradNameComment,
			 C.DepCode as FinDepPointCode
FROM Department as A
LEFT JOIN StaffDepartmentRequest as B ON B.DepartmentId = A.Id and B.IsUsed = 1 
LEFT JOIN StaffDepartmentManagerDetails as C ON C.DepRequestId = B.Id
--далее линкуются таблицы справочника кодировок
LEFT JOIN StaffDepartmentBranch as D ON D.DepartmentId = A.Id
LEFT JOIN StaffDepartmentManagement as E ON E.DepartmentId = A.Id 
LEFT JOIN StaffDepartmentAdministration as F ON F.DepartmentId = A.Id 
LEFT JOIN StaffDepartmentBusinessGroup as G ON G.DepartmentId = A.Id 
LEFT JOIN StaffDepartmentRPLink as H ON H.DepartmentId = A.Id 
GO