--������ ������� ��������� ���� ������ ��� ������� �������� ����������, ������� ������� ���� � ��������� ����� ����������� ���������� �������
--������ �� ������ ��������� ��� ������������ ������������, ������ ����������� ����� ������� ����������� ��� �������� ����������
--RETURN
use WebAppTest
go

--1. �������� ������
--��� ��� ����������� ���������� ������������� ���������, ������ ����� ��� ������ ������ 
IF OBJECT_ID ('FK_StaffMovements_SourceUserLink', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffMovements] DROP CONSTRAINT [FK_StaffMovements_SourceUserLink]
GO
IF OBJECT_ID ('FK_StaffMovements_TargetUserLink', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffMovements] DROP CONSTRAINT [FK_StaffMovements_TargetUserLink]
GO

IF OBJECT_ID ('FK_StaffMovementsFact_StaffEstablishedPostRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffMovementsFact] DROP CONSTRAINT [FK_StaffMovementsFact_StaffEstablishedPostRequest]
GO


UPDATE StaffMovements SET SourceStaffEstablishedPostRequest = null, TargetStaffEstablishedPostRequest = null

DELETE FROM DocumentApproval WHERE ApprovalType in (1, 2)

DBCC CHECKIDENT ('DocumentApproval');
GO

--��� ������� ������������
UPDATE Users SET SEPId = null
IF OBJECT_ID ('FK_Users_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_StaffEstablishedPost]
GO


IF OBJECT_ID ('FK_Department_StaffDepartmentAccessory', 'F') IS NOT NULL
	ALTER TABLE [dbo].[Department] DROP CONSTRAINT [FK_Department_StaffDepartmentAccessory]
GO


IF OBJECT_ID ('FK_RefAddresses_Editors', 'F') IS NOT NULL
	ALTER TABLE [dbo].[RefAddresses] DROP CONSTRAINT [FK_RefAddresses_Editors]
GO

IF OBJECT_ID ('FK_RefAddresses_Creators', 'F') IS NOT NULL
	ALTER TABLE [dbo].[RefAddresses] DROP CONSTRAINT [FK_RefAddresses_Creators]
GO


IF OBJECT_ID ('FK_StaffExtraCharges_StaffUnitReference', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffExtraCharges] DROP CONSTRAINT [FK_StaffExtraCharges_StaffUnitReference]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostUserLinks_Users', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] DROP CONSTRAINT [FK_StaffEstablishedPostUserLinks_Users]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostUserLinks_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] DROP CONSTRAINT [FK_StaffEstablishedPostUserLinks_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostUserLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] DROP CONSTRAINT [FK_StaffEstablishedPostUserLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostUserLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] DROP CONSTRAINT [FK_StaffEstablishedPostUserLinks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffPostReplacement_User', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostReplacement] DROP CONSTRAINT [FK_StaffPostReplacement_User]
GO

IF OBJECT_ID ('FK_StaffPostReplacement_StaffEstablishedPostUserLinks', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostReplacement] DROP CONSTRAINT [FK_StaffPostReplacement_StaffEstablishedPostUserLinks]
GO

IF OBJECT_ID ('FK_StaffPostReplacement_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostReplacement] DROP CONSTRAINT [FK_StaffPostReplacement_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffPostReplacement_Replaced', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostReplacement] DROP CONSTRAINT [FK_StaffPostReplacement_Replaced]
GO

IF OBJECT_ID ('FK_StaffPostReplacement_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostReplacement] DROP CONSTRAINT [FK_StaffPostReplacement_EditorUser]
GO

IF OBJECT_ID ('FK_StaffPostReplacement_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostReplacement] DROP CONSTRAINT [FK_StaffPostReplacement_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffRequestPyrusTasks_StaffEstablishedPostRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffRequestPyrusTasks] DROP CONSTRAINT [FK_StaffRequestPyrusTasks_StaffEstablishedPostRequest]
GO

IF OBJECT_ID ('FK_StaffRequestPyrusTasks_StaffDepartmentRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffRequestPyrusTasks] DROP CONSTRAINT [FK_StaffRequestPyrusTasks_StaffDepartmentRequest]
GO

IF OBJECT_ID ('FK_StaffRequestPyrusTasks_DocumentApproval', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffRequestPyrusTasks] DROP CONSTRAINT [FK_StaffRequestPyrusTasks_DocumentApproval]
GO

IF OBJECT_ID ('FK_StaffRequestPyrusTasks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffRequestPyrusTasks] DROP CONSTRAINT [FK_StaffRequestPyrusTasks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperations_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperations] DROP CONSTRAINT [FK_StaffDepartmentOperations_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperations_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperations] DROP CONSTRAINT [FK_StaffDepartmentOperations_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationGroups_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationGroups] DROP CONSTRAINT [FK_StaffDepartmentOperationGroups_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationGroups_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationGroups] DROP CONSTRAINT [FK_StaffDepartmentOperationGroups_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentRPLink_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRPLink] DROP CONSTRAINT [FK_StaffDepartmentRPLink_Department]
GO

IF OBJECT_ID ('FK_StaffDepartmentRPLink_StaffDepartmentBusinessGroup', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRPLink] DROP CONSTRAINT [FK_StaffDepartmentRPLink_StaffDepartmentBusinessGroup]
GO

IF OBJECT_ID ('FK_StaffDepartmentRPLink_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRPLink] DROP CONSTRAINT [FK_StaffDepartmentRPLink_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentRPLink_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRPLink] DROP CONSTRAINT [FK_StaffDepartmentRPLink_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentBusinessGroup_StaffDepartmentAdministration', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] DROP CONSTRAINT [FK_StaffDepartmentBusinessGroup_StaffDepartmentAdministration]
GO

IF OBJECT_ID ('FK_StaffDepartmentBusinessGroup_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] DROP CONSTRAINT [FK_StaffDepartmentBusinessGroup_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentBusinessGroup_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] DROP CONSTRAINT [FK_StaffDepartmentBusinessGroup_Department]
GO

IF OBJECT_ID ('FK_StaffDepartmentBusinessGroup_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] DROP CONSTRAINT [FK_StaffDepartmentBusinessGroup_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentAdministration_StaffDepartmentManagement', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentAdministration] DROP CONSTRAINT [FK_StaffDepartmentAdministration_StaffDepartmentManagement]
GO

IF OBJECT_ID ('FK_StaffDepartmentAdministration_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentAdministration] DROP CONSTRAINT [FK_StaffDepartmentAdministration_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentAdministration_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentAdministration] DROP CONSTRAINT [FK_StaffDepartmentAdministration_Department]
GO

IF OBJECT_ID ('FK_StaffDepartmentAdministration_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentAdministration] DROP CONSTRAINT [FK_StaffDepartmentAdministration_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagement_StaffDepartmentBranch', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagement] DROP CONSTRAINT [FK_StaffDepartmentManagement_StaffDepartmentBranch]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagement_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagement] DROP CONSTRAINT [FK_StaffDepartmentManagement_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagement_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagement] DROP CONSTRAINT [FK_StaffDepartmentManagement_Department]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagement_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagement] DROP CONSTRAINT [FK_StaffDepartmentManagement_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentBranch_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentBranch] DROP CONSTRAINT [FK_StaffDepartmentBranch_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentBranch_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentBranch] DROP CONSTRAINT [FK_StaffDepartmentBranch_Department]
GO

IF OBJECT_ID ('FK_StaffDepartmentBranch_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentBranch] DROP CONSTRAINT [FK_StaffDepartmentBranch_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_StaffProgramReference', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_StaffProgramReference]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_StaffDepartmentManagerDetails', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_StaffDepartmentManagerDetails]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_EditorUser]
GO

IF OBJECT_ID ('FK_StaffProgramCodes_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffProgramCodes] DROP CONSTRAINT [FK_StaffProgramCodes_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_StaffWorkingConditions', 'F') IS NOT NULL	
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_StaffWorkingConditions]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_Schedule', 'F') IS NOT NULL	
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_Schedule]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes', 'F') IS NOT NULL	
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_StaffEstablishedPost', 'F') IS NOT NULL	
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_Position', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_Position]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_EditorUsers', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_EditorUsers]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_Department]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostRequest_AppointmentReason', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostRequest] DROP CONSTRAINT [FK_StaffEstablishedPostRequest_AppointmentReason]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_Position', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_Position]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_Department]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostArchive_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostArchive] DROP CONSTRAINT [FK_StaffEstablishedPostArchive_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_Position', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_Position]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_EditorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_Department]
GO

IF OBJECT_ID ('FK_StaffEstablishedPost_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPost] DROP CONSTRAINT [FK_StaffEstablishedPost_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_StaffDepartmentRequestTypes', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentRequestTypes]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_StaffDepartmentAccessory', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentAccessory]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_RefAddresses', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_RefAddresses]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_DepartmentDeposit', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_DepartmentDeposit]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_DepartmentParent', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_DepartmentParent]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_DepartmentNext', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_DepartmentNext]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_Department]
GO

IF OBJECT_ID ('FK_StaffDepartmentRequest_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentRequest] DROP CONSTRAINT [FK_StaffDepartmentRequest_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_StaffDepartmentOperations', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperations]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_StaffDepartmentOperationGroups', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperationGroups]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationLinks] DROP CONSTRAINT [FK_StaffDepartmentOperationLinks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentTypes', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentTypes]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentOperationGroups', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentOperationGroups]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentSoftGroup', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentSoftGroup]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentSKB_GE', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentSKB_GE]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentRentPlace', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRentPlace]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentCashDeskAvailable', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentCashDeskAvailable]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffNetShopIdentification', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffNetShopIdentification]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentReasons', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentReasons]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_StaffDepartmentRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRequest]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_RefAddresses', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_RefAddresses]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentManagerDetails_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentManagerDetails] DROP CONSTRAINT [FK_StaffDepartmentManagerDetails_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_StaffLandmarkTypes', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_StaffLandmarkTypes]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentLandmarks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentLandmarks] DROP CONSTRAINT [FK_StaffDepartmentLandmarks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_StaffDepartmentRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_StaffDepartmentRequest]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_DepCashin', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_DepCashin]
GO

IF OBJECT_ID ('FK_StaffDepartmentCBDetails_DepATM', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentCBDetails] DROP CONSTRAINT [FK_StaffDepartmentCBDetails_DepATM]
GO

IF OBJECT_ID ('FK_DepartmentArchive_Department', 'F') IS NOT NULL
	ALTER TABLE [dbo].[DepartmentArchive] DROP CONSTRAINT [FK_DepartmentArchive_Department]
GO

IF OBJECT_ID ('FK_DepartmentArchive_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[DepartmentArchive] DROP CONSTRAINT [FK_DepartmentArchive_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_StaffMovements', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_StaffMovements]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_StaffExtraCharges', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraCharges]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_StaffExtraChargeActions', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraChargeActions]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_Staff', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_Staff]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffPostChargeLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffPostChargeLinks] DROP CONSTRAINT [FK_StaffPostChargeLinks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationModes] DROP CONSTRAINT [FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationModes_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationModes] DROP CONSTRAINT [FK_StaffDepartmentOperationModes_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentOperationModes_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentOperationModes] DROP CONSTRAINT [FK_StaffDepartmentOperationModes_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_StaffExtraChargeActions', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraChargeActions]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_StaffExtraCharges', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraCharges]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffEstablishedPostChargeLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] DROP CONSTRAINT [FK_StaffEstablishedPostChargeLinks_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentInstallSoft_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentInstallSoft] DROP CONSTRAINT [FK_StaffDepartmentInstallSoft_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentInstallSoft_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentInstallSoft] DROP CONSTRAINT [FK_StaffDepartmentInstallSoft_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentSoftGroup_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentSoftGroup] DROP CONSTRAINT [FK_StaffDepartmentSoftGroup_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentSoftGroup_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentSoftGroup] DROP CONSTRAINT [FK_StaffDepartmentSoftGroup_CreatorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentSoftGroupLinks_StaffDepartmentSoftGroup', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] DROP CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_StaffDepartmentSoftGroup]
GO

IF OBJECT_ID ('FK_StaffDepartmentSoftGroupLinks_StaffDepartmentInstallSoft', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] DROP CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_StaffDepartmentInstallSoft]
GO

IF OBJECT_ID ('FK_StaffDepartmentSoftGroupLinks_EditorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] DROP CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_EditorUser]
GO

IF OBJECT_ID ('FK_StaffDepartmentSoftGroupLinks_CreatorUser', 'F') IS NOT NULL
	ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] DROP CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_CreatorUser]
GO



--2. �������� ������
if OBJECT_ID (N'Kladr', 'U') is not null
	DROP TABLE [dbo].[Kladr]
GO
CREATE TABLE [dbo].[Kladr](
	[Name] [nvarchar](52) NULL,
	[ShortName] [nvarchar](12) NULL,
	[Index] [nvarchar](6) NULL,
	[AltName] [nvarchar](50) NULL,
	[AddressType] [nvarchar](1) NULL,
	[RegionCode] [nvarchar](2) NULL,
	[AreaCode] [nvarchar](3) NULL,
	[CityCode] [nvarchar](3) NULL,
	[SettlementCode] [nvarchar](3) NULL,
	[StreetCode] [nvarchar](4) NULL,
	[Code] [nvarchar](50) NULL
) ON [PRIMARY]

GO


if OBJECT_ID (N'RefAddresses', 'U') is not null
	DROP TABLE [dbo].[RefAddresses]
GO	
CREATE TABLE [dbo].[RefAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Address] [nvarchar](400) NULL,
	[PostIndex] [nvarchar](6) NULL,
	[RegionCode] [nvarchar](30) NULL,
	[RegionName] [nvarchar](50) NULL,
	[AreaCode] [nvarchar](30) NULL,
	[AreaName] [nvarchar](50) NULL,
	[CityCode] [nvarchar](30) NULL,
	[CityName] [nvarchar](50) NULL,
	[SettlementCode] [nvarchar](30) NULL,
	[SettlementName] [nvarchar](50) NULL,
	[StreetCode] [nvarchar](30) NULL,
	[StreetName] [nvarchar](50) NULL,
	[HouseType] [int] NULL,
	[HouseNumber] [nvarchar](10) NULL,
	[BuildType] [int] NULL,
	[BuildNumber] [nvarchar](10) NULL,
	[FlatType] [int] NULL,
	[FlatNumber] [nvarchar](5) NULL,
	[IsUsed] [bit] NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_RefAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffProgramReference', 'U') is not null
	DROP TABLE [dbo].[StaffProgramReference]
GO	
CREATE TABLE [dbo].[StaffProgramReference](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffProgramReference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffProgramCodes', 'U') is not null
	DROP TABLE [dbo].[StaffProgramCodes]
GO	
CREATE TABLE [dbo].[StaffProgramCodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DMDetailId] [int] NULL,
	[ProgramId] [int] NULL,
	[Code] [nvarchar](20) NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffProgramCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffLandmarkTypes', 'U') is not null
	DROP TABLE [dbo].[StaffLandmarkTypes]
GO	
CREATE TABLE [dbo].[StaffLandmarkTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffLandmarkTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffExtraCharges', 'U') is not null
	DROP TABLE [dbo].[StaffExtraCharges]
GO	
CREATE TABLE [dbo].[StaffExtraCharges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [nvarchar](40) NULL,
	[Name] [nvarchar](100) NULL,
	[IsPostOnly] [bit] NULL,
	[UnitId] [int] NULL,
	[IsNeeded] [bit] NULL,
 CONSTRAINT [PK_StaffExtraCharges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostRequest', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostRequest]
GO	
CREATE TABLE [dbo].[StaffEstablishedPostRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DateRequest] [datetime] NULL,
	[RequestTypeId] [int] NOT NULL,
	[SEPId] [int] NULL,
	[PositionId] [int] NULL,
	[DepartmentId] [int] NULL,
	[ScheduleId] [int] NULL,
	[WCId] [int] NULL,
	[Quantity] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[IsUsed] [bit] NULL,
	[IsDraft] [bit] NULL,
	[DateSendToApprove] [datetime] NULL,
	[DateAccept] [datetime] NULL,
	[BeginAccountDate] [datetime] NULL,
	[SendTo1C] [datetime] NULL,
	[ReasonId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostArchive', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostArchive]
GO	
CREATE TABLE [dbo].[StaffEstablishedPostArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SEPId] [int] NOT NULL,
	[PositionId] [int] NULL,
	[DepartmentId] [int] NULL,
	[Quantity] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[IsUsed] [bit] NULL,
	[BeginAccountDate] [datetime] NULL,
	[Priority] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPost', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPost]
GO
CREATE TABLE [dbo].[StaffEstablishedPost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[PositionId] [int] NULL,
	[DepartmentId] [int] NULL,
	[Quantity] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[IsUsed] [bit] NULL,
	[BeginAccountDate] [datetime] NULL,
	[Priority] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentTypes', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentTypes]
GO
CREATE TABLE [dbo].[StaffDepartmentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentTaxDetails', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentTaxDetails]
GO
CREATE TABLE [dbo].[StaffDepartmentTaxDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[KPP] [nvarchar](9) NULL,
	[OKTMO] [nvarchar](11) NULL,
	[OKATO] [nvarchar](11) NULL,
	[OKPO] [nvarchar](10) NULL,
	[RegionCode] [nvarchar](2) NULL,
	[TaxAdminCode] [nvarchar](10) NULL,
	[TaxAdminName] [nvarchar](100) NULL,
	[PostAddress] [nvarchar](250) NULL,
 CONSTRAINT [PK_StaffDepartmentTaxDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentRequest', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentRequest]
GO
CREATE TABLE [dbo].[StaffDepartmentRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DateRequest] [datetime] NULL,
	--[Number] [int] NULL,
	[RequestTypeId] [int] NOT NULL,
	[DepartmentId] [int] NULL,
	[ItemLevel] [int] NULL,
	[ParentId] [int] NULL,
	[Name] [nvarchar](128) NULL,
	[BFGId] [int] NULL,
	[OrderNumber] [nvarchar](200) NULL,
	[OrderDate] [datetime] NULL,
	[LegalAddressId] [int] NULL,
	[IsTaxAdminAccount] [bit] NULL,
	[IsEmployeAvailable] [bit] NULL,
	[DepNextId] [int] NULL,
	[DepDepositId] [int] NULL,
	[IsPlan] [bit] NULL,
	[IsUsed] [bit] NULL,
	[IsDraft] [bit] NULL,
	[DateSendToApprove] [datetime] NULL,
	[BeginAccountDate] [datetime] NULL,
	[DateState] [datetime] NULL,
	[IsTaxRequest] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentOperations', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentOperations]
GO
CREATE TABLE [dbo].[StaffDepartmentOperations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[IsUsed] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentOperations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentOperationLinks', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentOperationLinks]
GO
CREATE TABLE [dbo].[StaffDepartmentOperationLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[OperGroupId] [int] NULL,
	[OperationId] [int] NULL,
	[IsUsed] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentOperationLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentManagerDetails', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentManagerDetails]
GO
CREATE TABLE [dbo].[StaffDepartmentManagerDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DepRequestId] [int] NULL,
	[DepCode] [nvarchar](20) NULL,
	[PrevDepCode] [nvarchar](20) NULL,
	[NameShort] [nvarchar](100) NULL,
	[NameComment] [nvarchar](50) NULL,
	[ReasonId] [int] NULL,
	[FactAddressId] [int] NULL,
	[DepStatus] [nvarchar](50) NULL,
	[DepTypeId] [int] NULL,
	[OpenDate] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[OperationMode] [nvarchar](150) NULL,
	[OperationModeCash] [nvarchar](150) NULL,
	[OperationModeATM] [nvarchar](150) NULL,
	[OperationModeCashIn] [nvarchar](150) NULL,
	[BeginIdleDate] [datetime] NULL,
	[EndIdleDate] [datetime] NULL,
	[RentPlaceId] [int] NULL,
	[AgreementDetails] [nvarchar](250) NULL,
	[DivisionArea] [numeric](18, 2) NULL,
	[AmountPayment] [numeric](18, 2) NULL,
	[Phone] [nvarchar](200) NULL,
	[IsBlocked] [bit] NULL,
	[NetShopId] [int] NULL,
	[IsLegalEntity] [bit] NULL,
	[PlanEPCount] [int] NULL,
	[PlanSalaryFund] [numeric](18, 2) NULL,
	[Note] [nvarchar](250) NULL,
	[CDAvailableId] [int] NULL,
	[SKB_GE_Id] [int] NULL,
	[SoftGroupId] [int] NULL,
	[OperGroupId] [int] NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentManagerDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentLandmarks', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentLandmarks]
GO
CREATE TABLE [dbo].[StaffDepartmentLandmarks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DMDetailId] [int] NULL,
	[LandmarkId] [int] NULL,
	[Description] [nvarchar](300) NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentLandmarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentCBDetails', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentCBDetails]
GO
CREATE TABLE [dbo].[StaffDepartmentCBDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DepRequestId] [int] NULL,
	[ATMCountTotal] [int] NULL,
	[ATMCashInCount] [int] NULL,
	[ATMCount] [int] NULL,
	[ATMCashInStarted] [int] NULL,
	[DepCachinId] [int] NULL,
	[DepATMId] [int] NULL,
	[CashInStartedDate] [datetime] NULL,
	[ATMStartedDate] [datetime] NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentCBDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'DepartmentArchive', 'U') is not null
	DROP TABLE [dbo].[DepartmentArchive]
GO
CREATE TABLE [dbo].[DepartmentArchive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[Code] [nvarchar](10) NULL,
	[Name] [nvarchar](128) NULL,
	[Code1C] [int] NULL,
	[ParentId] [int] NULL,
	[Path] [nvarchar](128) NULL,
	[ItemLevel] [int] NULL,
	[Priority] [int] NULL,
	[IsUsed] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_DepartmentArchive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffPostChargeLinks', 'U') is not null
	DROP TABLE [dbo].[StaffPostChargeLinks]
GO
CREATE TABLE [dbo].[StaffPostChargeLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[StaffExtraChargeId] [int] NULL,
	[Salary] [numeric](18, 2) NULL,
	[ActionId] [int] NULL,
	[StaffMovementsId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffPostChargeLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentRequestTypes', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentRequestTypes]
GO
CREATE TABLE [dbo].[StaffDepartmentRequestTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentRequestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentOperationModes', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentOperationModes]
GO
CREATE TABLE [dbo].[StaffDepartmentOperationModes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DMDetailId] [int] NULL,
	[ModeType] [int] NULL,
	[WeekDay] [int] NOT NULL,
	[WorkBegin] [nvarchar](5) NULL,
	[WorkEnd] [nvarchar](5) NULL,
	[BreakBegin] [nvarchar](5) NULL,
	[BreakEnd] [nvarchar](5) NULL,
	[IsWorkDay] [bit] NULL,
	[CreatorId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentOperationModes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostRequestTypes', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostRequestTypes]
GO
CREATE TABLE [dbo].[StaffEstablishedPostRequestTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostRequestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffEstablishedPostChargeLinks', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostChargeLinks]
GO
CREATE TABLE [dbo].[StaffEstablishedPostChargeLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[SEPRequestId] [int] NULL,
	[SEPId] [int] NULL,
	[StaffExtraChargeId] [int] NULL,
	[Amount] [numeric](18, 2) NULL,
	[IsUsed] [bit] NULL,
	[ActionId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostChargeLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentReasons', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentReasons]
GO
CREATE TABLE [dbo].[StaffDepartmentReasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffDepartmentReasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffNetShopIdentification', 'U') is not null
	DROP TABLE [dbo].[StaffNetShopIdentification]
GO
CREATE TABLE [dbo].[StaffNetShopIdentification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StaffNetShopIdentification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffDepartmentCashDeskAvailable', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentCashDeskAvailable]
GO
CREATE TABLE [dbo].[StaffDepartmentCashDeskAvailable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentCashDeskAvailable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffDepartmentRentPlace', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentRentPlace]
GO
CREATE TABLE [dbo].[StaffDepartmentRentPlace](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentRentPlace] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffDepartmentSKB_GE', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentSKB_GE]
GO
CREATE TABLE [dbo].[StaffDepartmentSKB_GE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentSKB_GE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentInstallSoft', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentInstallSoft]
GO
CREATE TABLE [dbo].[StaffDepartmentInstallSoft](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentInstallSoft] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentSoftGroup', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentSoftGroup]
GO
CREATE TABLE [dbo].[StaffDepartmentSoftGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentSoftGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentSoftGroupLinks', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentSoftGroupLinks]
GO
CREATE TABLE [dbo].[StaffDepartmentSoftGroupLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[SoftId] [int] NULL,
	[SoftGroupId] [int] NULL,
	[IsUsed] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentSoftGroupLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentBranch', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentBranch]
GO
CREATE TABLE [dbo].[StaffDepartmentBranch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Code] [nvarchar](2) NULL,
	[Name] [nvarchar](50) NULL,
	[DepartmentId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentBranch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentManagement', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentManagement]
GO
CREATE TABLE [dbo].[StaffDepartmentManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Code] [nvarchar](3) NULL,
	[Name] [nvarchar](50) NULL,
	[BranchId] [int] NULL,
	[DepartmentId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentManagement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentAdministration', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentAdministration]
GO
CREATE TABLE [dbo].[StaffDepartmentAdministration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Code] [nvarchar](7) NULL,
	[Name] [nvarchar](150) NULL,
	[ManagementId] [int] NULL,
	[DepartmentId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentAdministration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentBusinessGroup', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentBusinessGroup]
GO
CREATE TABLE [dbo].[StaffDepartmentBusinessGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Code] [nvarchar](11) NULL,
	[Name] [nvarchar](150) NULL,
	[AdminId] [int] NULL,
	[DepartmentId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentBusinessGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentRPLink', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentRPLink]
GO
CREATE TABLE [dbo].[StaffDepartmentRPLink](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Code] [nvarchar](15) NULL,
	[Name] [nvarchar](250) NULL,
	[BGId] [int] NULL,
	[DepartmentId] [int] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentRPLink] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentOperationGroups', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentOperationGroups]
GO
CREATE TABLE [dbo].[StaffDepartmentOperationGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsUsed] [bit] NULL,
	[CreatorID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffDepartmentOperationGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffDepartmentAccessory', 'U') is not null
	DROP TABLE [dbo].[StaffDepartmentAccessory]
GO
CREATE TABLE [dbo].[StaffDepartmentAccessory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_StaffDepartmentAccessory_CreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_StaffDepartmentAccessory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
--��� ��� ������������� ��� ����� ������ �� ����� �����������, �� ��������� ������������ ���
--StaffDepartmentAccessory
INSERT INTO StaffDepartmentAccessory (Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentAccessory (Version, Name) VALUES(1, N'�����')
INSERT INTO StaffDepartmentAccessory (Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentAccessory (Version, Name) VALUES(1, N'��������������')
INSERT INTO StaffDepartmentAccessory (Version, Name) VALUES(1, N'�������/�������')
INSERT INTO StaffDepartmentAccessory (Version, Name) VALUES(1, N'��������')



if OBJECT_ID (N'StaffWorkingConditions', 'U') is not null
	DROP TABLE [dbo].[StaffWorkingConditions]
GO
CREATE TABLE [dbo].[StaffWorkingConditions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_StaffWorkingConditions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffRequestPyrusTasks', 'U') is not null
	DROP TABLE [dbo].[StaffRequestPyrusTasks]
GO
CREATE TABLE [dbo].[StaffRequestPyrusTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepRequestId] [int] NULL,
	[SEPRequestId] [int] NULL,
	[ApproveId] [int] NULL,
	[NumberTask] [nvarchar](20) NULL,
	[CreatorId] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_StaffRequestPyrusTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffPostReplacement', 'U') is not null
	DROP TABLE [dbo].[StaffPostReplacement]
GO
CREATE TABLE [dbo].[StaffPostReplacement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserLinkId] [int] NULL,
	[UserId] [int] NULL,
	[ReplacedId] [int] NULL,
	[IsUsed] [bit] NULL,
	[CreatorId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffPostReplacement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



if OBJECT_ID (N'StaffEstablishedPostUserLinks', 'U') is not null
	DROP TABLE [dbo].[StaffEstablishedPostUserLinks]
GO
CREATE TABLE [dbo].[StaffEstablishedPostUserLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[SEPId] [int] NULL,
	[UserId] [int] NULL,
	[IsUsed] [bit] NULL,
	[ReserveType] [int] NULL,
	[DocId] [int] NULL,
	[IsDismissal] [bit] NULL,
	[CreatorId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[EditorId] [int] NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_StaffEstablishedPostUserLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffUnitReference', 'U') is not null
	DROP TABLE [dbo].[StaffUnitReference]
GO

CREATE TABLE [dbo].[StaffUnitReference](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_StaffUnitReference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


if OBJECT_ID (N'StaffExtraChargeActions', 'U') is not null
	DROP TABLE [dbo].[StaffExtraChargeActions]
GO
CREATE TABLE [dbo].[StaffExtraChargeActions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_StaffExtraChargeActions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



--3. �������� ������ � �����������
ALTER TABLE [dbo].[StaffExtraCharges]  WITH CHECK ADD  CONSTRAINT [FK_StaffExtraCharges_StaffUnitReference] FOREIGN KEY([UnitId])
REFERENCES [dbo].[StaffUnitReference] ([Id])
GO

ALTER TABLE [dbo].[StaffExtraCharges] CHECK CONSTRAINT [FK_StaffExtraCharges_StaffUnitReference]
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] ADD  CONSTRAINT [DF_StaffEstablishedPostUserLinks_IsDissmisial]  DEFAULT ((0)) FOR [IsDismissal]
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] ADD  CONSTRAINT [DF_StaffEstablishedPostUserLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostUserLinks_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostUserLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostUserLinks_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostUserLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostUserLinks_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostUserLinks_StaffEstablishedPost]
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostUserLinks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostUserLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostUserLinks_Users]
GO

ALTER TABLE [dbo].[StaffPostReplacement] ADD  CONSTRAINT [DF_StaffPostReplacement_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffPostReplacement]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostReplacement_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostReplacement] CHECK CONSTRAINT [FK_StaffPostReplacement_CreatorUser]
GO

ALTER TABLE [dbo].[StaffPostReplacement]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostReplacement_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostReplacement] CHECK CONSTRAINT [FK_StaffPostReplacement_EditorUser]
GO

ALTER TABLE [dbo].[StaffPostReplacement]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostReplacement_Replaced] FOREIGN KEY([ReplacedId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostReplacement] CHECK CONSTRAINT [FK_StaffPostReplacement_Replaced]
GO

ALTER TABLE [dbo].[StaffPostReplacement]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostReplacement_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostReplacement] CHECK CONSTRAINT [FK_StaffPostReplacement_User]
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks] ADD  CONSTRAINT [DF_StaffRequestPyrusTasks_DateCreate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks]  WITH CHECK ADD  CONSTRAINT [FK_StaffRequestPyrusTasks_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks] CHECK CONSTRAINT [FK_StaffRequestPyrusTasks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks]  WITH CHECK ADD  CONSTRAINT [FK_StaffRequestPyrusTasks_DocumentApproval] FOREIGN KEY([ApproveId])
REFERENCES [dbo].[DocumentApproval] ([Id])
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks] CHECK CONSTRAINT [FK_StaffRequestPyrusTasks_DocumentApproval]
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks]  WITH CHECK ADD  CONSTRAINT [FK_StaffRequestPyrusTasks_StaffDepartmentRequest] FOREIGN KEY([DepRequestId])
REFERENCES [dbo].[StaffDepartmentRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks] CHECK CONSTRAINT [FK_StaffRequestPyrusTasks_StaffDepartmentRequest]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] ADD  CONSTRAINT [DF_StaffDepartmentRequest_IsTaxRequest]  DEFAULT ((0)) FOR [IsTaxRequest]
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks]  WITH CHECK ADD  CONSTRAINT [FK_StaffRequestPyrusTasks_StaffEstablishedPostRequest] FOREIGN KEY([SEPRequestId])
REFERENCES [dbo].[StaffEstablishedPostRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffRequestPyrusTasks] CHECK CONSTRAINT [FK_StaffRequestPyrusTasks_StaffEstablishedPostRequest]
GO

ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_StaffDepartmentAccessory] FOREIGN KEY([BFGId])
REFERENCES [dbo].[StaffDepartmentAccessory] ([Id])
GO

ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_StaffDepartmentAccessory]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationGroups] ADD  CONSTRAINT [DF_StaffDepartmentOperationGroups_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationGroups]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationGroups_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationGroups] CHECK CONSTRAINT [FK_StaffDepartmentOperationGroups_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationGroups]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationGroups_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationGroups] CHECK CONSTRAINT [FK_StaffDepartmentOperationGroups_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink] ADD  CONSTRAINT [DF_StaffDepartmentRPLink_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRPLink_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink] CHECK CONSTRAINT [FK_StaffDepartmentRPLink_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRPLink_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink] CHECK CONSTRAINT [FK_StaffDepartmentRPLink_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRPLink_StaffDepartmentBusinessGroup] FOREIGN KEY([BGId])
REFERENCES [dbo].[StaffDepartmentBusinessGroup] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink] CHECK CONSTRAINT [FK_StaffDepartmentRPLink_StaffDepartmentBusinessGroup]
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRPLink_Department] FOREIGN KEY(DepartmentId)
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRPLink] CHECK CONSTRAINT [FK_StaffDepartmentRPLink_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] ADD  CONSTRAINT [DF_StaffDepartmentBusinessGroup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentBusinessGroup_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] CHECK CONSTRAINT [FK_StaffDepartmentBusinessGroup_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentBusinessGroup_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] CHECK CONSTRAINT [FK_StaffDepartmentBusinessGroup_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentBusinessGroup_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] CHECK CONSTRAINT [FK_StaffDepartmentBusinessGroup_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentBusinessGroup_StaffDepartmentAdministration] FOREIGN KEY([AdminId])
REFERENCES [dbo].[StaffDepartmentAdministration] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentBusinessGroup] CHECK CONSTRAINT [FK_StaffDepartmentBusinessGroup_StaffDepartmentAdministration]
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration] ADD  CONSTRAINT [DF_StaffDepartmentAdministration_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentAdministration_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration] CHECK CONSTRAINT [FK_StaffDepartmentAdministration_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentAdministration_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration] CHECK CONSTRAINT [FK_StaffDepartmentAdministration_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentAdministration_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration] CHECK CONSTRAINT [FK_StaffDepartmentAdministration_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentAdministration_StaffDepartmentManagement] FOREIGN KEY([ManagementId])
REFERENCES [dbo].[StaffDepartmentManagement] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentAdministration] CHECK CONSTRAINT [FK_StaffDepartmentAdministration_StaffDepartmentManagement]
GO

ALTER TABLE [dbo].[StaffDepartmentManagement] ADD  CONSTRAINT [DF_StaffDepartmentManagement_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagement]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagement_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagement] CHECK CONSTRAINT [FK_StaffDepartmentManagement_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentManagement]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagement_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagement] CHECK CONSTRAINT [FK_StaffDepartmentManagement_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentManagement]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagement_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagement] CHECK CONSTRAINT [FK_StaffDepartmentManagement_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentManagement]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagement_StaffDepartmentBranch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[StaffDepartmentBranch] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagement] CHECK CONSTRAINT [FK_StaffDepartmentManagement_StaffDepartmentBranch]
GO

ALTER TABLE [dbo].[StaffDepartmentBranch] ADD  CONSTRAINT [DF_StaffDepartmentBranch_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentBranch]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentBranch_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentBranch] CHECK CONSTRAINT [FK_StaffDepartmentBranch_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentBranch]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentBranch_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentBranch] CHECK CONSTRAINT [FK_StaffDepartmentBranch_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentBranch]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentBranch_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentBranch] CHECK CONSTRAINT [FK_StaffDepartmentBranch_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] ADD  CONSTRAINT [DF_StaffDepartmentSoftGroupLinks_IsUsed]  DEFAULT ((1)) FOR [IsUsed]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentSoftGroup] FOREIGN KEY([SoftGroupId])
REFERENCES [dbo].[StaffDepartmentSoftGroup] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentSoftGroup]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentOperationGroups] FOREIGN KEY([OperGroupId])
REFERENCES [dbo].[StaffDepartmentOperationGroups] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentOperationGroups]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] ADD  CONSTRAINT [DF_StaffDepartmentSoftGroupLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] CHECK CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] CHECK CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_StaffDepartmentInstallSoft] FOREIGN KEY([SoftId])
REFERENCES [dbo].[StaffDepartmentInstallSoft] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] CHECK CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_StaffDepartmentInstallSoft]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_StaffDepartmentSoftGroup] FOREIGN KEY([SoftGroupId])
REFERENCES [dbo].[StaffDepartmentSoftGroup] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroupLinks] CHECK CONSTRAINT [FK_StaffDepartmentSoftGroupLinks_StaffDepartmentSoftGroup]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroup] ADD  CONSTRAINT [DF_StaffDepartmentSoftGroup_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroup]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentSoftGroup_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroup] CHECK CONSTRAINT [FK_StaffDepartmentSoftGroup_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroup]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentSoftGroup_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentSoftGroup] CHECK CONSTRAINT [FK_StaffDepartmentSoftGroup_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentInstallSoft] ADD  CONSTRAINT [DF_StaffDepartmentInstallSoft_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentInstallSoft]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentInstallSoft_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentInstallSoft] CHECK CONSTRAINT [FK_StaffDepartmentInstallSoft_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentInstallSoft]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentInstallSoft_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentInstallSoft] CHECK CONSTRAINT [FK_StaffDepartmentInstallSoft_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentSKB_GE] FOREIGN KEY([SKB_GE_Id])
REFERENCES [dbo].[StaffDepartmentSKB_GE] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentSKB_GE]
GO

ALTER TABLE [dbo].[StaffDepartmentSKB_GE] ADD  CONSTRAINT [DF_StaffDepartmentSKB_GE_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRentPlace] FOREIGN KEY([RentPlaceId])
REFERENCES [dbo].[StaffDepartmentRentPlace] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRentPlace]
GO

ALTER TABLE [dbo].[StaffDepartmentRentPlace] ADD  CONSTRAINT [DF_StaffDepartmentRentPlace_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentCashDeskAvailable] ADD  CONSTRAINT [DF_StaffDepartmentCashDeskAvailable_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentCashDeskAvailable] FOREIGN KEY([CDAvailableId])
REFERENCES [dbo].[StaffDepartmentCashDeskAvailable] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentCashDeskAvailable]
GO

ALTER TABLE [dbo].[StaffNetShopIdentification] ADD  CONSTRAINT [DF_StaffNetShopIdentification_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffNetShopIdentification] FOREIGN KEY([NetShopId])
REFERENCES [dbo].[StaffNetShopIdentification] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffNetShopIdentification]
GO

ALTER TABLE [dbo].[StaffDepartmentReasons] ADD  CONSTRAINT [DF_StaffDepartmentReasons_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentReasons] FOREIGN KEY([ReasonId])
REFERENCES [dbo].[StaffDepartmentReasons] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentReasons]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] ADD  CONSTRAINT [DF_StaffEstablishedPostChargeLinks_Salary]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] ADD  CONSTRAINT [DF_StaffEstablishedPostChargeLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraChargeActions] FOREIGN KEY([ActionId])
REFERENCES [dbo].[StaffExtraChargeActions] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraChargeActions]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPost]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_Schedule] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedule] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_Schedule]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_StaffWorkingConditions] FOREIGN KEY([WCId])
REFERENCES [dbo].[StaffWorkingConditions] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_StaffWorkingConditions]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest] FOREIGN KEY([SEPRequestId])
REFERENCES [dbo].[StaffEstablishedPostRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffEstablishedPostRequest]
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraCharges] FOREIGN KEY([StaffExtraChargeId])
REFERENCES [dbo].[StaffExtraCharges] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostChargeLinks] CHECK CONSTRAINT [FK_StaffEstablishedPostChargeLinks_StaffExtraCharges]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequestTypes] ADD  CONSTRAINT [DF_StaffEstablishedPostRequestTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRequestTypes] ADD  CONSTRAINT [DF_StaffDepartmentRequestTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_DepartmentParent] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_DepartmentParent]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_DepartmentDeposit] FOREIGN KEY([DepDepositId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_DepartmentDeposit]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] ADD  CONSTRAINT [DF_StaffPostChargeLinks_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] ADD  CONSTRAINT [DF_StaffPostChargeLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraChargeActions] FOREIGN KEY([ActionId])
REFERENCES [dbo].[StaffExtraChargeActions] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraChargeActions]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_Staff] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_Staff]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraCharges] FOREIGN KEY([StaffExtraChargeId])
REFERENCES [dbo].[StaffExtraCharges] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_StaffExtraCharges]
GO

ALTER TABLE [dbo].[StaffPostChargeLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffPostChargeLinks_StaffMovements] FOREIGN KEY([StaffMovementsId])
REFERENCES [dbo].[StaffMovements] ([Id])
GO

ALTER TABLE [dbo].[StaffPostChargeLinks] CHECK CONSTRAINT [FK_StaffPostChargeLinks_StaffMovements]
GO

ALTER TABLE [dbo].[DepartmentArchive] ADD  CONSTRAINT [DF_DepartmentArchive_IsUsed]  DEFAULT ((1)) FOR [IsUsed]
GO

ALTER TABLE [dbo].[DepartmentArchive] ADD  CONSTRAINT [DF_DepartmentArchive_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[DepartmentArchive]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentArchive_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[DepartmentArchive] CHECK CONSTRAINT [FK_DepartmentArchive_CreatorUser]
GO

ALTER TABLE [dbo].[DepartmentArchive]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentArchive_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[DepartmentArchive] CHECK CONSTRAINT [FK_DepartmentArchive_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] ADD  CONSTRAINT [DF_StaffDepartmentCBDetails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_StaffDepartmentRequest] FOREIGN KEY([DepRequestId])
REFERENCES [dbo].[StaffDepartmentRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_StaffDepartmentRequest]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_DepATM] FOREIGN KEY([DepATMId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_DepATM]
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentCBDetails_DepCashin] FOREIGN KEY([DepCachinId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentCBDetails] CHECK CONSTRAINT [FK_StaffDepartmentCBDetails_DepCashin]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] ADD  CONSTRAINT [DF_StaffDepartmentLandmarks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails] FOREIGN KEY([DMDetailId])
REFERENCES [dbo].[StaffDepartmentManagerDetails] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_StaffDepartmentManagerDetails]
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentLandmarks_StaffLandmarkTypes] FOREIGN KEY([LandmarkId])
REFERENCES [dbo].[StaffLandmarkTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentLandmarks] CHECK CONSTRAINT [FK_StaffDepartmentLandmarks_StaffLandmarkTypes]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_DivisionArea]  DEFAULT ((0)) FOR [DivisionArea]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_AmountPayment]  DEFAULT ((0)) FOR [AmountPayment]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_IsBlocked]  DEFAULT ((0)) FOR [IsBlocked]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_PlanSalaryFund]  DEFAULT ((0)) FOR [PlanSalaryFund]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] ADD  CONSTRAINT [DF_StaffDepartmentManagerDetails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_RefAddresses] FOREIGN KEY([FactAddressId])
REFERENCES [dbo].[RefAddresses] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_RefAddresses]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRequest] FOREIGN KEY([DepRequestId])
REFERENCES [dbo].[StaffDepartmentRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentRequest]
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentTypes] FOREIGN KEY([DepTypeId])
REFERENCES [dbo].[StaffDepartmentTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentManagerDetails] CHECK CONSTRAINT [FK_StaffDepartmentManagerDetails_StaffDepartmentTypes]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] ADD  CONSTRAINT [DF_StaffDepartmentOperationLinks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperations] FOREIGN KEY([OperationId])
REFERENCES [dbo].[StaffDepartmentOperations] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperations]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperationGroups] FOREIGN KEY([OperGroupId])
REFERENCES [dbo].[StaffDepartmentOperationGroups] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationLinks] CHECK CONSTRAINT [FK_StaffDepartmentOperationLinks_StaffDepartmentOperationGroups]
GO

ALTER TABLE [dbo].[StaffDepartmentOperations] ADD  CONSTRAINT [DF_StaffDepartmentOperations_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentOperations]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperations_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperations] CHECK CONSTRAINT [FK_StaffDepartmentOperations_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperations]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperations_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperations] CHECK CONSTRAINT [FK_StaffDepartmentOperations_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] ADD  CONSTRAINT [DF_StaffDepartmentRequest_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_DepartmentNext] FOREIGN KEY([DepNextId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_DepartmentNext]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_RefAddresses] FOREIGN KEY([LegalAddressId])
REFERENCES [dbo].[RefAddresses] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_RefAddresses]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentRequestTypes] FOREIGN KEY([RequestTypeId])
REFERENCES [dbo].[StaffDepartmentRequestTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentRequestTypes]
GO

ALTER TABLE [dbo].[StaffDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentAccessory] FOREIGN KEY([BFGId])
REFERENCES [dbo].[StaffDepartmentAccessory] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentRequest] CHECK CONSTRAINT [FK_StaffDepartmentRequest_StaffDepartmentAccessory]
GO

ALTER TABLE [dbo].[StaffDepartmentTaxDetails]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentTaxDetails_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentTaxDetails] CHECK CONSTRAINT [FK_StaffDepartmentTaxDetails_Department]
GO

ALTER TABLE [dbo].[StaffDepartmentTypes] ADD  CONSTRAINT [DF_StaffDepartmentTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPost] ADD  CONSTRAINT [DF_StaffEstablishedPost_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO

ALTER TABLE [dbo].[StaffEstablishedPost] ADD  CONSTRAINT [DF_StaffEstablishedPost_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffEstablishedPost] ADD  CONSTRAINT [DF_StaffEstablishedPost_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_Department]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_EditorUser] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_EditorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPost]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPost_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPost] CHECK CONSTRAINT [FK_StaffEstablishedPost_Position]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] ADD  CONSTRAINT [DF_StaffEstablishedPostArchive_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] ADD  CONSTRAINT [DF_StaffEstablishedPostArchive_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] ADD  CONSTRAINT [DF_StaffEstablishedPostArchive_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_Department]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_Position]
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostArchive_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostArchive] CHECK CONSTRAINT [FK_StaffEstablishedPostArchive_StaffEstablishedPost]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] ADD  CONSTRAINT [DF_StaffEstablishedPostRequest_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] ADD  CONSTRAINT [DF_StaffEstablishedPostRequest_Salary]  DEFAULT ((0)) FOR [Salary]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] ADD  CONSTRAINT [DF_StaffEstablishedPostRequest_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_AppointmentReason] FOREIGN KEY([ReasonId])
REFERENCES [dbo].[AppointmentReason] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_AppointmentReason]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_CreatorUser] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_CreatorUser]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_Department]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_EditorUsers] FOREIGN KEY([EditorID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_EditorUsers]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_Position]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPost]
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest]  WITH CHECK ADD  CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes] FOREIGN KEY([RequestTypeId])
REFERENCES [dbo].[StaffEstablishedPostRequestTypes] ([Id])
GO

ALTER TABLE [dbo].[StaffEstablishedPostRequest] CHECK CONSTRAINT [FK_StaffEstablishedPostRequest_StaffEstablishedPostRequestTypes]
GO

ALTER TABLE [dbo].[StaffLandmarkTypes] ADD  CONSTRAINT [DF_StaffLandmarkTypes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffProgramCodes] ADD  CONSTRAINT [DF_StaffProgramCodes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_CreatorUser]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_EditorUser]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_StaffDepartmentManagerDetails] FOREIGN KEY([DMDetailId])
REFERENCES [dbo].[StaffDepartmentManagerDetails] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_StaffDepartmentManagerDetails]
GO

ALTER TABLE [dbo].[StaffProgramCodes]  WITH CHECK ADD  CONSTRAINT [FK_StaffProgramCodes_StaffProgramReference] FOREIGN KEY([ProgramId])
REFERENCES [dbo].[StaffProgramReference] ([Id])
GO

ALTER TABLE [dbo].[StaffProgramCodes] CHECK CONSTRAINT [FK_StaffProgramCodes_StaffProgramReference]
GO

ALTER TABLE [dbo].[StaffProgramReference] ADD  CONSTRAINT [DF_StaffProgramReference_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[RefAddresses] ADD  CONSTRAINT [DF_RefAddresses_IsUsed]  DEFAULT ((0)) FOR [IsUsed]
GO

ALTER TABLE [dbo].[RefAddresses] ADD  CONSTRAINT [DF_RefAddresses_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[RefAddresses]  WITH CHECK ADD  CONSTRAINT [FK_RefAddresses_Creators] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[RefAddresses] CHECK CONSTRAINT [FK_RefAddresses_Creators]
GO

ALTER TABLE [dbo].[RefAddresses]  WITH CHECK ADD  CONSTRAINT [FK_RefAddresses_Editors] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[RefAddresses] CHECK CONSTRAINT [FK_RefAddresses_Editors]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] ADD  CONSTRAINT [DF_StaffDepartmentOperationModes_IsWorkDay]  DEFAULT ((0)) FOR [IsWorkDay]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] ADD  CONSTRAINT [DF_StaffDepartmentOperationModes_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationModes_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] CHECK CONSTRAINT [FK_StaffDepartmentOperationModes_CreatorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationModes_EditorUser] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] CHECK CONSTRAINT [FK_StaffDepartmentOperationModes_EditorUser]
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes]  WITH CHECK ADD  CONSTRAINT [FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails] FOREIGN KEY([DMDetailId])
REFERENCES [dbo].[StaffDepartmentManagerDetails] ([Id])
GO

ALTER TABLE [dbo].[StaffDepartmentOperationModes] CHECK CONSTRAINT [FK_StaffDepartmentOperationModes_StaffDepartmentManagerDetails]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_StaffEstablishedPost] FOREIGN KEY([SEPId])
REFERENCES [dbo].[StaffEstablishedPost] ([Id])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_StaffEstablishedPost]
GO

--������ ��� ������� �����������
ALTER TABLE [dbo].[StaffMovements]  WITH CHECK ADD  CONSTRAINT [FK_StaffMovements_SourceUserLink] FOREIGN KEY([SourceStaffEstablishedPostRequest])
REFERENCES [dbo].[StaffEstablishedPostUserLinks] ([Id])
GO

ALTER TABLE [dbo].[StaffMovements] CHECK CONSTRAINT [FK_StaffMovements_SourceUserLink]
GO

ALTER TABLE [dbo].[StaffMovements]  WITH CHECK ADD  CONSTRAINT [FK_StaffMovements_TargetUserLink] FOREIGN KEY([TargetStaffEstablishedPostRequest])
REFERENCES [dbo].[StaffEstablishedPostUserLinks] ([Id])
GO

ALTER TABLE [dbo].[StaffMovements] CHECK CONSTRAINT [FK_StaffMovements_TargetUserLink]
GO

ALTER TABLE [dbo].[StaffMovementsFact]  WITH CHECK ADD  CONSTRAINT [FK_StaffMovementsFact_StaffEstablishedPostRequest] FOREIGN KEY([StaffEstablishedPostRequestId])
REFERENCES [dbo].[StaffEstablishedPostRequest] ([Id])
GO

ALTER TABLE [dbo].[StaffMovementsFact] CHECK CONSTRAINT [FK_StaffMovementsFact_StaffEstablishedPostRequest]
GO

--4. �������� ��������
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������ ������ � ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsTaxRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DepDepositId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraChargeActions', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraChargeActions', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� �������� � ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraChargeActions'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffUnitReference', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffUnitReference', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ������ ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffUnitReference'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'SEPId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'ReserveType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������/������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'DocId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'IsDismissal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������� ������ � ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostUserLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����� ���������� � ������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'UserLinkId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'ReplacedId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ��������� �����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostReplacement'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks', @level2type=N'COLUMN',@level2name=N'DepRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ��� ������� ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks', @level2type=N'COLUMN',@level2name=N'SEPRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks', @level2type=N'COLUMN',@level2name=N'ApproveId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks', @level2type=N'COLUMN',@level2name=N'NumberTask'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffRequestPyrusTasks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffWorkingConditions', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffWorkingConditions', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffWorkingConditions', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffWorkingConditions'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAccessory', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAccessory', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAccessory', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAccessory', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� �������������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAccessory'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationGroups'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��-��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ��-��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������-������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'BGId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� 6 ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ��-��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRPLink'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������-������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������-������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'AdminId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� 5 ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ������-�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBusinessGroup'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'ManagementId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� 4 ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentAdministration'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'BranchId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� 3 ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagement'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� 2 ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentBranch'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ����������� ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'SoftGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OperGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������� ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'SoftId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ����������� ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'SoftGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ����� � �������������� ����������� ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroupLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������ ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ����������� ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSoftGroup'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����������� ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentInstallSoft'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ � �����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'SKB_GE_Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSKB_GE', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSKB_GE', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSKB_GE', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���/GE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentSKB_GE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRentPlace', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRentPlace', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRentPlace', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������ ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRentPlace'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCashDeskAvailable', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCashDeskAvailable', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCashDeskAvailable', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCashDeskAvailable'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���� ������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'CDAvailableId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������������� ������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffNetShopIdentification'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���� ������������� �������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'NetShopId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentReasons'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ��� ������� ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'SEPRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'SEPId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'StaffExtraChargeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������� � ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'ActionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������� � �������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostChargeLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������ ��� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequestTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ������ ��� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequestTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DateState'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'ParentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� � �������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Priority'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'StaffExtraChargeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������� � ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'ActionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ �� �������� �����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'StaffMovementsId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������� � ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������� � ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffPostChargeLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� 1�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'Code1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� 1� ������������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'ParentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'Path'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'ItemLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� � �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'Priority'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ����������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DepartmentArchive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'DepRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCountTotal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� � �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCashInCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� � �������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ���������� ���������� � �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMCashInStarted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� ������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'DepCachinId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������� ������������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'DepATMId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������� ������ (������)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'CashInStartedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������� ��������� (������)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'ATMStartedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�� ��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentCBDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'DMDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'LandmarkId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentLandmarks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepRequestId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������������� ��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'NameShort'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� � �������� �������� ��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'NameComment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ��� ������������� ��� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'PrevDepCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'FactAddressId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DepTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������������ �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OpenDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'CloseDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������� � ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'ReasonId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OperationMode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OperationModeCash'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OperationModeATM'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'OperationModeCashIn'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'BeginIdleDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ����� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'EndIdleDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������������ ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'RentPlaceId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'AgreementDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'DivisionArea'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������������ �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'AmountPayment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'IsBlocked'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������������ ����������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'IsLegalEntity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� ���������� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'PlanEPCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'PlanSalaryFund'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'Note'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentManagerDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'OperGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'OperationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationLinks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� �������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperations'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DateRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'RequestTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'ItemLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'BFGId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'OrderNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'OrderDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'LegalAddressId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������� �� ���� � ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsTaxAdminAccount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������� ��������� � �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsEmployeAvailable'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DepNextId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsPlan'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'IsDraft'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� �� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'DateSendToApprove'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �� ��������� ��������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'KPP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'OKTMO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'OKATO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'OKPO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'TaxAdminCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ��������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'TaxAdminName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails', @level2type=N'COLUMN',@level2name=N'PostAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTaxDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'PositionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPost'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'SEPId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'PositionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� � �������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'Priority'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������/���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ��������/�������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ����������� ������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostArchive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'DateRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'RequestTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'SEPId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'PositionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'ScheduleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'WCId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'Salary'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'IsDraft'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� �� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'DateSendToApprove'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'DateAccept'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ������ ����� � �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'BeginAccountDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� (�����������) � 1�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'SendTo1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'ReasonId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� �������������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ �� ������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffEstablishedPostRequest'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� ������ ��� 1�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'GUID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'IsPostOnly'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'UnitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� ������������� ����� ��������, � �� ����� �� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges', @level2type=N'COLUMN',@level2name=N'IsNeeded'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffExtraCharges'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffLandmarkTypes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'DMDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ����������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'ProgramId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� (�������������?)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����� ����������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramCodes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� ����������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffProgramReference'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'PostIndex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ���/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ����/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� �������/��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ��������/����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������/�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ���� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'ShortName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������� ������ �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Index'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������������� �������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AltName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AddressType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AreaCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'CityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ����������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'SettlementCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'StreetCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������������� �������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id �������������� ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'DMDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��� ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'ModeType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'WeekDay'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'WorkBegin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ��������� �������� ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'WorkEnd'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������ ���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'BreakBegin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ���������� ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'BreakEnd'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������� �������� ��� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'IsWorkDay'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ������ ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� �������� ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���� ���������� ��������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����� ������ �������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StaffDepartmentOperationModes'
GO



--5. �������� �������������
IF OBJECT_ID ('vwKladr_1', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_1]
GO
CREATE VIEW [dbo].[vwKladr_1]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 1)

GO



IF OBJECT_ID ('vwKladr_2', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_2]
GO
CREATE VIEW [dbo].[vwKladr_2]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 2)
GO


IF OBJECT_ID ('vwKladr_3', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_3]
GO
CREATE VIEW [dbo].[vwKladr_3]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 3)
GO


IF OBJECT_ID ('vwKladr_4', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_4]
GO
CREATE VIEW [dbo].[vwKladr_4]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 4)
GO


IF OBJECT_ID ('vwKladr_5', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwKladr_5]
GO
CREATE VIEW [dbo].[vwKladr_5]
AS
SELECT     NULL AS Name, NULL AS ShortName, NULL AS [Index], NULL AS AltName, NULL AS AddressType, NULL AS RegionCode, NULL AS AreaCode, NULL AS CityCode, NULL
                       AS SettlementCode, NULL AS StreetCode, NULL AS Code
UNION ALL
SELECT     Name + ' ' + ShortName AS Name, ShortName, [Index], AltName, AddressType, RegionCode, AreaCode, CityCode, SettlementCode, StreetCode, Code
FROM         dbo.Kladr
WHERE     (AddressType = 5)
GO


IF OBJECT_ID ('vwStaffListDepartment', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffListDepartment]
GO

CREATE VIEW [dbo].[vwStaffListDepartment]
AS
SELECT A.Id, A.Code
				--���� ���� ����� ����, �� ��� ����������, ���������� ��������� �������� �����
			 ,case when B.DepNextId is not null and isnull(TaxAdminCode, '') = '' then K.Name + N' (' + A.Name + N')' else A.Name end as Name
			 ,A.Code1C, A.ParentId, A.Path, A.ItemLevel, A.CodeSKD, A.Priority, 
			 case when A.ItemLevel = 2 then D.Name 
						when A.ItemLevel = 3 then E.Name 
						when A.ItemLevel = 4 then F.Name 
						when A.ItemLevel = 5 then G.Name 
						when A.ItemLevel = 6 then H.Name
						else C.NameShort end as DepFingradName, 
			 --C.NameComment as DepFingradNameComment, 
			 I.Name as DepFingradNameComment,
			 C.DepCode as FinDepPointCode, A.BFGId
FROM Department as A
LEFT JOIN StaffDepartmentRequest as B ON B.DepartmentId = A.Id and B.IsUsed = 1 
LEFT JOIN StaffDepartmentManagerDetails as C ON C.DepRequestId = B.Id
--����� ��������� ������� ����������� ���������
LEFT JOIN StaffDepartmentBranch as D ON D.DepartmentId = A.Id
LEFT JOIN StaffDepartmentManagement as E ON E.DepartmentId = A.Id 
LEFT JOIN StaffDepartmentAdministration as F ON F.DepartmentId = A.Id 
LEFT JOIN StaffDepartmentBusinessGroup as G ON G.DepartmentId = A.Id 
LEFT JOIN StaffDepartmentRPLink as H ON H.DepartmentId = A.Id 
LEFT JOIN StaffDepartmentAccessory as I ON I.id = A.BFGId
--��������� ���������
LEFT JOIN StaffDepartmentTaxDetails as J ON J.DepartmentId = A.Id
LEFT JOIN Department as K ON K.Id = B.DepNextId
WHERE A.IsUsed = 1
GO

IF OBJECT_ID ('vwStaffPostSalary', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffPostSalary]
GO

--������������� ���� ��������� ��� ���������� �� ��� ��������
CREATE VIEW [dbo].[vwStaffPostSalary]
AS
SELECT UserId
			,sum(A.Salary) as Salary
			,sum(Regional) as Regional
			,sum(Personnel) as Personnel
			,sum(Territory) as Territory
			,sum(Front) as Front
			,sum(Drive) as Drive
			,sum(NorthAuto) as NorthAuto
			,sum(North) as North
			,sum(Qualification) as Qualification
			,((sum(A.Salary) * B.Rate) + sum(Personnel) + sum(Territory) + sum(Front) + sum(Drive) + sum(Qualification)) +
			 (((sum(A.Salary) * B.Rate) + sum(Personnel) + sum(Territory) + sum(Front) + sum(Drive) + sum(Qualification)) * (sum(Regional) / 100)) +
			 (((sum(A.Salary) * B.Rate) + sum(Personnel) + sum(Territory) + sum(Front) + sum(Drive) + sum(Qualification)) * (case when sum(NorthAuto) = 0 then sum(North) else sum(NorthAuto) end / 100))
			as TotalSalary
FROM (--������������ ��������
			SELECT UserId, 0 as Salary, 0 as Regional
						,case when StaffExtraChargeId = 4 then Salary else 0 end as Personnel	--������������ ��������
						,case when StaffExtraChargeId = 5 then Salary else 0 end as Territory	--��������������� ��������
						,case when StaffExtraChargeId = 10 then Salary else 0 end as Front	--����� ��������
						,case when StaffExtraChargeId = 3 then Salary else 0 end as Drive	--���������� ��������
						,case when StaffExtraChargeId = 7 then Salary else 0 end as NorthAuto	--�������� ������� ��������
						,case when StaffExtraChargeId = 16 then Salary else 0 end as North	--�������� ������ ��������
						,case when StaffExtraChargeId = 2 then Salary else 0 end as Qualification	--������������ ��������
			FROM StaffPostChargeLinks
			WHERE IsActive = 1
			UNION ALL
			--����� � ����������� ��������
			SELECT C.UserId, A.Salary, isnull(B.Amount, 0) as Regional, 0 as Personnel, 0 as Territory, 0 as Front, 0 as Drive, 0 as NorthAuto, 0 as North, 0 as Qualification
			FROM StaffEstablishedPost as A
			LEFT JOIN StaffEstablishedPostChargeLinks as B ON B.SEPId = A.Id
			INNER JOIN StaffEstablishedPostUserLinks as C ON C.SEPId = A.Id and C.IsUsed = 1
			INNER JOIN Users as D ON D.Id = C.UserId and D.IsActive = 1
			INNER JOIN StaffEstablishedPostRequest as E ON E.SEPId = A.Id and E.IsUsed = 1
			WHERE A.IsUsed = 1) as A
INNER JOIN Users as B ON B.Id = A.UserId
GROUP BY A.UserId, B.Rate

--select * from vwStaffPostSalary
GO


--6. ���������� ������������ �������
--StaffExtraChargeActions
INSERT INTO StaffExtraChargeActions(Name) VALUES('������'), ('��������'), ('�� ��������'), ('����������')

--StaffUnitReference
INSERT INTO StaffUnitReference(Name) VALUES(N'%'), (N'�����������'), (N'�����')

--StaffWorkingConditions
INSERT INTO StaffWorkingConditions(Version, Name) VALUES(1, N'�� ����� ���������� ��������� ���������')
INSERT INTO StaffWorkingConditions(Version, Name) VALUES(1, N'����������������')
INSERT INTO StaffWorkingConditions(Version, Name) VALUES(1, N'�������� ����� ������')
INSERT INTO StaffWorkingConditions(Version, Name) VALUES(1, N'������� �������� �������')
INSERT INTO StaffWorkingConditions(Version, Name) VALUES(1, N'�� ������ ������� ����')


--StaffDepartmentOperationGroups
INSERT INTO StaffDepartmentOperationGroups(Version, Name, IsUsed) VALUES(1, N'������ ������� 1', 1)	--1
INSERT INTO StaffDepartmentOperationGroups(Version, Name, IsUsed) VALUES(1, N'������ ������� 2', 1)	--2
INSERT INTO StaffDepartmentOperationGroups(Version, Name, IsUsed) VALUES(1, N'������ ������� 3', 1)	--3
INSERT INTO StaffDepartmentOperationGroups(Version, Name, IsUsed) VALUES(1, N'������ ������� 4', 1)	--4
INSERT INTO StaffDepartmentOperationGroups(Version, Name, IsUsed) VALUES(1, N'������ ������� 5', 1)	--5
INSERT INTO StaffDepartmentOperationGroups(Version, Name, IsUsed) VALUES(1, N'������ ������� 6', 1)	--6
INSERT INTO StaffDepartmentOperationGroups(Version, Name, IsUsed) VALUES(1, N'������ ������� 7', 1)	--7
--StaffDepartmentOperations
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'���������� ��������� �������', 1)																			--1
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'��������', 1)																													--2
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'���������� ������', 1)																									--3
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'����� (�������, �������) ������', 1)																		--4
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'���������� ��������� �������', 1)																			--5
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'����������  �������/������ �������� �� �/� ��', 1)											--6
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'����������� �� �� ������ ��', 1)																				--7
INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed) VALUES(1, N'���������� ��������� - "������ ������", Contact, ������� "�����"', 1)	--8
--StaffDepartmentOperationLinks
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 1, 1, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 2, 2, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 2, 3, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 2, 4, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 3, 5, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 3, 1, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 3, 6, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 3, 7, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 4, 4, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 4, 1, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 4, 6, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 5, 4, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 5, 1, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 6, 4, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 6, 1, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 6, 2, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 6, 7, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 7, 4, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 7, 1, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 7, 7, 1)
INSERT INTO StaffDepartmentOperationLinks(Version, OperGroupId, OperationId, IsUsed) VALUES(1, 7, 8, 1)

--StaffDepartmentBranch
INSERT INTO StaffDepartmentBranch(Version, Code, Name, DepartmentId) VALUES(1, N'01', N'�������� ����', 4129)				--1
INSERT INTO StaffDepartmentBranch(Version, Code, Name, DepartmentId) VALUES(1, N'02', N'����������� ������ (������)', null)				--2
INSERT INTO StaffDepartmentBranch(Version, Code, Name, DepartmentId) VALUES(1, N'03', N'���������� ������', 4131)		--3
INSERT INTO StaffDepartmentBranch(Version, Code, Name, DepartmentId) VALUES(1, N'04', N'����������� ������', 4132)	--4
INSERT INTO StaffDepartmentBranch(Version, Code, Name, DepartmentId) VALUES(1, N'05', N'����������������� � ������� ����������', null)	--5

--StaffDepartmentManagement
INSERT INTO StaffDepartmentManagement(Version, Code, Name, BranchId, DepartmentId)
SELECT 1 as Version, A.[Id ��������], A.[��������], B.Id as dep2id, d.id as dep3id
FROM DepFinManager as A
LEFT JOIN StaffDepartmentBranch as B on SUBSTRING(B.Code, 2, 1) = substring(cast(A.[ID ��������] as varchar(3)), 1, 1)
LEFT JOIN Department as C ON C.Id = B.DepartmentId
LEFT JOIN (SELECT distinct C.[ID_��������], D.Id, D.Name
						FROM TerraPoint as A
						INNER JOIN Department as B ON B.Id = A.PossibleDepartmentId and B.ItemLevel = 7 --and isnull(B.BFGId, 2) = 2
						INNER JOIN Fingrad_csv as C ON C.[���_�������������] = A.Code
						INNER JOIN Department as D ON B.Path like D.Path + N'%' and D.ItemLevel = 3 and (D.Name not like '%���%' and D.Name not like '%����������%' and D.Name not like '%������������%')
						WHERE A.PossibleDepartmentId is not null and A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, a.EndDate, getdate()) <= 30)) 
									and A.ParentId <> '') as D ON  D.[ID_��������] = A.[Id ��������]

--������ �������������� ��������
UPDATE StaffDepartmentManagement SET DepartmentId = 4175 WHERE Code = '301'

--StaffDepartmentAdministration
INSERT INTO StaffDepartmentAdministration(Version, Code, Name, ManagementId, DepartmentId)
SELECT 1 as [Version], A.[ID ���������� ��������], A.[���������� ��������], B.Id, case when A.[���������� ��������] like '%�������%' then null else D.Id end as Id--D.Id
FROM DepFinAdmin as A
LEFT JOIN StaffDepartmentManagement as B ON B.Name = A.[��������]
LEFT JOIN Department as E ON E.Id = B.DepartmentId
LEFT JOIN (SELECT distinct C.[ID_����������_��������_����������_��������], D.Id, D.Name, D.ParentId
						FROM TerraPoint as A
						INNER JOIN Department as B ON B.Id = A.PossibleDepartmentId and B.ItemLevel = 7 --and isnull(B.BFGId, 2) = 2
						INNER JOIN Fingrad_csv as C ON C.[���_�������������] = A.Code
						INNER JOIN Department as D ON B.Path like D.Path + N'%' and D.ItemLevel = 4 and D.Name not like '%����������%' --and D.Id not in (10397)
						WHERE A.PossibleDepartmentId is not null and A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, a.EndDate, getdate()) <= 30)) 
									and A.ParentId <> '') as D ON D.[ID_����������_��������_����������_��������] = A.[ID ���������� ��������] 
									and D.ParentId = E.Code1C
									--������ �������� �� ���������� ��������, ������ ��� ������ ������
									and not (A.[ID ���������� ��������] = '04-03-2' and D.Id = 10035)
									and not (A.[ID ���������� ��������] = '04-07-1' and D.Id = 4314)
									and not (A.[ID ���������� ��������] = '03-00-3' and D.Id = 10397)
									and not (A.[ID ���������� ��������] = '04-04-5' and D.Id = 5256)

--� ����������, ��� �������� ������� ����� � ����� ������������
UPDATE StaffDepartmentAdministration SET DepartmentId = null WHERE Name like '%�������%'

--StaffDepartmentBusinessGroup
--������� ������, ��� � ����������� ������-����� ��������, ������ ��� � ��� ���� ������������� � ������ ������, � ����������� � ����� ������ ������
INSERT INTO StaffDepartmentBusinessGroup (Version, Code, Name, AdminId, DepartmentId)
SELECT 1, A.[ID ������-������], A.[������-������], B.Id, C.Id
FROM DepFinBG as a
LEFT JOIN StaffDepartmentAdministration as B ON B.Name = A.[���������� ��������]
LEFT JOIN Department as E ON E.Id = B.DepartmentId
LEFT JOIN (SELECT distinct C.[������_������_ID_������_������], D.Id, D.ParentId, D.Name
					FROM TerraPoint as A
					INNER JOIN Department as B ON B.Id = A.PossibleDepartmentId and B.ItemLevel = 7 --and isnull(B.BFGId, 2) = 2
					INNER JOIN Fingrad_csv as C ON C.[���_�������������] = A.Code
					INNER JOIN Department as D ON B.Path like D.Path + N'%' and D.ItemLevel = 5
					WHERE A.PossibleDepartmentId is not null and A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, a.EndDate, getdate()) <= 30)) 
								and A.ParentId <> '') as C ON C.������_������_ID_������_������ = A.[ID ������-������]
								and C.ParentId = E.Code1C 
								--���������� �� ��������� ��������
								and A.[������-������] like '%' + substring(c.Name, charindex('"', c.Name), len(c.Name)) + '%'




--StaffDepartmentRPLink
INSERT INTO StaffDepartmentRPLink(Version, Code, Name, BGId, DepartmentId)
SELECT 1, A.[��� �� � �������], A.[��-��������], B.Id, C.Id--, C.Name
FROM DepFinRP as a
LEFT JOIN StaffDepartmentBusinessGroup as B ON B.Name = A.[��� �� � �������#������-������]
LEFT JOIN Department as E ON E.Id = B.DepartmentId
LEFT JOIN (SELECT distinct C.[��_��������_���_��_�_�������], D.Id, D.Name, D.ParentId
					FROM TerraPoint as A
					INNER JOIN Department as B ON B.Id = A.PossibleDepartmentId and B.ItemLevel = 7 --and isnull(B.BFGId, 2) = 2
					INNER JOIN Fingrad_csv as C ON C.[���_�������������] = A.Code
					INNER JOIN Department as D ON B.Path like D.Path + N'%' and D.ItemLevel = 6
					WHERE A.PossibleDepartmentId is not null and A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, a.EndDate, getdate()) <= 30)) 
								and A.ParentId <> '') as C ON C.[��_��������_���_��_�_�������] = A.[��� �� � �������]
								and C.ParentId = E.Code1C
								--���������� �� ��������� ��������
								and A.[��-��������] like '%' + substring(c.Name, charindex('"', c.Name), len(c.Name)) + '%'


--StaffDepartmentInstallSoft
INSERT INTO StaffDepartmentInstallSoft(Version, Name) VALUES(1, N'*')															--1
INSERT INTO StaffDepartmentInstallSoft(Version, Name) VALUES(1, N'=')															--2
INSERT INTO StaffDepartmentInstallSoft(Version, Name) VALUES(1, N'���-������ ��')									--3
INSERT INTO StaffDepartmentInstallSoft(Version, Name) VALUES(1, N'���-��� ��������')							--4
INSERT INTO StaffDepartmentInstallSoft(Version, Name) VALUES(1, N'���')														--5
INSERT INTO StaffDepartmentInstallSoft(Version, Name) VALUES(1, N'������� ������� (��, ��,��)')		--6
INSERT INTO StaffDepartmentInstallSoft(Version, Name) VALUES(1, N'��������')											--7


--StaffDepartmentSoftGroup
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 01')	--1
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 02')	--2
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 03')	--3
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 04')	--4
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 05')	--5
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 06')	--6
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 07')	--7
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 08')	--8
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 09')	--9
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 10')	--10
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 11')	--11
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 12')	--12
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 13')	--13
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 14')	--14
INSERT INTO StaffDepartmentSoftGroup(Version, Name) VALUES(1, N'������ �� - 15')	--15


--StaffDepartmentSoftGroupLinks
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 1, 1)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 2, 2)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 3, 3)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 4, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 5, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 5, 6)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 6, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 6, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 7, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 7, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 7, 6)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 8, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 8, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 8, 7)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 9, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 9, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 9, 7)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 9, 6)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 10, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 10, 3)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 11, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 11, 3)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 11, 6)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 12, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 12, 3)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 12, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 13, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 13, 3)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 13, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 13, 6)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 14, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 14, 3)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 14, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 14, 7)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 15, 4)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 15, 3)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 15, 5)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 15, 7)
INSERT INTO StaffDepartmentSoftGroupLinks(Version, SoftGroupId, SoftId) VALUES(1, 15, 6)


--StaffDepartmentSKB_GE
INSERT INTO StaffDepartmentSKB_GE(Name) VALUES(N'-')
INSERT INTO StaffDepartmentSKB_GE(Name) VALUES(N'exGE')
INSERT INTO StaffDepartmentSKB_GE(Name) VALUES(N'GE (�� ������������)')
INSERT INTO StaffDepartmentSKB_GE(Name) VALUES(N'ICICI')
INSERT INTO StaffDepartmentSKB_GE(Name) VALUES(N'���')


--StaffDepartmentRentPlace
INSERT INTO StaffDepartmentRentPlace(Name) VALUES(N'-')
INSERT INTO StaffDepartmentRentPlace(Name) VALUES(N'������')
INSERT INTO StaffDepartmentRentPlace(Name) VALUES(N'��� �������� (� ������ ���������� � ���)')
INSERT INTO StaffDepartmentRentPlace(Name) VALUES(N'��� �������� ������ (�� ���������)')
INSERT INTO StaffDepartmentRentPlace(Name) VALUES(N'�������������')


--StaffDepartmentCashDeskAvailable
INSERT INTO StaffDepartmentCashDeskAvailable(Name) VALUES(N'-')
INSERT INTO StaffDepartmentCashDeskAvailable(Name) VALUES(N'��')
INSERT INTO StaffDepartmentCashDeskAvailable(Name) VALUES(N'���� ����� ������������ ��������')
INSERT INTO StaffDepartmentCashDeskAvailable(Name) VALUES(N'���')
INSERT INTO StaffDepartmentCashDeskAvailable(Name) VALUES(N'��� �����')
INSERT INTO StaffDepartmentCashDeskAvailable(Name) VALUES(N'������ ����� ���������')


--StaffExtraCharges
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'4f4a4697-cc10-11dd-87ea-00304861d218', N'�������� �� ������� ��� ������� � ��������#1114', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'4f4a4696-cc10-11dd-87ea-00304861d218', N'�������� �� ������������#1115', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'd9cd6dfe-b4b0-11de-b733-003048359abd', N'�������� �� ���������� �������� ������#1116', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'784efe28-3634-11dd-b8e4-00304861d218', N'�������� ������������#1117', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'c693b11a-ec98-11df-aabb-003048ba0538', N'�������� ���������������#1123', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'66f08438-f006-44e8-b9ee-32a8dcf557ba', N'�������� �����������#1301', 1, 1, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, IsNeeded) VALUES(N'1f076cf3-1ebb-11e4-80c8-002590d1e727', N'�������� �������� (�������) 1#1302', 0, 1)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'a5ceb324-a745-11de-b733-003048359abd', N'�������� �������� (���.) 1#1302', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'91a004fc-d13e-11dd-b086-00308d000000', N'������� �� ���������� (������)#1113', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'521ba992-ef7d-11e2-8985-003048ba0538', N'�������� �� ���� ������ ������������ �����-�����#1128', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'9e6ec242-49f2-4320-a5aa-024c5d607aa3', N'������ �� ����� �� �������� ��� ������#1802', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'1671e1b6-0281-489c-b191-50e6fb241e75', N'������� �� ����� �� ������� �� 1.5 ���#1502', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'db5cc88b-4080-4061-8bba-42f22b500bb4', N'������� �� ����� �� ������� �� 3 ���#1503', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'35c7a5dd-d8e9-4aa0-8378-2a7e501d846a', N'����� �� ����#1101', 0, 3, 0)
INSERT INTO StaffExtraCharges([GUID], Name, IsPostOnly, UnitId, IsNeeded) VALUES(N'537ff7ed-5e51-48d1-bf5e-4f680cb3e1b7', N'����� �� �����#1102', 0, 3, 0)


--StaffLandmarkTypes
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'������� �����')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'��������� ����������')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'�������� �������')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'�������� ������')
INSERT INTO StaffLandmarkTypes(Name) VALUES(N'����� ������')

--StaffProgramReference
INSERT INTO StaffProgramReference(Name) VALUES(N'��������')
INSERT INTO StaffProgramReference(Name) VALUES(N'���')
--INSERT INTO StaffProgramReference(Name) VALUES(N'��������')
--INSERT INTO StaffProgramReference(Name) VALUES(N'��')
--INSERT INTO StaffProgramReference(Name) VALUES(N'���������')
--INSERT INTO StaffProgramReference(Name) VALUES(N'���')
--INSERT INTO StaffProgramReference(Name) VALUES(N'���/GE')

--StaffDepartmentRequestTypes
INSERT INTO StaffDepartmentRequestTypes(Version, Name) VALUES(1, N'�������� ��')
INSERT INTO StaffDepartmentRequestTypes(Version, Name) VALUES(1, N'��������� ���������� ��')
INSERT INTO StaffDepartmentRequestTypes(Version, Name) VALUES(1, N'�������� ��')
INSERT INTO StaffDepartmentRequestTypes(Version, Name) VALUES(1, N'���� ��������� ������')

--StaffEstablishedPostRequestTypes
INSERT INTO StaffEstablishedPostRequestTypes(Version, Name) VALUES(1, N'�������� ��')
INSERT INTO StaffEstablishedPostRequestTypes(Version, Name) VALUES(1, N'��������� ��')
INSERT INTO StaffEstablishedPostRequestTypes(Version, Name) VALUES(1, N'���������� ��')
INSERT INTO StaffEstablishedPostRequestTypes(Version, Name) VALUES(1, N'���� ��������� ������')

--DepartmentArchive
INSERT INTO DepartmentArchive(DepartmentId, Code, Name, Code1C, ParentId, Path, ItemLevel, IsUsed)
SELECT Id, Code, Name, Code1C, ParentId, Path, ItemLevel, 1 FROM Department

--StaffDepartmentReasons
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'-')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��� �����')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��� ����� ��')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'�������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'�� ������� - ��������������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� ����')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� ������ ���������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� �����������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� ��������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� �������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'������� �����������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'�� ����� ��')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��������� �����')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��������� �������')
INSERT INTO StaffDepartmentReasons(Name) VALUES (N'��������� ������� (��������� ���������� ������)')

--StaffNetShopIdentification
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'-')
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'�� ���������')
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'�� �������')
INSERT INTO StaffNetShopIdentification(Name) VALUES (N'�������')

--StaffDepartmentTypes
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'-')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'Cash-in')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������, Cash-in')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��1')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��2')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'��������� � ����������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����������������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����1')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����2')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'�����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'������')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���1')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���2')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'����')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'���')
INSERT INTO StaffDepartmentTypes(Version, Name) VALUES(1, N'������')





--����� �������������
UPDATE Department SET BFGId = 3	WHERE Name like '%���%'	
--UPDATE Department SET BFGId = null	WHERE Name like '%���%'	
	--��������� �������������� ��� ��������� ������
--UPDATE Department SET BFGId = 5	WHERE Name like '%����������%' or Name like '%������%' or Name like '%�� ���%' or Name like '%�������%' 
--UPDATE Department SET BFGId = null	WHERE BFGId = 5

IF DB_NAME() = 'WebAppTest' or DB_NAME() = 'WebAppTest2'
BEGIN
	INSERT INTO Kladr 
	SELECT * FROM WebAppSKB.dbo.Kladr

	UPDATE sysdiagrams SET definition = B.definition
	FROM sysdiagrams as A
	INNER JOIN WebAppSKB.dbo.sysdiagrams as B ON B.diagram_id = A.diagram_id 

END


DELETE FROM Messages WHERE CommentPlaceType in (2, 4)

--7. �������� �������

IF OBJECT_ID ('fnGetDepartmentOperationModes', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetDepartmentOperationModes]
GO

--������� ������� ����� ������ ��� ������������� �� Id �������������� ���������� ������
CREATE FUNCTION [dbo].[fnGetDepartmentOperationModes]
(
	@DMDetailId int
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
	,DMDetailId int
	,ModeType int
	,WeekDay int
	,WorkBegin nvarchar(5)
	,WorkEnd nvarchar(5)
	,BreakBegin nvarchar(5)
	,BreakEnd nvarchar(5)
	,IsWorkDay bit
)
AS
BEGIN

	INSERT INTO @ReturnTable
	SELECT Id, DMDetailId, ModeType, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay 
	FROM StaffDepartmentOperationModes
	WHERE DMDetailId = @DMDetailId

	IF NOT EXISTS (SELECT * FROM @ReturnTable)
	BEGIN
		INSERT INTO @ReturnTable
		SELECT Id, DMDetailId, ModeType, WeekDay, WorkBegin, WorkEnd, BreakBegin, BreakEnd, IsWorkDay FROM StaffDepartmentOperationModes WHERE DMDetailId = -1
		UNION ALL
		SELECT null, null, 1, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 1, 7, null, null, null, null, 0
		--��� �����
		UNION ALL
		SELECT null, null, 2, 7, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 2, 7, null, null, null, null, 0
		--��� ���������
		UNION ALL
		SELECT null, null, 3, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 3, 7, null, null, null, null, 0
		--��� ������
		UNION ALL
		SELECT null, null, 4, 1, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 2, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 3, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 4, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 5, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 6, null, null, null, null, 0
		UNION ALL
		SELECT null, null, 4, 7, null, null, null, null, 0
	END


	
	
--select * from dbo.fnGetDepartmentOperationModes(36) order by WeekDay

	RETURN 
END

GO







IF OBJECT_ID ('fnGetStaffEstablishedPostCountByDepartment', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedPostCountByDepartment]
GO


--������� ���������� ���������� ������� ������ ������������� �������� ���������� ��������� ������������� �� 7 ������ ������������
CREATE FUNCTION dbo.fnGetStaffEstablishedPostCountByDepartment
(
	@DepartmentId int	--Id �������������
)
RETURNS int
AS
BEGIN
	DECLARE @SEPCount int, @DepId int
	DECLARE @tbl table (DepartmentId int)

	--������������ �� �������� ������������� ���������� ������� ������
	IF (SELECT ItemLevel FROM Department WHERE Id = @DepartmentId) = 1
	BEGIN
		SELECT @SEPCount = isnull(sum(Quantity), 0) FROM StaffEstablishedPost WHERE IsUsed = 1
		RETURN @SEPCount
	END
	ELSE
		--SELECT @SEPCount = isnull(sum(Quantity), 0) FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and IsUsed = 1
		SELECT @SEPCount = isnull(sum(C.Quantity), 0) 
		FROM Department as A
		INNER JOIN Department as B ON B.Path like A.Path + N'%'
		INNER JOIN StaffEstablishedPost as C ON C.DepartmentId = B.Id and C.IsUsed = 1
		WHERE A.Id = @DepartmentId 
/*
	--���� ���� ����������� �������������, �� ������� � ���
	IF EXISTS (SELECT * FROM Department as A
						 INNER JOIN Department as B ON B.ParentId = A.Code1C and B.ItemLevel <= 7
						 WHERE A.Id = @DepartmentId)
	BEGIN
		INSERT INTO @tbl 
		SELECT B.Id FROM Department as A
		INNER JOIN Department as B ON B.ParentId = A.Code1C and B.ItemLevel <= 7
		WHERE A.Id = @DepartmentId

		WHILE EXISTS (SELECT * FROM @tbl)
		BEGIN
			SELECT top 1 @DepId = DepartmentId FROM @tbl

			SET @SEPCount += (SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(@DepId))

			DELETE FROM @tbl WHERE DepartmentId = @DepId
		END
	END
*/
	
	RETURN @SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4128) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4129) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4130) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4131) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4132) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(4205) as SEPCount
--SELECT dbo.fnGetStaffEstablishedPostCountByDepartment(8010) as SEPCount
END
GO


IF OBJECT_ID ('fnGetReplacedName', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetReplacedName]
GO


--������� ���������� ������ � ����������� ������������ � ������� ������� ��� ������� ����������
CREATE FUNCTION dbo.fnGetReplacedName
(
	--��� ������� ������� ����� ������� ���� �� ����������
	@LinkId int	--Id ����� ������� ������� � ����������
	,@ReplacedId int	--Id ���������� ������� � �������, �� ��� ����� �� ��������
)
RETURNS nvarchar(500)
AS
BEGIN
DECLARE 
	@ReplacedName nvarchar(500)
	--���������� ���� �������� ���������
	IF @ReplacedId is null	
		SELECT @ReplacedName = case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
														B.Name + N' - (' + convert(nvarchar, C.BeginDate, 103) + N' - ' + convert(nvarchar, C.EndDate, 103) + ')'
		FROM StaffPostReplacement as A
		INNER JOIN Users as B ON B.Id = A.ReplacedId and B.IsActive = 1 and B.RoleId & 2 > 0
		--���� ��������� �������� �� ����� �� ��������
		LEFT JOIN ChildVacation as C ON C.UserId = B.Id and C.SendTo1C is not null and C.DeleteDate is null and getdate() between C.BeginDate and C.EndDate 
		WHERE A.UserLinkId = @LinkId and A.IsUsed = 1
	ELSE	--���������� ����������, ������� ���� � ������ �� ����� �� ��������, �� ��������� ��� ��������
		SELECT @ReplacedName = A.Name + N' - (' + convert(nvarchar, B.BeginDate, 103) + N' - ' + convert(nvarchar, B.EndDate, 103) + N')'
		FROM Users as A 
		--���� ��������� �������� �� ����� �� ��������
		LEFT JOIN ChildVacation as B ON B.UserId = A.Id and B.SendTo1C is not null and B.DeleteDate is null and getdate() between B.BeginDate and B.EndDate 
		WHERE A.Id = @ReplacedId and A.IsActive = 1 and A.RoleId & 2 > 0

	
	
	
	RETURN @ReplacedName
END
GO








IF OBJECT_ID ('fnGetStaffEstablishedArrangements', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
GO

--������� ������� ������� ����������� �� ���������� ������������� + ������� ��������� �������� 
CREATE FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
(
	@DepartmentId int
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
	,SEPId int
	,PositionId int
	,PositionName nvarchar(250)
	,DepartmentId int
	,Quantity int
	,Salary numeric(18, 2)
	,Path nvarchar(250)
	,RequestId int
	,Rate decimal(18, 2)
	,UserId int
	,Surname nvarchar(250)
	,ReplacedId int
	,ReplacedName nvarchar(500)
	,ReserveType int
	,Reserve nvarchar(50)
	,DocId int
	,IsReserve bit	--������� ������������ ��������
	,IsPregnant bit
	,IsVacation bit	--��������
	,IsSTD bit			--�������� �� �������� ��������
	,IsDismiss bit
	,IsDismissal bit
	--����� � ��������
	,SalaryPersonnel numeric(18, 2)	--����� (�� �������������)
	,Regional numeric(18, 2)
	,Personnel numeric(18, 2)
	,Territory numeric(18, 2)
	,Front numeric(18, 2)
	,Drive numeric(18, 2)
	,North numeric(18, 2)
	,Qualification numeric(18, 2)
	,TotalSalary numeric(18, 2)
)
AS
BEGIN
	INSERT INTO @ReturnTable
	SELECT F.Id, A.Id as SEPId, A.PositionId, B.Name as PositionName, A.DepartmentId, 1 as Quantity, A.Salary, C.Path, D.Id as RequestId, 
				 E.Rate,	--������
				 --���� � ������� � ����� �� �������� � ��� ������ ���������� � �������� ��� ����������
				 case when E.IsPregnant = 1 then null else E.Id end as UserId, 
				 --case when E.IsPregnant = 1 then null else E.Name end as Surname, 
				 case when (case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) and isnull(F.ReserveType, 0) = 0 then 1 else 0 end) = 1 or (case when isnull(F.DocId, 0) = 0 then 0 else 1 end) = 1
							then (case when (case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end) = 1 
												 then '��������� ��������' else '��������' end) 
							else E.Name end as Surname, 
												 
				 case when E.IsPregnant = 1 then E.Id else G.ReplacedId end as ReplacedId
				 ,case when E.IsPregnant = 1 then isnull(dbo.fnGetReplacedName(null, E.Id), E.Name)  else isnull(dbo.fnGetReplacedName(F.Id, null), H.Name) end as ReplacedName
				 ,F.ReserveType
				 ,case when F.ReserveType = 1 then N'�����������' when F.ReserveType = 2 then N'�����' end as Reserve
				 ,F.DocId
				 ,cast(case when isnull(F.DocId, 0) = 0 then 0 else 1 end as bit) as IsReserve
				 ,E.IsPregnant
				 ,case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) and isnull(F.ReserveType, 0) = 0 then 1 else 0 end as IsVacation
				 --,case when (case when E.IsPregnant = 1 then null else E.Id end) is null and H.Id is not null then 1 else 0 end as IsSTD
				 ,case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end as IsSTD
				 ,case when J.UserId is null then 0 else 1 end as IsDismiss	--����������
				 ,F.IsDismissal		--����������
				 --����� � ��������
				 ,I.Salary as SalaryPersonnel
				 ,I.Regional
				 ,I.Personnel
				 ,I.Territory
				 ,I.Front
				 ,I.Drive
				 ,case when I.NorthAuto = 0 then I.North else I.NorthAuto end as North
				 ,I.Qualification
				 ,isnull(I.TotalSalary, A.Salary) as TotalSalary	--���� ��������, �� ���� �������� ����� ������� �������
	FROM StaffEstablishedPost as A
	INNER JOIN Position as B ON B.Id = A.PositionId
	INNER JOIN Department as C ON C.Id = A.DepartmentId
	INNER JOIN StaffEstablishedPostRequest as D ON D.SEPId = A.Id and D.IsUsed = 1
	--INNER JOIN Users as E ON E.SEPId = A.Id and E.IsActive = 1 and E.RoleId & 2 > 0
	INNER JOIN StaffEstablishedPostUserLinks as F ON F.SEPId = A.Id and F.IsUsed = 1
	LEFT JOIN Users as E ON E.Id = F.UserId and E.IsActive = 1 and E.RoleId & 2 > 0 --and E.IsPregnant = 0
	LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = F.Id and F.IsUsed = 1
	LEFT JOIN Users as H ON H.Id = G.ReplacedId
	LEFT JOIN vwStaffPostSalary as I ON I.UserId = E.Id
	LEFT JOIN (SELECT UserId FROM Dismissal 
						 WHERE UserDateAccept is not null and DeleteDate is null
						 GROUP BY UserId) as J ON J.UserId = E.Id
	WHERE A.DepartmentId = @DepartmentId /*and A.PositionId = 356*/ and A.IsUsed = 1 
				--���������� ������� �� ������ ���� ��������
				--and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and ReplacedId = E.Id)
	ORDER BY A.Priority

		
--select * from dbo.fnGetStaffEstablishedArrangements(7924) 

	RETURN 
END

GO





IF OBJECT_ID ('fnGetFingradStructureForDeparment', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetFingradStructureForDeparment]
GO

--������� ������� ��������� �������� ��� �������������
CREATE FUNCTION [dbo].[fnGetFingradStructureForDeparment]
(
	@DepartmentId int	--������������ �������������
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






















