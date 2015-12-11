IF OBJECT_ID ('fnGetFingradStructureForDeparment', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetFingradStructureForDeparment]
GO

--функция достает структуру финграда для подразделения
CREATE FUNCTION [dbo].[fnGetFingradStructureForDeparment]
(
	@DepartmentId int	--родительское подразделение
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
	,Name nvarchar(250)
	,RPLinkCode nvarchar(20)
	,RPLinkName nvarchar(250)
	,BGCode nvarchar(20)
	,BGName nvarchar(250)
	,AdminCode nvarchar(20)
	,AdminName nvarchar(250)
	,ManagementCode nvarchar(20)
	,ManagementName nvarchar(250)
)
AS
BEGIN
DECLARE 
	@ItemLevel int
	
	SELECT @ItemLevel = ItemLevel FROM Department WHERE Id = @DepartmentId

	IF @ItemLevel = 6
		INSERT INTO @ReturnTable
		SELECT A.Id, A.Name, B.Code as RPLinkCode, B.Name as RPLinkName, C.Code as BGCode, C.Name as BGName, D.Code as AdminCode, D.Name as AdminName, E.Code as ManagementCode, E.Name as ManagementName
		FROM Department as A
		LEFT JOIN StaffDepartmentRPLink as B ON B.DepartmentId = A.Id
		LEFT JOIN StaffDepartmentBusinessGroup as C ON C.Id = B.BGId
		LEFT JOIN StaffDepartmentAdministration as D ON D.Id = C.AdminId
		LEFT JOIN StaffDepartmentManagement as E ON E.Id = D.ManagementId
		WHERE A.Id = @DepartmentId


	IF @ItemLevel = 5
		INSERT INTO @ReturnTable
		SELECT A.Id, A.Name, null as RPLinkCode, null as RPLinkName, C.Code as BGCode, C.Name as BGName, D.Code as AdminCode, D.Name as AdminName, E.Code as ManagementCode, E.Name as ManagementName
		FROM Department as A
		LEFT JOIN StaffDepartmentBusinessGroup as C ON C.DepartmentId = A.Id
		LEFT JOIN StaffDepartmentAdministration as D ON D.Id = C.AdminId
		LEFT JOIN StaffDepartmentManagement as E ON E.Id = D.ManagementId
		WHERE A.Id = @DepartmentId


	IF @ItemLevel = 4
		INSERT INTO @ReturnTable
		SELECT A.Id, A.Name, null as RPLinkCode, null as RPLinkName, null as BGCode, null as BGName, D.Code as AdminCode, D.Name as AdminName, E.Code as ManagementCode, E.Name as ManagementName
		FROM Department as A
		LEFT JOIN StaffDepartmentAdministration as D ON D.DepartmentId = A.Id
		LEFT JOIN StaffDepartmentManagement as E ON E.Id = D.ManagementId
		WHERE A.Id = @DepartmentId


	IF @ItemLevel = 3
		INSERT INTO @ReturnTable
		SELECT A.Id, A.Name, null as RPLinkCode, null as RPLinkName, null as BGCode, null as BGName, null as AdminCode, null as AdminName, E.Code as ManagementCode, E.Name as ManagementName
		FROM Department as A
		LEFT JOIN StaffDepartmentManagement as E ON E.DepartmentId = A.Id
		WHERE A.Id = @DepartmentId		
--select * from dbo.fnGetFingradStructureForDeparment(4839) 

	RETURN 
END

GO


