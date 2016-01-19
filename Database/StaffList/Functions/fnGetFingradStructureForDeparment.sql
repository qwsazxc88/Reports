IF OBJECT_ID ('fnGetFingradStructureForDeparment', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetFingradStructureForDeparment]
GO

--функция достает структуру финграда для подразделения + соответствия структуры СКД
CREATE FUNCTION [dbo].[fnGetFingradStructureForDeparment]
(
	@DepartmentId int	--родительское подразделение
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
	,RPLinkCode nvarchar(20)
	,RPLinkName nvarchar(250)
	,RPLinkNameSKD nvarchar(250)
	,BGCode nvarchar(20)
	,BGName nvarchar(250)
	,BGNameSKD nvarchar(250)
	,AdminCode nvarchar(20)
	,AdminName nvarchar(250)
	,AdminNameSKD nvarchar(250)
	,ManagementCode nvarchar(20)
	,ManagementName nvarchar(250)
	,ManagementNameSKD nvarchar(250)
)
AS
BEGIN
DECLARE 
	@ItemLevel int
	
	SELECT @ItemLevel = ItemLevel FROM Department WHERE Id = @DepartmentId

	IF @ItemLevel = 6
		INSERT INTO @ReturnTable
		SELECT A.Id
					,B.Code as RPLinkCode, B.Name as RPLinkName, A.Name as RPLinkNameSKD
					,BB.Code as BGCode, BB.Name as BGName, B.Name as BGNameSKD
					,CC.Code as AdminCode, CC.Name as AdminName, C.Name as AdminNameSKD
					,DD.Code as ManagementCode, DD.Name as ManagementName, D.Name as ManagementNameSKD
		FROM Department as A
		LEFT JOIN StaffDepartmentRPLink as AA ON AA.DepartmentId = A.Id
		INNER JOIN Department as B ON B.Id = A.ParentId
		LEFT JOIN StaffDepartmentBusinessGroup as BB ON BB.DepartmentId = B.Id
		INNER JOIN Department as C ON C.Id = B.ParentId
		LEFT JOIN StaffDepartmentAdministration as CC ON CC.DepartmentId = C.Id
		LEFT JOIN Department as D ON D.Id = C.ParentId
		LEFT JOIN StaffDepartmentManagement as DD ON DD.DepartmentId = D.Id
		WHERE A.Id = @DepartmentId


	IF @ItemLevel = 5
		INSERT INTO @ReturnTable
		SELECT B.Id
					,null as RPLinkCode, null as RPLinkName, null as RPLinkNameSKD
					,BB.Code as BGCode, BB.Name as BGName, B.Name as BGNameSKD
					,CC.Code as AdminCode, CC.Name as AdminName, C.Name as AdminNameSKD
					,DD.Code as ManagementCode, DD.Name as ManagementName, D.Name as ManagementNameSKD
		FROM Department as B
		LEFT JOIN StaffDepartmentBusinessGroup as BB ON BB.DepartmentId = B.Id
		INNER JOIN Department as C ON C.Id = B.ParentId
		LEFT JOIN StaffDepartmentAdministration as CC ON CC.DepartmentId = C.Id
		LEFT JOIN Department as D ON D.Id = C.ParentId
		LEFT JOIN StaffDepartmentManagement as DD ON DD.DepartmentId = D.Id
		WHERE B.Id = @DepartmentId


	IF @ItemLevel = 4
		INSERT INTO @ReturnTable
		SELECT C.Id
					,null as RPLinkCode, null as RPLinkName, null as RPLinkNameSKD
					,null as BGCode, null as BGName, null as BGNameSKD
					,CC.Code as AdminCode, CC.Name as AdminName, C.Name as AdminNameSKD
					,DD.Code as ManagementCode, DD.Name as ManagementName, D.Name as ManagementNameSKD
		FROM Department as C
		LEFT JOIN StaffDepartmentAdministration as CC ON CC.DepartmentId = C.Id
		LEFT JOIN Department as D ON D.Id = C.ParentId
		LEFT JOIN StaffDepartmentManagement as DD ON DD.DepartmentId = D.Id
		WHERE C.Id = @DepartmentId


	IF @ItemLevel = 3
		INSERT INTO @ReturnTable
		SELECT D.Id
					 ,null as RPLinkCode, null as RPLinkName, null as RPLinkNameSKD
					 ,null as BGCode, null as BGName, null as BGNameSKD
					 ,null as AdminCode, null as AdminName, null as AdminNameSKD
					 ,DD.Code as ManagementCode, DD.Name as ManagementName, D.Name as ManagementNameSKD
		FROM Department as D
		LEFT JOIN StaffDepartmentManagement as DD ON DD.DepartmentId = D.Id
		WHERE D.Id = @DepartmentId		
--select * from dbo.fnGetFingradStructureForDeparment(4839) 

	RETURN 
END

GO


