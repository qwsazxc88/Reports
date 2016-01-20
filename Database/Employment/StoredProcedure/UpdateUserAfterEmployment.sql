IF OBJECT_ID ('UpdateUserAfterEmployment', 'P') IS NOT NULL
	DROP PROCEDURE dbo.UpdateUserAfterEmployment
GO

--��������� ��������� ������ ��� ��������� ��� ������
CREATE PROCEDURE dbo.UpdateUserAfterEmployment
	@CandidateId int	--Id ������ � ������
AS
BEGIN
SET NOCOUNT ON
DECLARE 
	@UserId int	--Id ����������
	,@UserLinkId int	--Id � �����������
	,@PersonalAddition numeric(18, 2)	--������������ �������� (���)
	,@AreaAddition numeric(18, 2)	--��������������� �������� (���)
	,@TravelRelatedAddition numeric(18, 2)	--�������� �� ���������� �������� ������ (���)
	,@CompetenceAddition numeric(18, 2)	--�������� �� ������������ (���)
	,@FrontOfficeExperienceAddition numeric(18, 2)	--�������� �� ���� ������ ������������ �����-����� (���)
	,@NorthernAreaAddition numeric(18, 2)	--�������� %
BEGIN TRANSACTION	
	SELECT @UserId = UserId FROM EmploymentCandidate WHERE Id = @CandidateId
	SELECT @UserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE DocId = @CandidateId and ReserveType = 2

	--���� ����� �� �������� ��, �� � ����������� ����� ������ ������ ����������� � ����� ������� ������� �� ������
	--������� ������ � ������ ���������
	IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE DocId = @CandidateId and ReserveType = 2 and UserId is not null)
	BEGIN
		INSERT INTO StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed)
		SELECT Id, @UserId, UserId, 1 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId

		--����������� ��������� ����� ������
		UPDATE Users SET TempUserLinkId = @UserLinkId WHERE Id = @UserId
	END
	ELSE--����������� ���������� ����� ������
	BEGIN
		UPDATE Users SET RegularUserLinkId = @UserLinkId WHERE Id = @UserId
	END

	--������ ��������� ���������� �� ����������������� �����
	UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId, DocId = null, ReserveType = null, IsDismissal = 0, IsUsed = 1
	WHERE Id = @UserLinkId

	--������� ������������ ��������
	SELECT @PersonalAddition = isnull(B.PersonalAddition, 0)
				,@AreaAddition = isnull(B.AreaAddition, 0)
				,@TravelRelatedAddition = isnull(B.TravelRelatedAddition, 0)
				,@CompetenceAddition = isnull(B.CompetenceAddition, 0)
				,@FrontOfficeExperienceAddition = isnull(B.FrontOfficeExperienceAddition, 0)
				,@NorthernAreaAddition = isnull(B.NorthernAreaAddition, 0)
	FROM EmploymentCandidate as A
	INNER JOIN PersonnelManagers as B ON B.CandidateId = A.Id
	WHERE A.Id = @CandidateId
	
	IF @PersonalAddition <> 0
	BEGIN
		INSERT INTO StaffPostChargeLinks(UserId, StaffExtraChargeId, Salary, ActionId, CreateDate, IsActive)
		SELECT @UserId, Id, @PersonalAddition, 1, getdate(), 1
		FROM StaffExtraCharges
		WHERE GUID = N'784efe28-3634-11dd-b8e4-00304861d218'
	END

	IF @AreaAddition <> 0
	BEGIN
		INSERT INTO StaffPostChargeLinks(UserId, StaffExtraChargeId, Salary, ActionId, CreateDate, IsActive)
		SELECT @UserId, Id, @AreaAddition, 1, getdate(), 1
		FROM StaffExtraCharges
		WHERE GUID = N'c693b11a-ec98-11df-aabb-003048ba0538'
	END

	IF @TravelRelatedAddition <> 0
	BEGIN
		INSERT INTO StaffPostChargeLinks(UserId, StaffExtraChargeId, Salary, ActionId, CreateDate, IsActive)
		SELECT @UserId, Id, @TravelRelatedAddition, 1, getdate(), 1
		FROM StaffExtraCharges
		WHERE GUID = N'd9cd6dfe-b4b0-11de-b733-003048359abd'
	END

	IF @CompetenceAddition <> 0
	BEGIN
		INSERT INTO StaffPostChargeLinks(UserId, StaffExtraChargeId, Salary, ActionId, CreateDate, IsActive)
		SELECT @UserId, Id, @CompetenceAddition, 1, getdate(), 1
		FROM StaffExtraCharges
		WHERE GUID = N'4f4a4696-cc10-11dd-87ea-00304861d218'
	END

	IF @FrontOfficeExperienceAddition <> 0
	BEGIN
		INSERT INTO StaffPostChargeLinks(UserId, StaffExtraChargeId, Salary, ActionId, CreateDate, IsActive)
		SELECT @UserId, Id, @FrontOfficeExperienceAddition, 1, getdate(), 1
		FROM StaffExtraCharges
		WHERE GUID = N'521ba992-ef7d-11e2-8985-003048ba0538'
	END

	IF @NorthernAreaAddition <> 0
	BEGIN
		INSERT INTO StaffPostChargeLinks(UserId, StaffExtraChargeId, Salary, ActionId, CreateDate, IsActive)
		SELECT @UserId, Id, 0, 1, getdate(), 1
		FROM StaffExtraCharges
		WHERE GUID = N'1f076cf3-1ebb-11e4-80c8-002590d1e727'
	END

COMMIT TRANSACTION
    
END
GO
